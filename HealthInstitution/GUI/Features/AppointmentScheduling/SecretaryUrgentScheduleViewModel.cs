using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.NotificationManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Features.AppointmentScheduling.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class SecretaryUrgentScheduleViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { OnPropertyChanged(ref _patients, value); }
        }

        private Patient? _selectedPatient;
        public Patient? SelectedPatient
        {
            get { return _selectedPatient; }
            set { OnPropertyChanged(ref _selectedPatient, value); }
        }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;
        private readonly ISchedulingService _schedulingService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorService _doctorService;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;
        private readonly IUserNotificationRepository _userNotificationRepository;
        private readonly IRoomService _roomService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ScheduleExamination { get; private set; }

        public ICommand ScheduleOperation { get; private set; }

        #endregion

        public SecretaryUrgentScheduleViewModel(ISchedulingService schedulingService, IDialogService dialogService, IAppointmentRepository appointmentRepository,
            IDoctorService doctorService, IPatientRepository patientRepository, IPatientService patientService, IUserNotificationRepository userNotificationRepository, IRoomService roomService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());

            _appointmentRepository = appointmentRepository;
            _doctorService = doctorService;
            _patientRepository = patientRepository;
            _dialogService = dialogService;
            _userNotificationRepository = userNotificationRepository;
            _roomService = roomService;
            _schedulingService = schedulingService;
            _patientService = patientService;

            SearchCommand = new RelayCommand(Search);

            ScheduleExamination = new RelayCommand(() => { UrgentSchedule(AppointmentType.Regular); }, () => SelectedPatient != null);

            ScheduleOperation = new RelayCommand(() => { UrgentSchedule(AppointmentType.Operation); }, () => SelectedPatient != null);
        }

        #region Schedule logic

        public void UrgentSchedule(AppointmentType appointmentType)
        {
            DoctorSpecializationSelectViewModel doctorSpecializationSelectVM = new DoctorSpecializationSelectViewModel();
            Tuple<DoctorSpecialization, bool?> chosenSpecialization = _dialogService.OpenDialogWithReturnType(doctorSpecializationSelectVM);

            // Dialog was closed
            if (chosenSpecialization.Item2 == false)
                return;

            DoctorSpecialization doctorSpecialization = chosenSpecialization.Item1;
            IList<Doctor> availableDoctors = new List<Doctor>(_doctorService.FindDoctorsWithSpecialization(doctorSpecialization));

            if (availableDoctors.Count == 0)
            {
                MessageBox.Show("There aren't any available doctors for chosen specialization.");
                return;
            }

            if (TryToSchedule(availableDoctors, appointmentType))
            {
                MessageBox.Show("Appointment is successfully created.");
                return;
            }

            ShowAppointmentsToDelay(availableDoctors);
        }

        public bool TryToSchedule(IList<Doctor> availableDoctors, AppointmentType appointmentType)
        {
            DateTime now = DateTime.Now;
            DateTime end = now.AddHours(2);

            RoomType roomType = appointmentType == AppointmentType.Regular ? RoomType.ExaminationRoom : RoomType.OperationRoom;

            for (DateTime potentialTime = now.AddMinutes(15); potentialTime <= end; potentialTime = potentialTime.AddMinutes(5))
            {
                Room potentialRoom = _roomService.FindFreeRoom(roomType, potentialTime, potentialTime.AddMinutes(15));

                if (potentialRoom == null)
                    continue;

                foreach (var doctor in availableDoctors)
                {
                    if (_doctorService.IsDoctorAvailable(doctor, potentialTime, potentialTime.AddMinutes(15)))
                    {
                        Appointment newAppointment = new Appointment
                        {
                            Doctor = doctor,
                            Patient = _patientRepository.Read(_selectedPatient.Id),
                            StartDate = potentialTime,
                            EndDate = potentialTime.AddMinutes(15),
                            Room = potentialRoom,
                            AppointmentType = appointmentType,
                            IsDone = false,
                            Anamnesis = null
                        };

                        _appointmentRepository.Create(newAppointment);

                        return true;
                    }
                }
            }

            return false;
        }

        public void ShowAppointmentsToDelay(IList<Doctor> availableDoctors)
        {
            IList<Tuple<Appointment, DateTime>> appointmentDelayPairs = GetCandidatesToDelay(availableDoctors);

            DelayAppointmentViewModel delayAppointmentVM = new DelayAppointmentViewModel(_roomService, _appointmentRepository, _patientRepository,
                _schedulingService, _userNotificationRepository, appointmentDelayPairs, _selectedPatient.Id);
            _dialogService.OpenDialog(delayAppointmentVM);
        }

        public IList<Tuple<Appointment, DateTime>> GetCandidatesToDelay(IList<Doctor> availableDoctors)
        {
            IList<Tuple<Appointment, DateTime>> appointmentDelayPairs = new List<Tuple<Appointment, DateTime>>();

            foreach (var doctor in availableDoctors)
            {
                IEnumerable<Appointment> appointments = _schedulingService.GetAppointmentsForDateRangeAndDoctor(DateTime.Now.AddMinutes(15), DateTime.Now.AddHours(2), doctor);
                foreach (var appointment in appointments)
                {
                    DateTime delayedTime = FindTimeToDelayAppointment(appointment);

                    Tuple<Appointment, DateTime> delayPair = Tuple.Create(appointment, delayedTime);
                    appointmentDelayPairs.Add(delayPair);
                }
            }

            return appointmentDelayPairs.OrderBy(pair => pair.Item2.Subtract(pair.Item1.StartDate))
                                        .Take(5)
                                        .ToList();
        }

        public DateTime FindTimeToDelayAppointment(Appointment appointment)
        {
            DateTime potentialTime = appointment.StartDate.AddMinutes(10);

            while (true)
            {
                potentialTime = potentialTime.AddMinutes(5);

                Room freeRoom = _roomService.FindFreeRoom(appointment.Room.RoomType, potentialTime, potentialTime.AddMinutes(15));

                if (freeRoom == null)
                    continue;

                if (_doctorService.IsDoctorAvailable(appointment.Doctor, potentialTime, potentialTime.AddMinutes(15)))
                    return potentialTime;
            }
        }

        #endregion

        public void UpdatePage()
        {
            Patients = new ObservableCollection<Patient>(_patientService.ReadAllValidPatients());
        }

        private void Search()
        {
            if (SearchText == "" || SearchText == null)
                UpdatePage();
            else
                Patients = new ObservableCollection<Patient>(_patientService.FilterValidPatientsBySearchText(SearchText));
        }


    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.room;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.secretary
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

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { OnPropertyChanged(ref _selectedPatient, value); }
        }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly INotificationService _notificationService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ScheduleExamination { get; private set; }

        public ICommand ScheduleOperation { get; private set; }

        #endregion

        public SecretaryUrgentScheduleViewModel(IDialogService dialogService, IAppointmentService appointmentService,
            IDoctorService doctorService, IPatientService patientService, INotificationService notificationService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());

            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _dialogService = dialogService;
            _notificationService = notificationService;

            SearchCommand = new RelayCommand(() =>
            {
                Search();
            });

            ScheduleExamination = new RelayCommand(() => { UrgentSchedule(AppointmentType.Regular); });

            ScheduleOperation = new RelayCommand(() => { UrgentSchedule(AppointmentType.Operation); });
        }

        #region Schedule logic

        public void UrgentSchedule(AppointmentType appointmentType)
        {
            if (_selectedPatient == null)
            {
                MessageBox.Show("You did not select any patient");
                return;
            }

            DoctorSpecializationSelectViewModel doctorSpecializationSelectVM = new DoctorSpecializationSelectViewModel();
            Tuple<DoctorSpecialization, bool?> chosenSpecialization = _dialogService.OpenDialogWithReturnType(doctorSpecializationSelectVM);

            // Dialog was closed
            if (chosenSpecialization.Item2 == false)
                return;

            DoctorSpecialization doctorSpecialization = chosenSpecialization.Item1;
            IList<Doctor> availableDoctors = _doctorService.GetDoctorsForDoctorSpecialization(doctorSpecialization);

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
                Room potentialRoom = _appointmentService.FindFreeRoom(roomType, potentialTime, potentialTime.AddMinutes(15));

                if (potentialRoom == null)
                    continue;

                foreach (var doctor in availableDoctors)
                {
                    if (_appointmentService.IsDoctorAvailable(doctor, potentialTime, potentialTime.AddMinutes(15)))
                    {
                        Appointment newAppointment = new Appointment
                        {
                            Doctor = doctor,
                            Patient = _patientService.Read(_selectedPatient.Id),
                            StartDate = potentialTime,
                            EndDate = potentialTime.AddMinutes(15),
                            Room = potentialRoom,
                            AppointmentType = appointmentType,
                            IsDone = false,
                            Anamnesis = null
                        };

                        _appointmentService.Create(newAppointment);

                        return true;
                    }
                }
            }

            return false;
        }

        public void ShowAppointmentsToDelay(IList<Doctor> availableDoctors)
        {
            IList<Tuple<Appointment, DateTime>> appointmentDelayPairs = GetCandidatesToDelay(availableDoctors);

            DelayAppointmentViewModel delayAppointmentVM = new DelayAppointmentViewModel(_patientService, _appointmentService,
                _notificationService, appointmentDelayPairs, _selectedPatient.Id);
            _dialogService.OpenDialog(delayAppointmentVM);
        }

        public IList<Tuple<Appointment, DateTime>> GetCandidatesToDelay(IList<Doctor> availableDoctors)
        {
            IList<Tuple<Appointment, DateTime>> appointmentDelayPairs = new List<Tuple<Appointment, DateTime>>();

            foreach (var doctor in availableDoctors)
            {
                IEnumerable<Appointment> appointments = _appointmentService.GetAppointmentsForDateRangeAndDoctor(DateTime.Now.AddMinutes(15), DateTime.Now.AddHours(2), doctor);
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

                Room freeRoom = _appointmentService.FindFreeRoom(appointment.Room.RoomType, potentialTime, potentialTime.AddMinutes(15));

                if (freeRoom == null)
                    continue;

                if (_appointmentService.IsDoctorAvailable(appointment.Doctor, potentialTime, potentialTime.AddMinutes(15)))
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

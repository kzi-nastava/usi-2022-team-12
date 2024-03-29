﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.OperationsAndExaminations
{
    public class DoctorReferralCreationViewModel : ViewModelBase
    {
        #region Atributes
        private readonly IDoctorService _doctorService;

        private readonly IDoctorRepository _doctorRepository;

        private readonly Patient _patient;

        private Doctor _selectedDoctor;

        private string _searchText;
        #endregion

        #region Properties
        public IDoctorService DoctorService => _doctorService;
        public Patient Patient => _patient;
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(_searchText));
            }
        }
        #endregion

        #region Collections
        private ObservableCollection<Doctor> _doctors;
        public IEnumerable<Doctor> Doctors => _doctors;

        private ObservableCollection<Referral> _referrals;
        public IEnumerable<Referral> Referrals => _referrals;
        #endregion

        #region Commands
        public ICommand IssueRefferalCommand { get; }
        public ICommand BackToExaminationCommand { get; }
        public ICommand SearchDoctorCommand { get; }
        #endregion
        public DoctorReferralCreationViewModel(IDoctorService doctorService, IDoctorRepository doctorRepository, Patient patient)
        {
            _doctorService = doctorService;
            _doctorRepository = doctorRepository;
            _patient = patient;
            UpdateData(null);
            _referrals = new ObservableCollection<Referral>();
            List<Referral> refferals = GlobalStore.ReadObject<List<Referral>>("NewReferrals");
            foreach (Referral referral in refferals)
            {
                _referrals.Add(referral);
            }
            SearchDoctorCommand = new SearchDoctorCommand(this);
            IssueRefferalCommand = new IssueRefferalCommand(this);
            BackToExaminationCommand = new BackToExaminationCommandFromReferral(this);

        }

        public void UpdateData(string? prefix)
        {
            _doctors = new ObservableCollection<Doctor>();
            IEnumerable<Doctor> doctors;
            if (string.IsNullOrEmpty(prefix))
            {
                doctors = _doctorRepository.ReadAll();
            }
            else
            {
                doctors = _doctorService.FilterDoctorsBySearchText(prefix);
            }
            foreach (var doctor in doctors)
            {
                _doctors.Add(doctor);
            }
            OnPropertyChanged(nameof(Doctors));
        }

        public void addReferral(Referral referral)
        {
            _referrals.Add(referral);
            OnPropertyChanged(nameof(Referrals));
        }
    }
}

﻿using System.Windows.Input;
using HealthInstitution.Core.Features.ApplicationAccess.Commands;
using HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.MedicineManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.SurveyManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Ninject;
using HealthInstitution.GUI.Features.EquipmentManagement;
using HealthInstitution.GUI.Features.MedicineManagement;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Features.SurveyManagement;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.Navigation
{
    public class ManagerHomeViewModel : NavigableViewModel
    {
        #region Properties
        public string ManagerName
        {
            get => GlobalStore.ReadObject<Manager>("LoggedUser").FirstName;
        }
        #endregion

        #region Commands

        public ICommand LogOutCommand { get; set; }
        public ICommand? RoomsOverviewCommand { get; set; }
        public ICommand? EquipmentOverviewCommand { get; set; }
        public ICommand? ArrangeEquipmentCommand { get; set; }
        public ICommand? RoomRenovationCommand { get; set; }
        public ICommand? MedicineOverviewCommand { get; set; }
        public ICommand? IngredientOverviewCommand { get; set; }
        public ICommand? SurveyOverviewCommand { get; set; }

        #endregion

        #region Handlers

        private void RegisterHandler()
        {
            EventBus.RegisterHandler("RoomsOverview", () =>
            {
                RoomsCRUDViewModel Rcvm = ServiceLocator.Get<RoomsCRUDViewModel>();
                SwitchCurrentViewModel(Rcvm);
            });

            EventBus.RegisterHandler("RoomEquipment", () =>
            {
                RoomEquipmentViewModel Revm = ServiceLocator.Get<RoomEquipmentViewModel>();
                SwitchCurrentViewModel(Revm);
            });

            EventBus.RegisterHandler("CreateRoom", () =>
            {
                RoomCreateViewModel Rcvm = ServiceLocator.Get<RoomCreateViewModel>();
                SwitchCurrentViewModel(Rcvm);
            });

            EventBus.RegisterHandler("OpenUpdateRoom", () =>
            {
                RoomUpdateViewModel Ruvm = ServiceLocator.Get<RoomUpdateViewModel>();
                SwitchCurrentViewModel(Ruvm);
            });

            EventBus.RegisterHandler("EquipmentOverview", () =>
            {
                EquipmentOverviewViewModel Eovm = ServiceLocator.Get<EquipmentOverviewViewModel>();
                SwitchCurrentViewModel(Eovm);
            });

            EventBus.RegisterHandler("ArrangeEquipment", () =>
            {
                RoomChoiceViewModel Rcvm = ServiceLocator.Get<RoomChoiceViewModel>();
                SwitchCurrentViewModel(Rcvm);
            });

            EventBus.RegisterHandler("RoomsConfirmed", () =>
            {
                ArrangeEquipmentViewModel Aevm = ServiceLocator.Get<ArrangeEquipmentViewModel>();
                SwitchCurrentViewModel(Aevm);
            });

            EventBus.RegisterHandler("RoomRenovation", () =>
            {
                RoomRenovationViewModel Rrvm = ServiceLocator.Get<RoomRenovationViewModel>();
                SwitchCurrentViewModel(Rrvm);
            });

            EventBus.RegisterHandler("MedicineOverview", () =>
            {
                MedicineOverviewViewModel Movm = ServiceLocator.Get<MedicineOverviewViewModel>();
                SwitchCurrentViewModel(Movm);
            });

            EventBus.RegisterHandler("IngredientOverview", () =>
            {
                IngredientOverviewViewModel Iovm = ServiceLocator.Get<IngredientOverviewViewModel>();
                SwitchCurrentViewModel(Iovm);
            });

            EventBus.RegisterHandler("SurveyOverview", () =>
            {
                SurveyOverviewViewModel Sovm = ServiceLocator.Get<SurveyOverviewViewModel>();
                SwitchCurrentViewModel(Sovm);
            });

            EventBus.RegisterHandler("DoctorSurveyOverview", () =>
            {
                DoctorSurveyOverviewViewModel Dsovm = ServiceLocator.Get<DoctorSurveyOverviewViewModel>();
                SwitchCurrentViewModel(Dsovm);
            });

        }

        #endregion

        public ManagerHomeViewModel()
        {
            RoomsOverviewCommand = new RoomsOverviewCommand();
            EquipmentOverviewCommand = new EquipmentOverviewCommand();
            ArrangeEquipmentCommand = new ArrangeEquipmentCommand();
            RoomRenovationCommand = new RoomRenovationCommand();
            MedicineOverviewCommand = new MedicineOverviewCommand();
            IngredientOverviewCommand = new IngredientOverviewCommand();
            SurveyOverviewCommand = new SurveyOverviewCommand();
            LogOutCommand = new LogOutCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<RoomsCRUDViewModel>());
            RegisterHandler();
        }
    }
}

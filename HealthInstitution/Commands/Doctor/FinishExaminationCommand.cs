using HealthInstitution.Dialogs.Custom.Doctor;
using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthInstitution.Commands
{
    public class FinishExaminationCommand : CommandBase
    {
        private ExaminationViewModel _viewModel;
        public FinishExaminationCommand(ExaminationViewModel viewModel)
        {
            _viewModel = viewModel;

        }
        public override void Execute(object? parameter)
        {
            if((bool)!OpenDynamicEqupmentDialog())
                return;
            UpdateMedicalRecord();
            UpdateAppointment();
            CreateReferrals();
            EventBus.FireEvent("DoctorSchedule");
        }

        private bool? OpenDynamicEqupmentDialog()
        {
            DynamicEquipmentUpdateViewModel dynamicEquipmentUpdateViewModel = new DynamicEquipmentUpdateViewModel(_viewModel.Appointment.Room, _viewModel.EntryService);
            bool? result = _viewModel.DialogService.OpenDialog(dynamicEquipmentUpdateViewModel);
            return result;
        }
        private void UpdateAppointment()
        {
            Appointment appointment = _viewModel.Appointment;
            appointment.Anamnesis = _viewModel.Anamnesis;
            appointment.IsDone = true;

            List<PrescribedMedicine> prescriptions = GlobalStore.ReadObject<List<PrescribedMedicine>>("Prescription");
            List<PrescribedMedicine> prescribedMedicines = new List<PrescribedMedicine>();
            foreach (PrescribedMedicine medicine in prescriptions)
            {
                PrescribedMedicine prescribedMedicine = _viewModel.PrescribedMedicineService.Create(medicine);
                _viewModel.MedicalRecord.PrescribedMedicines.Add(prescribedMedicine);
                prescribedMedicines.Add(prescribedMedicine);
            }
            appointment.PrescribedMedicines = prescribedMedicines;
            _viewModel.AppointmentService.Update(appointment);
            _viewModel.MedicalRecordService.Update(_viewModel.MedicalRecord);
        }
        private void UpdateMedicalRecord()
        {
            _viewModel.UpdatedMedicalRecord.Allergens = (IList<Allergen>)_viewModel.Allergens;
            _viewModel.UpdatedMedicalRecord.IllnessHistory = (IList<Illness>)_viewModel.IllnessHistoryData;
            _viewModel.MedicalRecordService.Update(_viewModel.UpdatedMedicalRecord);
            List<Allergen> updatedAllergenList = new List<Allergen>();
            foreach (var allergen in _viewModel.MedicalRecord.Allergens)
            {
                updatedAllergenList.Add(allergen);
            }
            foreach (var allergen in _viewModel.NewAllergens)
            {
                Allergen newAllergen = _viewModel.AllergenService.Create(allergen);
                updatedAllergenList.Add(newAllergen);
            }
            List<Illness> updatedIllnessList = new List<Illness>();
            foreach (var illness in _viewModel.MedicalRecord.IllnessHistory)
            {
                updatedIllnessList.Add(illness);
            }
            foreach (var illness in _viewModel.NewIllnesses)
            {
                Illness newIllness = _viewModel.IllnessService.Create(illness);
                updatedIllnessList.Add(newIllness);
            }
            MedicalRecord medicalRecord = _viewModel.MedicalRecord;
            medicalRecord.Allergens = updatedAllergenList;
            medicalRecord.IllnessHistory = updatedIllnessList;
            medicalRecord.Height = _viewModel.UpdatedMedicalRecord.Height;
            medicalRecord.Weight = _viewModel.UpdatedMedicalRecord.Weight;
            _viewModel.MedicalRecordService.Update(medicalRecord);
        }

        private void CreateReferrals()
        {
            List<Referral> referrals = GlobalStore.ReadObject<List<Referral>>("NewReferrals");
            foreach (Referral referral in referrals)
            {
                _viewModel.ReferralService.Create(referral);
            }

        }
    }
}

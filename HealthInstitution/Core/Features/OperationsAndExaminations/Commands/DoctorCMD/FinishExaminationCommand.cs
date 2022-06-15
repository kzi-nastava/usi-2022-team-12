using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.EquipmentManagement;
using HealthInstitution.GUI.Features.OperationsAndExaminations;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
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
            if ((bool)!OpenDynamicEqupmentDialog())
                return;
            UpdateMedicalRecord();
            UpdateAppointment();
            CreateReferrals();
            EventBus.FireEvent("DoctorSchedule");
        }

        private bool? OpenDynamicEqupmentDialog()
        {
            DynamicEquipmentUpdateViewModel dynamicEquipmentUpdateViewModel = new DynamicEquipmentUpdateViewModel(_viewModel.Appointment.Room, _viewModel.EntryRepository);
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
                PrescribedMedicine prescribedMedicine = _viewModel.PrescribedMedicineRepository.Create(medicine);
                _viewModel.MedicalRecord.PrescribedMedicines.Add(prescribedMedicine);
                prescribedMedicines.Add(prescribedMedicine);
            }
            appointment.PrescribedMedicines = prescribedMedicines;
            _viewModel.SchedulingService.Update(appointment);
            _viewModel.MedicalRecordRepository.Update(_viewModel.MedicalRecord);
        }
        private void UpdateMedicalRecord()
        {
            _viewModel.UpdatedMedicalRecord.Allergens = (IList<Allergen>)_viewModel.Allergens;
            _viewModel.UpdatedMedicalRecord.IllnessHistory = (IList<Illness>)_viewModel.IllnessHistoryData;
            _viewModel.MedicalRecordRepository.Update(_viewModel.UpdatedMedicalRecord);
            List<Allergen> updatedAllergenList = new List<Allergen>();
            foreach (var allergen in _viewModel.MedicalRecord.Allergens)
            {
                updatedAllergenList.Add(allergen);
            }
            foreach (var allergen in _viewModel.NewAllergens)
            {
                Allergen newAllergen = _viewModel.AllergenRepository.Create(allergen);
                updatedAllergenList.Add(newAllergen);
            }
            List<Illness> updatedIllnessList = new List<Illness>();
            foreach (var illness in _viewModel.MedicalRecord.IllnessHistory)
            {
                updatedIllnessList.Add(illness);
            }
            foreach (var illness in _viewModel.NewIllnesses)
            {
                Illness newIllness = _viewModel.IllnessRepository.Create(illness);
                updatedIllnessList.Add(newIllness);
            }
            MedicalRecord medicalRecord = _viewModel.MedicalRecord;
            medicalRecord.Allergens = updatedAllergenList;
            medicalRecord.IllnessHistory = updatedIllnessList;
            medicalRecord.Height = _viewModel.UpdatedMedicalRecord.Height;
            medicalRecord.Weight = _viewModel.UpdatedMedicalRecord.Weight;
            _viewModel.MedicalRecordRepository.Update(medicalRecord);
        }

        private void CreateReferrals()
        {
            List<Referral> referrals = GlobalStore.ReadObject<List<Referral>>("NewReferrals");
            foreach (Referral referral in referrals)
            {
                _viewModel.ReferralRepository.Create(referral);
            }

        }
    }
}

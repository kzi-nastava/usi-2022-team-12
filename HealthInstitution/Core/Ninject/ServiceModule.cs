using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Services;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Features.NotificationManagement.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Repository;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Implementation;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.Core.Utility;
using HealthInstitution.GUI.Features.Navigation;
using HealthInstitution.GUI.Utility.Dialog.Service;
using Ninject.Modules;

namespace HealthInstitution.Core.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICrudService<>)).To(typeof(CrudService<>));
            Bind(typeof(IUserService<>)).To(typeof(UserService<>));
            Bind(typeof(IPatientService)).To(typeof(PatientService));
            Bind(typeof(IDoctorService)).To(typeof(DoctorService));
            Bind(typeof(IRoomService)).To(typeof(RoomService));
            Bind(typeof(IEquipmentService)).To(typeof(EquipmentService));
            Bind(typeof(IEquipmentPurchaseRequestService)).To(typeof(EquipmentPurchaseRequestService));
            Bind(typeof(IAppointmentService)).To(typeof(AppointmentService));
            Bind(typeof(IAppointmentRequestService<>)).To(typeof(AppointmentRequestService<>));
            Bind(typeof(IAppointmentUpdateRequestService)).To(typeof(AppointmentUpdateRequestService));
            Bind(typeof(IAppointmentDeleteRequestService)).To(typeof(AppointmentDeleteRequestService));
            Bind(typeof(IActivityService)).To(typeof(ActivityService));
            Bind(typeof(IEntryService)).To(typeof(EntryService));
            Bind(typeof(IIllnessService)).To(typeof(IllnessService));
            Bind(typeof(IAllergenService)).To(typeof(AllergenService));
            Bind(typeof(IMedicalRecordService)).To(typeof(MedicalRecordService));
            Bind(typeof(IRoomRenovationService)).To(typeof(RoomRenovationService));
            Bind(typeof(IReferralService)).To(typeof(ReferralService));
            Bind(typeof(IMedicineService)).To(typeof(MedicineService));
            Bind(typeof(IPrescribedMedicineService)).To(typeof(PrescribedMedicineService));
            Bind(typeof(INotificationService)).To(typeof(NotificationService));
            Bind(typeof(IIngredientService)).To(typeof(IngredientService));
            Bind(typeof(IOffDaysRequestService)).To(typeof(OffDaysRequestService));
            Bind(typeof(IMedicineReviewService)).To(typeof(MedicineReviewService));
            Bind(typeof(IDoctorSurveyService)).To(typeof(DoctorSurveyService));
            Bind(typeof(IHealthInstitutionSurveyService)).To(typeof(HealthInstitutionSurveyService));
            Bind(typeof(IDialogService)).To(typeof(DialogService));
            Bind(typeof(IPrescribedMedicineNotificationService)).To(typeof(PrescribedMedicineNotificationService));

            Bind(typeof(ICrudRepository<>)).To(typeof(CrudRepository<>));
            Bind(typeof(IUserRepository<>)).To(typeof(UserRepository<>));
            Bind(typeof(IPatientRepository)).To(typeof(PatientRepository));
            Bind(typeof(IDoctorRepository)).To(typeof(DoctorRepository));
            Bind(typeof(IRoomRepository)).To(typeof(RoomRepository));
            Bind(typeof(IEquipmentRepository)).To(typeof(EquipmentRepository));
            Bind(typeof(IEquipmentPurchaseRequestRepository)).To(typeof(EquipmentPurchaseRequestRepository));
            Bind(typeof(IAppointmentRepository)).To(typeof(AppointmentRepository));
            Bind(typeof(IAppointmentRequestRepository<>)).To(typeof(AppointmentRequestRepository<>));
            Bind(typeof(IAppointmentUpdateRequestRepository)).To(typeof(AppointmentUpdateRequestRepository));
            Bind(typeof(IAppointmentDeleteRequestRepository)).To(typeof(AppointmentDeleteRequestRepository));
            Bind(typeof(IActivityRepository)).To(typeof(ActivityRepository));
            Bind(typeof(IEntryRepository)).To(typeof(EntryRepository));
            Bind(typeof(IIllnessRepository)).To(typeof(IllnessRepository));
            Bind(typeof(IAllergenRepository)).To(typeof(AllergenRepository));
            Bind(typeof(IMedicalRecordRepository)).To(typeof(MedicalRecordRepository));
            Bind(typeof(IRoomRenovationRepository)).To(typeof(RoomRenovationRepository));
            Bind(typeof(IReferralRepository)).To(typeof(ReferralRepository));
            Bind(typeof(IMedicineRepository)).To(typeof(MedicineRepository));
            Bind(typeof(IPrescribedMedicineRepository)).To(typeof(PrescribedMedicineRepository));
            Bind(typeof(IUserNotificationRepository)).To(typeof(UserNotificationRepository));
            Bind(typeof(IIngredientRepository)).To(typeof(IngredientRepository));
            Bind(typeof(IOffDaysRequestRepository)).To(typeof(OffDaysRequestRepository));
            Bind(typeof(IMedicineReviewRepository)).To(typeof(MedicineReviewRepository));
            Bind(typeof(IDoctorSurveyRepository)).To(typeof(DoctorSurveyRepository));
            Bind(typeof(IHealthInstitutionSurveyRepository)).To(typeof(HealthInstitutionSurveyRepository));
            Bind(typeof(IPrescribedMedicineNotificationRepository)).To(typeof(PrescribedMedicineNotificationRepository));

            Bind<DatabaseContext>().To<DatabaseContext>().InSingletonScope().WithConstructorArgument(0);
            Bind<LoginViewModel>().To<LoginViewModel>();
        }
    }
}

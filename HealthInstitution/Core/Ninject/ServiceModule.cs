using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.EquipmentManagement.Service;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.MedicalRecordManagement.Service;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Service;
using HealthInstitution.Core.Features.NotificationManagement.Repository;
using HealthInstitution.Core.Features.NotificationManagement.Service;
using HealthInstitution.Core.Features.OffDaysManagement.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Service;
using HealthInstitution.Core.Features.OperationsAndExaminations.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Services;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Services;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Persistence;
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
            Bind(typeof(IDialogService)).To(typeof(DialogService));

            Bind(typeof(ICrudRepository<>)).To(typeof(CrudRepository<>));
            Bind(typeof(IUserRepository<>)).To(typeof(UserRepository<>));
            Bind(typeof(IPatientRepository)).To(typeof(PatientRepository));
            Bind(typeof(IDoctorRepository)).To(typeof(DoctorRepository));
            Bind(typeof(IRoomRepository)).To(typeof(RoomRepository));
            Bind(typeof(IEquipmentRepository)).To(typeof(EquipmentRepository));
            Bind(typeof(IEquipmentPurchaseRequestRepository)).To(typeof(EquipmentPurchaseRequestRepository));
            Bind(typeof(IAppointmentRepository)).To(typeof(AppointmentRepository));
            Bind(typeof(IAppointmentRequestRepository)).To(typeof(AppointmentRequestRepository));
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

            Bind(typeof(IAppointmentRequestService)).To(typeof(AppointmentRequestService));
            Bind(typeof(IRecommendationService)).To(typeof(RecommendationService));
            Bind(typeof(ISchedulingService)).To(typeof(SchedulingService));
            Bind(typeof(IEquipmentService)).To(typeof(EquipmentService));
            Bind(typeof(IEquipmentPurchaseRequestService)).To(typeof(EquipmentPurchaseRequestService));
            Bind(typeof(IMedicineService)).To(typeof(MedicineService));
            Bind(typeof(IPrescribedMedicineNotificationService)).To(typeof(PrescribedMedicineNotificationService));
            Bind(typeof(IOffDaysService)).To(typeof(OffDaysService));
            Bind(typeof(IRoomService)).To(typeof(RoomService));
            Bind(typeof(IDoctorSurveyService)).To(typeof(DoctorSurveyService));
            Bind(typeof(IHealthInstitutionSurveyService)).To(typeof(HealthInstitutionSurveyService));
            Bind(typeof(IDoctorService)).To(typeof(DoctorService));
            Bind(typeof(IPatientService)).To(typeof(PatientService));
            Bind(typeof(IActivityService)).To(typeof(ActivityService));
            Bind(typeof(IReferralService)).To(typeof(ReferralService));
            Bind(typeof(IAppointmentService)).To(typeof(AppointmentService));
            Bind(typeof(IUserNotificationService)).To(typeof(UserNotificationService)).WithConstructorArgument(new UserNotificationRepository(new DatabaseContext(0)));
            Bind(typeof(IUserService)).To(typeof(UserService));
            Bind(typeof(IMedicalRecordService)).To(typeof(MedicalRecordService));
 

            Bind<DatabaseContext>().To<DatabaseContext>().InSingletonScope().WithConstructorArgument(0);
            Bind<LoginViewModel>().To<LoginViewModel>();
        }
    }
}

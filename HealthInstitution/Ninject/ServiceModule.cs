using HealthInstitution.Dialogs.Service;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Implementation;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.ViewModel;
using Ninject.Modules;

namespace HealthInstitution.Ninject
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
            //Bind(typeof(IDoctorMarkService)).To(typeof(DoctorMarkService));
            Bind(typeof(IMedicineReviewService)).To(typeof(MedicineReviewService));

            Bind(typeof(IDoctorSurveyService)).To(typeof(DoctorSurveyService));
            Bind(typeof(IHealthInstitutionSurveyService)).To(typeof(HealthInstitutionSurveyService));
            Bind(typeof(IDialogService)).To(typeof(DialogService));
            Bind(typeof(IPrescribedMedicineNotificationService)).To(typeof(PrescribedMedicineNotificationService));

            Bind<DatabaseContext>().To<DatabaseContext>().InSingletonScope().WithConstructorArgument(0);

            Bind<LoginViewModel>().To<LoginViewModel>();
        }
    }
}

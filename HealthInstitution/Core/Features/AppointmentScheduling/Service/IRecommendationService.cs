using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public interface IRecommendationService
    {
        Appointment FindFreeAppointmentForDoctorInTimeSpan(Patient patient, List<Doctor> doctors, DateTime intervalStart, DateTime intervalEnd);
        Appointment FindFreeAppointmentForDoctors(Patient patient, List<Doctor> doctors, DateOnly deadline);
        Appointment FindFreeAppointmentForDoctorsInTimeInterval(Patient patient, List<Doctor> doctors, TimeOnly intervalStart, TimeOnly intervalEnd, DateOnly deadline);
        Appointment FindFreeAppointmentInTimeInterval(Patient patient, DoctorSpecialization specialization, TimeOnly intervalStart, TimeOnly intervalEnd, DateOnly deadline);
        List<Appointment> FindFreeAppointmentsForDoctorSpecialization(Patient patient, DoctorSpecialization specialization, DateOnly deadline, int expectedNumber);
        List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, TimeOnly startTime, TimeOnly endTime, DateOnly deadline, string priority);
    }
}
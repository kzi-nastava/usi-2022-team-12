using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        #region CRUD methods

        public IEnumerable<Appointment> ReadAll()
        {
            return _appointmentRepository.ReadAll();
        }

        public Appointment Read(Guid appointmentId)
        {
            return _appointmentRepository.Read(appointmentId);
        }

        public Appointment Create(Appointment newAppointment)
        {
            return _appointmentRepository.Create(newAppointment);
        }

        public Appointment Update(Appointment appointmentToUpdate)
        {
            return _appointmentRepository.Update(appointmentToUpdate);
        }

        public Appointment Delete(Guid appointmentId)
        {
            return _appointmentRepository.Delete(appointmentId);
        }

        #endregion
    }
}

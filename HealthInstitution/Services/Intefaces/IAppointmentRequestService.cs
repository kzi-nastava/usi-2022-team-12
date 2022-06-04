﻿using HealthInstitution.Model;
using System.Collections.Generic;
using HealthInstitution.Model.patient;

namespace HealthInstitution.Services.Intefaces
{
    public interface IAppointmentRequestService<T> : ICrudService<T> where T : AppointmentRequest
    {
        public IEnumerable<T> ReadAllPendingRequests();

        public IEnumerable<T> FilterPendingRequestsBySearchText(string searchText);
    }
}

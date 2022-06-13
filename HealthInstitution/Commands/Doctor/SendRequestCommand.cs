using HealthInstitution.Model;
using HealthInstitution.Model.doctor;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel.doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands.doctor
{
    public class SendRequestCommand : CommandBase
    {
        private CreateOffDayRequestViewModel _viewModel;
        public SendRequestCommand(CreateOffDayRequestViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            DateTime startDate = _viewModel.StartDate;
            DateTime endDate = _viewModel.EndDate;
            if(endDate.Date < startDate.Date)
            {
                MessageBox.Show("End date cannot be before start date");
                return;
            }
            OffDaysRequest odr = new OffDaysRequest
            {
                Doctor = _viewModel.Doctor,
                IsUrgent = _viewModel.IsUrgent,
                Reason = _viewModel.Reason,
                RefuseComment = null,
                StartDate = startDate,
                EndDate = endDate,
                Status = Status.Pending
            };
            if (IsOverlapping(odr))
            {
                MessageBox.Show("Request already sent for chosen dates");
                return;
            }
            if (_viewModel.IsUrgent)
            {
                odr.Status = Status.Approved;
            }
            _viewModel.OffDaysRequestService.Create(odr);
            if (_viewModel.IsUrgent)
            {
                MessageBox.Show("Your off days are approved");
            }
            else
            {
                MessageBox.Show("A request for off days has been sent");
            }
            EventBus.FireEvent("DoctorOffDays");
        }

        bool IsOverlapping(OffDaysRequest newRequest)
        {
            return !(_viewModel.OffDaysRequestService.ReadAll().Where(e => e.Doctor == newRequest.Doctor)
                .Count(e => e.StartDate <= newRequest.EndDate && newRequest.StartDate <= e.EndDate) == 0);
        }

    }
}

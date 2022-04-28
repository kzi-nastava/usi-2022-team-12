using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class DeleteRoomCommand : CommandBase
    {

        private Room _selectedRoom;
        private IAppointmentService _appointmentService;
        private IRoomService _roomService;

        public DeleteRoomCommand(IAppointmentService appointmentService, IRoomService roomService)
        {
            _appointmentService = appointmentService;
            _roomService = roomService;
        }

        public override void Execute(object? parameter)
        {
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            var apts = _appointmentService.ReadRoomAppointments(_selectedRoom);
            if (apts.Count() == 0)
            {
                _roomService.Delete(_selectedRoom.Id);
                MessageBox.Show("Room deleted successfully!");
            }
            else 
            {
                MessageBox.Show("Room with that name has appointments!");
            }

        }
    }
}
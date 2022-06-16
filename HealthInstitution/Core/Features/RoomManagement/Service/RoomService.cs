using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;

namespace HealthInstitution.Core.Features.RoomManagement.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IEquipmentService _equipmentService;
        private readonly IRoomRenovationRepository _roomRenovationRepository;
        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public RoomService(IRoomRepository roomRepository, IEquipmentService equipmentService, IRoomRenovationRepository roomRenovationRepository, IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository, IAppointmentRepository appointmentRepository)
        {
            _roomRepository = roomRepository;
            _equipmentService = equipmentService;
            _roomRenovationRepository = roomRenovationRepository;
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
            _appointmentRepository = appointmentRepository;
        }

        #region CRUD methods

        public IEnumerable<Room> ReadAll()
        {
            return _roomRepository.ReadAll();
        }

        public Room Read(Guid roomId)
        {
            return _roomRepository.Read(roomId);
        }

        public Room Create(Room newRoom)
        {
            return _roomRepository.Create(newRoom);
        }

        public Room Update(Room roomToUpdate)
        {
            return _roomRepository.Update(roomToUpdate);
        }

        public Room Delete(Guid roomId)
        {
            return _roomRepository.Delete(roomId);
        }

        #endregion

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt.Room.Id == room.Id && apt.StartDate < toDate && fromDate < apt.EndDate) == 0;
        }

        public bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt.Room.Id == room.Id && apt != aptToUpdate && apt.StartDate < toDate && fromDate < apt.EndDate) == 0;
        }

        public Room FindFreeRoom(RoomType roomType, DateTime start, DateTime end)
        {
            var examinationRooms = ReadRooms(roomType);
            foreach (var room in examinationRooms)
            {
                if (IsRoomAvailable(room, start, end) && _appointmentUpdateRequestRepository.IsRoomAvailable(room, start, end)
                    && _roomRenovationRepository.IsRoomNotRenovating(room, start, end))
                {
                    return room;
                }
            }
            return null;
        }

        public IEnumerable<Room> ReadRooms(RoomType rt)
        {
            return _roomRepository.ReadAll().Where(room => room.RoomType == rt)
                            .ToList();
        }

        public IEnumerable<Room> ReadRooms(List<RoomType> types)
        {
            return _roomRepository.ReadAll().Where(room => types.Contains(room.RoomType))
                            .ToList();
        }

        public IEnumerable<Room> ReadRoomsWithName(string name)
        {
            return _roomRepository.ReadAll().Where(room => room.Name == name).ToList();
        }


        public IEnumerable<Room> FilterRoomsLowOnEquipment(string searchText)
        {
            searchText = searchText.ToLower();
            return GetRoomsLowOnEquipment().Where(room => room.Name.ToLower().Contains(searchText) || room.RoomType.ToString().ToLower().Contains(searchText));
        }

        public IEnumerable<Room> GetRoomsLowOnEquipment()
        {
            var allRooms = _roomRepository.ReadAll();
            var priorityRooms = new List<Room>();
            var requiredEquipment = _equipmentService.GetEquipment(EquipmentType.DynamicEquipment);

            foreach (var room in allRooms)
            {
                if (IsRoomMissingEquipment(room, requiredEquipment))
                    priorityRooms.Add(room);
                else if (room.IsLowOnEquipment())
                    priorityRooms.Add(room);
            }

            return priorityRooms;
        }

        private bool IsRoomMissingEquipment(Room room, IEnumerable<Equipment> requiredEquipment)
        {
            var missingEquipment = room.GetMissingEquipment(new List<Equipment>(requiredEquipment));
            return missingEquipment.Count != 0;
        }

        public Room GetStorage()
        {
            return _roomRepository.ReadAll().FirstOrDefault(rm => rm.Name == "S");
        }

        public void AddItemQuantityToStorage(Entry<Equipment> deliveredEquipment)
        {
            var storage = GetStorage();
            AddItemQuantity(storage, deliveredEquipment);
        }

        public void AddItemQuantity(Room room, Entry<Equipment> deliveredEquipment)
        {
            IncreaseItemQuantity(room, deliveredEquipment);
            _roomRepository.Update(room);
        }

        public void IncreaseItemQuantity(Room room, Entry<Equipment> deliveredEquipment)
        {
            var entryToUpdate = room.Inventory.FirstOrDefault(e => e.Item.Id == deliveredEquipment.Item.Id);
            if (entryToUpdate != null)
                entryToUpdate.Quantity += deliveredEquipment.Quantity;
            else
                room.Inventory.Add(deliveredEquipment);
        }
    }
}

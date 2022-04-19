using System;

namespace HealthInstitution.Model
{
    public class RoomRenovation : BaseObservableEntity
    {
        #region Attributes

        private Room _renovatedRoom;
        public virtual Room RenovatedRoom { get => _renovatedRoom; set => OnPropertyChanged(ref _renovatedRoom, value); }

        private DateTime _startTime;
        public DateTime StartTime { get => _startTime; set => OnPropertyChanged(ref _startTime, value); }

        private DateTime _endTime;
        public DateTime EndTime { get => _endTime; set => OnPropertyChanged(ref _endTime, value); }

        #endregion

        #region Constructor

        public RoomRenovation() { }
        public RoomRenovation(Room renovatedRoom, DateTime startTime, DateTime endTime)
        {
            _renovatedRoom = renovatedRoom;
            _startTime = startTime;
            _endTime = endTime;
        }

        #endregion

    }
}

using System;

namespace HealthInstitution.Model.room
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

        private bool? _advancedDivide;
        public bool? AdvancedDivide { get => _advancedDivide; set => OnPropertyChanged(ref _advancedDivide, value); }

        private string? _renovatedSmallRoom1Name;
        public virtual string? RenovatedSmallRoom1Name { get => _renovatedSmallRoom1Name; set => OnPropertyChanged(ref _renovatedSmallRoom1Name, value); }

        private string? _renovatedSmallRoom2Name;
        public virtual string? RenovatedSmallRoom2Name { get => _renovatedSmallRoom2Name; set => OnPropertyChanged(ref _renovatedSmallRoom2Name, value); }

        #endregion

        #region Constructor

        public RoomRenovation() { }

        public RoomRenovation(Room renovatedRoom, DateTime startTime, DateTime endTime)
        {
            _renovatedRoom = renovatedRoom;
            _startTime = startTime;
            _endTime = endTime;
            _advancedDivide = null;
            _renovatedSmallRoom1Name = null;
            _renovatedSmallRoom2Name = null;
        }
        public RoomRenovation(Room renovatedRoom, DateTime startTime, DateTime endTime, bool divide, string smallRoom1Name, string smallRoom2Name)
        {
            _renovatedRoom = renovatedRoom;
            _startTime = startTime;
            _endTime = endTime;
            _advancedDivide = divide;
            _renovatedSmallRoom1Name = smallRoom1Name;
            _renovatedSmallRoom2Name = smallRoom2Name;
        }

        #endregion

    }
}

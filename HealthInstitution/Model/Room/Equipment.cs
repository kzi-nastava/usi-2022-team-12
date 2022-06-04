namespace HealthInstitution.Model.room
{
    public class Equipment : BaseObservableEntity
    {
        #region Attributes

        private EquipmentType _equipmentType;
        public EquipmentType EquipmentType { get { return _equipmentType; } set { OnPropertyChanged(ref _equipmentType, value); } }

        private string _name;
        public string Name { get { return _name; } set { OnPropertyChanged(ref _name, value); } }

        #endregion

        #region Constructor

        public Equipment() { }

        public Equipment(EquipmentType equipmentType, string name)
        {
            _equipmentType = equipmentType;
            _name = name;
        }

        #endregion
    }
}

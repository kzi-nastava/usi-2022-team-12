using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class PrescribedMedicine : BaseObservableEntity
    {
        #region Attributes

        private Medicine _medicine;
        public Medicine Medicine { get => _medicine; set => OnPropertyChanged(ref _medicine, value);}

        private string? _instruction;
        public string? Instruction { get => _instruction; set => OnPropertyChanged(ref _instruction, value); }

        private string _dailyUsage;
        public string DailyUsage { get => _dailyUsage; set => OnPropertyChanged(ref _dailyUsage, value); }

        #endregion

        #region Constructor

        public PrescribedMedicine()
        {

        }

        #endregion
    }
}

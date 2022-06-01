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
        public virtual Medicine Medicine { get => _medicine; set => OnPropertyChanged(ref _medicine, value);}

        private string? _instruction;
        public string? Instruction { get => _instruction; set => OnPropertyChanged(ref _instruction, value); }

        private DateTime? _usageStart;
        public DateTime? UsageStart { get => _usageStart; set => OnPropertyChanged(ref _usageStart, value); }

        private DateTime? _usageEnd;
        public DateTime? UsageEnd { get => _usageEnd; set => OnPropertyChanged(ref _usageEnd, value); }

        private int? _usageSpan;
        public int? UsageHourSpan { get => _usageSpan; set => OnPropertyChanged(ref _usageSpan, value); }

        #endregion

        #region Constructor

        public PrescribedMedicine()
        {

        }

        #endregion
    }
}

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

        private string? _usage;
        public string? Usage { get => _usage; set => OnPropertyChanged(ref _usage, value); }

        #endregion

        #region Constructor

        public PrescribedMedicine()
        {

        }

        #endregion
    }
}

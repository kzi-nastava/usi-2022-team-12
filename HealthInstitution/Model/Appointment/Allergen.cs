using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class Allergen : BaseObservableEntity
    {
        #region Attributes

        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        #endregion

        #region Constructor

        public Allergen(string name)
        {
            Name = name;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.GUI.Utility.Title
{
    public static class TitleManager
    {
        private static string? _title;

        public static string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnTitleChanged();
            }
        }

        public static event Action? TitleChanged;

        public static void OnTitleChanged()
        {
            TitleChanged?.Invoke();
        }
    }
}

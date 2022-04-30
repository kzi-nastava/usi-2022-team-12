using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthInstitution.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentView.xaml
    /// </summary>
    public partial class AppointmentUpdateView : UserControl
    {
        public AppointmentUpdateView()
        {
            InitializeComponent();
        }

        private void HoursValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text, 0, 23);
        }

        public static bool IsValid(string str, int lowerBound, int upperBound)
        {
            int i;
            return int.TryParse(str, out i) && i >= lowerBound && i <= upperBound;
        }

        private void MinutesValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text, 0, 59);
        }
    }
}

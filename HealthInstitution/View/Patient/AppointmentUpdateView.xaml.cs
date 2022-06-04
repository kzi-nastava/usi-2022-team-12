using System.Windows.Controls;
using System.Windows.Input;

namespace HealthInstitution.View.patient
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

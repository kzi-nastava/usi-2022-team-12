using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //NavigationStore.CurrentViewModel = new LoginViewModel();
                //MainWindow = new MainWindow
                //{
                //    DataContext = new MainViewModel()
                //};
                //MainWindow.Show();
                //base.OnStartup(e);
            }
        }
    }
}

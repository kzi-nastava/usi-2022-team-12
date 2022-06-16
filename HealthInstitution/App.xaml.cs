using System.Windows;
using HealthInstitution.Core.Persistence;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (DatabaseContext db = new DatabaseContext(0))
            {
                //DatabaseContextSeed.Seed(db);
            }
        }
    }
}

using HealthInstitution.Model;
using HealthInstitution.Persistence;
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
                DatabaseContextSeed.Seed(db);

                IList<Medicine> med = db.Medicines.Where(m => m.Description == "Opis leka").ToList();

                foreach(Medicine medicine in med)
                {
                    foreach(Ingredient ingredient in medicine.Ingredients)
                    {
                        MessageBox.Show(ingredient.Name);
                    }
                }

            }
        }
    }
}

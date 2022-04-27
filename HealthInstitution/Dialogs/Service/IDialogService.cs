using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Dialogs.Service
{
    public interface IDialogService
    {
        T OpenDialog<V, T>(DialogViewModelBase<V, T> viewModel);
    }
}

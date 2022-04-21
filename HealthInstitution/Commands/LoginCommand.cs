using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel? _viewModel;

        public LoginCommand(LoginViewModel lwm) {
            _viewModel = lwm;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(_viewModel.Email) || e.PropertyName == nameof(_viewModel.Password))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.Email) && !string.IsNullOrEmpty(_viewModel.Password) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var user = _viewModel._userService.Authenticate(_viewModel.Email, _viewModel.Password);
            if (user == null)
            {
                _viewModel.ErrMsgVisibility = Visibility.Visible;
            }
            else {
                GlobalStore.AddObject("loggedUser", user);
                switch (user.Role) {
                    case Role.Manager:
                        //todo
                        break;
                    case Role.Administrator:
                        //todo
                        break;
                    case Role.Patient:
                        PatientHomeViewModel viewModel = ServiceLocator.Get<PatientHomeViewModel>();
                        NavigationStore.CurrentViewModel = viewModel;
                        break;
                    case Role.Doctor:
                        break;
                    case Role.Secretary:
                        break;
                    default:
                        MessageBox.Show("ERR");
                        return;
                }
            }
            
        }
    }
}

﻿using HealthInstitution.GUI.Utility.Dialog.Pagination;
using System;

namespace HealthInstitution.Core.Utility.Command
{
    public class UpdatePageCommand : CommandBase
    {
        public event EventHandler CanExecuteChanged;
        private readonly IPagingViewModel _vm;

        public UpdatePageCommand(IPagingViewModel vm)
        {
            _vm = vm;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int pageNumber))
            {
                _vm.UpdatePage(pageNumber);
            }
        }
    }
}

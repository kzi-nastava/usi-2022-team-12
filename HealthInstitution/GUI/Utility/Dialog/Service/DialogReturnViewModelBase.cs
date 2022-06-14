using HealthInstitution.GUI.Utility.Validation;

namespace HealthInstitution.GUI.Utility.Dialog.Service
{
    public class DialogReturnViewModelBase<T, R> : ValidationModel<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }

        private double _windowWidth;
        public double WindowWidth { get { return _windowWidth; } set { OnPropertyChanged(ref _windowWidth, value); OnPropertyChanged("ContentWidth"); } }
        private double _windowHeight;
        public double WindowHeight { get { return _windowHeight; } set { OnPropertyChanged(ref _windowHeight, value); OnPropertyChanged("ContentHeight"); } }
        public double ContentWidth { get => WindowWidth - 60; }
        public double ContentHeight { get => WindowHeight - 60; }

        public R DialogResult { get; set; }

        public DialogReturnViewModelBase(string title, string message, int windowWidht, int windowHeight) : base()
        {
            Title = title;
            Message = message;
            WindowWidth = windowWidht;
            WindowHeight = windowHeight;
        }

        public DialogReturnViewModelBase() :
            this(string.Empty, string.Empty, 360, 160)
        {
        }

        public DialogReturnViewModelBase(string title) :
            this(title, string.Empty, 360, 160)
        {
        }

        public DialogReturnViewModelBase(string title, string message) :
            this(title, message, 360, 160)
        {
        }

        public DialogReturnViewModelBase(string title, int windowWidht, int windowHeight) :
            this(title, string.Empty, windowWidht, windowHeight)
        {
        }

        public void CloseDialogWithResult(IDialogWindow dialog, R result)
        {
            DialogResult = result;

            if (dialog != null)
            {
                dialog.DialogResult = true;
            }
        }
    }
}

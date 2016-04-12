using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Koopakiller.Apps.Brainstorming.Shared.ViewModel
{
    public class MenuItemViewModel : ViewModelBase
    {
        private bool _isChecked;
        private bool _isCheckable;
        private string _header;
        private ICommand _command;

        public MenuItemViewModel(string header)
        {
            this.Header = header;
        }

        public ICommand Command
        {
            get { return this._command; }
            set
            {
                this._command = value;
                this.RaisePropertyChanged();
            }
        }

        public string Header
        {
            get { return this._header; }
            set
            {
                this._header = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsCheckable
        {
            get { return this._isCheckable; }
            set
            {
                this._isCheckable = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                this._isChecked = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<MenuItemViewModel> SubItems { get; } = new ObservableCollection<MenuItemViewModel>();
    }
}

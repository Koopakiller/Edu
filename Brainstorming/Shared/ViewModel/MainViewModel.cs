using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;
using Shared.ViewModel;

#if _SERVER
namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
#elif _CLIENT
namespace Koopakiller.Apps.Brainstorming.Client.ViewModel
#endif
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private ViewModelBase _messageViewModel;

        private readonly StartupViewModel _startupViewModel = new StartupViewModel();
        private readonly AboutViewModel _aboutViewModel = new AboutViewModel();


        public ViewModelBase CurrentViewModel
        {
            get
            {
                return this._currentViewModel;
            }
            set
            {
                if (this._currentViewModel == value) { return; }

                this._currentViewModel = value;
                this.RaisePropertyChanged();
            }
        }

        public ViewModelBase MessageViewModel
        {
            get
            {
                return this._messageViewModel;
            }
            set
            {
                if (this._messageViewModel == value) { return; }

                this._messageViewModel = value;
                this.RaisePropertyChanged();
            }
        }


        public ICommand AboutCommand { get; }

        public void ExecuteAboutCommand()
        {
            this._aboutViewModel.Close += this.AboutViewModelClose;
            this.MessageViewModel = this._aboutViewModel;
        }

        private void AboutViewModelClose(MessageViewModelBase<AboutViewModel> sender)
        {
            this._aboutViewModel.Close -= this.AboutViewModelClose;
            this.MessageViewModel = null;
        }
    }
}
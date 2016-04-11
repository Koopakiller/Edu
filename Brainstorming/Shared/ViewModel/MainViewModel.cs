using System.Windows.Input;
using GalaSoft.MvvmLight;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;

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
        private CurrentViewModelBase _currentViewModel;
        private MessageViewModelBase _messageViewModel;

        private readonly StartupViewModel _startupViewModel = new StartupViewModel();
        private readonly AboutViewModel _aboutViewModel = new AboutViewModel();

        void OnNavigationStarted(object sender, CurrentViewModelBase cvm)
        {
            this.CurrentViewModel = cvm;
        }
        void OnShowMessageStarted(object sender, MessageViewModelBase vm)
        {
            this.MessageViewModel = vm;
        }

        public CurrentViewModelBase CurrentViewModel
        {
            get
            {
                return this._currentViewModel;
            }
            set
            {
                if (this._currentViewModel == value) { return; }

                if (this._currentViewModel != null)
                {
                    this._currentViewModel.NavigationStarted -= this.OnNavigationStarted;
                    this._currentViewModel.ShowMessageStarted -= this.OnShowMessageStarted;
                }

                this._currentViewModel = value;
                if (this._currentViewModel != null)
                {
                    this._currentViewModel.NavigationStarted += this.OnNavigationStarted;
                    this._currentViewModel.ShowMessageStarted += this.OnShowMessageStarted;
                }
                this.RaisePropertyChanged();
            }
        }

        public MessageViewModelBase MessageViewModel
        {
            get
            {
                return this._messageViewModel;
            }
            set
            {
                if (this._messageViewModel == value) { return; }
                if (this._messageViewModel != null)
                {
                    this._messageViewModel.Close -= this.MessageViewModelClose;

                }
                this._messageViewModel = value; if (this._messageViewModel != null)
                {
                    this._messageViewModel.Close += this.MessageViewModelClose;
                }
                this.RaisePropertyChanged();
            }
        }


        public ICommand AboutCommand { get; }

        public void ExecuteAboutCommand()
        {
            this.MessageViewModel = this._aboutViewModel;
        }

        private void MessageViewModelClose(MessageViewModelBase sender)
        {
            this.MessageViewModel = null;
        }
    }
}
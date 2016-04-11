using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public partial class MainViewModel
    {
        public MainViewModel()
        {
            this.CurrentViewModel = this._startupViewModel;

            this.AboutCommand = new RelayCommand(this.ExecuteAboutCommand);
            
        }
    }
}

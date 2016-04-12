using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.Brainstorming.Client.ViewModel
{
    public partial class MainViewModel
    {
        public MainViewModel()
        {
            this.CurrentViewModel = this._startupViewModel;
        }
    }
}

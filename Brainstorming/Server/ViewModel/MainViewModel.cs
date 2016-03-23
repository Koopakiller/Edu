using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public partial class MainViewModel
    {
        public MainViewModel()
        {
            this.CurrentViewModel = this._startupViewModel;
            this._startupViewModel.StartServer += this.StartServer;

            this.AboutCommand = new RelayCommand(this.ExecuteAboutCommand);
            
        }
        

        private void StartServer(object sender, Model.Server e)
        {
            this.CurrentViewModel = new ReceiveDataViewModel(e) ;
        }
    }
}

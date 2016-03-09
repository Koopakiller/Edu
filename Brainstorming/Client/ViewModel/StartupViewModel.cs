using System.Net;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.Brainstorming.Shared;

namespace Koopakiller.Apps.Brainstorming.Client.ViewModel
{
    public class StartupViewModel : ViewModelBase
    {
        public StartupViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.ServerIP = new IPAddress(new byte[] { 12, 34, 56, 78 });
            }

            this.ConnectCommand = new RelayCommand(this.OnConnectToServer, ()=>!this.IsConnected);
        }

        #region Fields

        // ReSharper disable once InconsistentNaming
        private IPAddress _serverIP;
        private int _port = Constants.StandardPort;
        private bool _isConnected;

        #endregion

        #region Properties

        // ReSharper disable once InconsistentNaming
        public IPAddress ServerIP
        {
            get { return this._serverIP; }
            set
            {
                this._serverIP = value;
                this.RaisePropertyChanged();
            }
        }

        public int Port
        {
            get { return this._port; }
            set
            {
                this._port = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsConnected
        {
            get { return this._isConnected; }
            private set
            {
                this._isConnected = value;
                this.RaisePropertyChanged();
                this.ConnectCommand.RaiseCanExecuteChanged();
            }
        }

        #region Commands

        public RelayCommand ConnectCommand { get; set; }

        #endregion

        #endregion

        public void OnConnectToServer()
        {
            this.IsConnected = true;
        }
    }
}
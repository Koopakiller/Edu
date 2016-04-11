using System;
using System.Net;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.Brainstorming.Shared;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;

namespace Koopakiller.Apps.Brainstorming.Client.ViewModel
{
    public class StartupViewModel : CurrentViewModelBase
    {
        public StartupViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.ServerIP = new IPAddress(new byte[] { 12, 34, 56, 78 });
            }
            else
            {
                this.ServerIP = NetworkHelper.GetCurrentIP();
            }

            this.ConnectCommand = new RelayCommand(this.OnConnectToServer, () => !this.IsConnected);
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

        public RelayCommand ConnectCommand { get; }

        #endregion

        #endregion

        private async void OnConnectToServer()
        {
            try
            {
                this.IsConnected = true;
                var client = new Model.Client(this.ServerIP, this.Port);
            var topic=    await client.Connect();
                this.NavigateToViewModel(new SendViewModel() { Client = client , Topic=topic});
            }
            catch (Exception ex)
            {
                this.ShowMessage("Fehler beim Verbinden",
                     $"Das Verbinden mit dem Server ist fehlgeschlagen.\n\nTechnische Informationen:\n{ex.Message}");
                this.IsConnected = false;
            }
        }
    }
}
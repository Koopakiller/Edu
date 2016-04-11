using System;
using System.Net;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.Brainstorming.Shared;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public class StartupViewModel : CurrentViewModelBase
    {
        public StartupViewModel()
        {
            this.UpdateCommand = new RelayCommand(this.UpdateCurrentIP);
            this.StartServerCommand = new RelayCommand(this.OnStartServer, this.IsDataValid);
            if (this.IsInDesignMode)
            {
                this.CurrentIP = new IPAddress(new byte[] { 12, 34, 56, 78 });
            }
            else
            {
                this.UpdateCurrentIP();
            }
        }

        private bool IsDataValid()
        {
            return !string.IsNullOrWhiteSpace(this.Topic) && this.CurrentIP != null&&this.Port!=null;
        }
        // ReSharper disable once InconsistentNaming
        private void UpdateCurrentIP()
        {
            this.CurrentIP = NetworkHelper.GetCurrentIP();
        }

        private void OnStartServer()
        {
            var server = new Model.Server(this.CurrentIP, (int)this.Port) { WelcomeMessage = this.Topic };
            this.StartServer?.Invoke(this, server);

            this.NavigateToViewModel(new ReceiveDataViewModel(server) { Topic = this.Topic });
        }

        #region Fields

        // ReSharper disable once InconsistentNaming
        private IPAddress _currentIP;
        private int? _port = Constants.StandardPort;
        private string _topic;

        #endregion

        #region Properties

        // ReSharper disable once InconsistentNaming
        public IPAddress CurrentIP
        {
            get { return this._currentIP; }
            set
            {
                this._currentIP = value;
                this.RaisePropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                this.RaisePropertyChanged(nameof(this.IsOnline));
                this.StartServerCommand.RaiseCanExecuteChanged();
            }
        }

        public int? Port
        {
            get { return this._port; }
            set
            {
                this._port = value;
                this.RaisePropertyChanged();
            }
        }

        public string Topic
        {
            get { return this._topic; }
            set
            {
                this._topic = value;
                this.RaisePropertyChanged();
                this.StartServerCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsOnline => this.CurrentIP != null;

        public ICommand UpdateCommand { get; }
        public RelayCommand StartServerCommand { get; }

        #endregion

        public event EventHandler<Model.Server> StartServer;
    }
}
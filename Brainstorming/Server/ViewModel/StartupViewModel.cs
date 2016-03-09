using System.Linq;
using System.Net;
using System.Net.Sockets;
using GalaSoft.MvvmLight;
using Koopakiller.Apps.Brainstorming.Shared;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public class StartupViewModel : ViewModelBase
    {
        public StartupViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.CurrentIP = new IPAddress(new byte[] { 12, 34, 56, 78 });
            }
            else
            {
                this.UpdateCurrentIP();
            }
        }

        #region Fields

        // ReSharper disable once InconsistentNaming
        private IPAddress _currentIP;
        private int _port = Constants.StandardPort;

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

        public bool IsOnline => this.CurrentIP != null;

        #endregion

        // ReSharper disable once InconsistentNaming
        private void UpdateCurrentIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            this.CurrentIP = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public class StartupViewModel : ViewModelBase
    {
        public StartupViewModel()
        {
            if (!this.IsInDesignMode)
            {
                this.UpdateCurrentIP();
            }
        }

        // ReSharper disable once InconsistentNaming
        private IPAddress _currentIP;

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

        public bool IsOnline => this.CurrentIP != null;

        // ReSharper disable once InconsistentNaming
        private void UpdateCurrentIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            this.CurrentIP = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
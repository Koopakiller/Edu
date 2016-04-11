using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Koopakiller.Apps.Brainstorming.Shared
{
    public class NetworkHelper
    {
        // ReSharper disable once InconsistentNaming
        public static IPAddress GetCurrentIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}

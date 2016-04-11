using System.Net;
using System.Net.Sockets;

namespace Koopakiller.Apps.Brainstorming.Server.Model
{
    public class MyTcpListener : TcpListener
    {
        // ReSharper disable once InconsistentNaming
        public MyTcpListener(IPEndPoint localEP) : base(localEP) { }
        public MyTcpListener(IPAddress localaddr, int port) : base(localaddr, port) { }

        public new bool Active => base.Active;
    }
}
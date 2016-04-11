using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Koopakiller.Apps.Brainstorming.Server.Model
{
    public class Server
    {
        private MyTcpListener _server;

        // ReSharper disable once InconsistentNaming
        public IPAddress IPAddress { get; }
        public int Port { get; }

        public Server(IPAddress ip, int port)
        {
            this.IPAddress = ip;
            this.Port = port;
        }

        public string WelcomeMessage { get; set; } = string.Empty;

        public void Start(CancellationToken token)
        {
            this._server = new MyTcpListener(this.IPAddress, this.Port);
            this._server.Start();
            this.WaitForClientConnect();
        }

        private void WaitForClientConnect()
        {
            this._server.BeginAcceptTcpClient(this.OnClientConnect, this._server);
        }

        private readonly List<ClientHandler> _clientHandlers = new List<ClientHandler>();

        private void OnClientConnect(IAsyncResult asyn)
        {
            if (asyn.AsyncState == null || !((MyTcpListener)asyn.AsyncState).Active) return;

            var client = ((MyTcpListener)asyn.AsyncState).EndAcceptTcpClient(asyn);

            var x = new ClientHandler(client);
            this._clientHandlers.Add(x);
            x.WelcomeMessage = this.WelcomeMessage;
            x.DataReceived += this.x_DataReceived;
            x.StartWaitForRequest();

            this.WaitForClientConnect();
        }

        private void x_DataReceived(object sender, string e)
        {
            this.DataReceived?.Invoke(sender, e);
        }

        public event EventHandler<string> DataReceived;
    }
}

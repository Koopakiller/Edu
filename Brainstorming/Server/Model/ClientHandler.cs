using System;
using System.Net;
using System.Net.Sockets;
using Koopakiller.Apps.Brainstorming.Shared;

namespace Koopakiller.Apps.Brainstorming.Server.Model
{
    public class ClientHandler
    {
        private readonly TcpClient _clientSocket;
        private NetworkStream _networkStream;
        public ClientHandler(TcpClient clientConnected)
        {
            this._clientSocket = clientConnected;
        }
        public void StartWaitForRequest()
        {
            this._isClosed = false;
            this._networkStream = this._clientSocket.GetStream();
            var bytes = Constants.Encoding.GetBytes(this.WelcomeMessage);
            this._networkStream.Write(bytes, 0, bytes.Length);
            this.WaitForRequest();
        }

        public void WaitForRequest()
        {
            var buffer = new byte[this._clientSocket.ReceiveBufferSize];

            this._networkStream.BeginRead(buffer, 0, buffer.Length, this.ReadCallback, buffer);
        }

        private void ReadCallback(IAsyncResult result)
        {
            if (this._isClosed)
            {
                this._clientSocket.Close();
                return;
            }
            var networkStream = this._clientSocket.GetStream();

            var read = networkStream.EndRead(result);
            if (read == 0)
            {
                this._networkStream.Close();
                this._clientSocket.Close();
                return;
            }

            var buffer = (byte[])result.AsyncState;
            var data = Constants.Encoding.GetString(buffer, 0, read);

            this.DataReceived?.Invoke(((IPEndPoint)this._clientSocket.Client.RemoteEndPoint).Address, data);

            this.WaitForRequest();
        }

        public event EventHandler<string> DataReceived;

        public string WelcomeMessage { get; set; }
        private bool _isClosed = true;
        internal void Stop()
        {
            this._isClosed = true;
            this._networkStream.Close();
        }
    }
}
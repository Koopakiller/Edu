using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Koopakiller.Apps.Brainstorming.Server.Model
{
    public class Server
    {
        private const int BufferSize = 256;

        private readonly TcpListener _server;
        private readonly Encoding _encoding = Encoding.Unicode;

        public Server(IPAddress ip, int port)
        {
            this._server = new TcpListener(ip, port);
        }

        public async Task Start(CancellationToken token)
        {
            this._server.Start();

            while (true)
            {
                var client = this._server.AcceptTcpClient();

                await Task.Run(() =>
                {
                    try
                    {
                        var bytes = new byte[BufferSize];
                        // Get a stream object for reading and writing
                        using (var stream = client.GetStream())
                        {
                            int i;
                            var sb = new StringBuilder();
                            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                sb.Append(this._encoding.GetString(bytes, 0, i));

                                //  var msg = this._encoding.GetBytes(data);

                                //  stream.Write(msg, 0, msg.Length);
                            }
                            this.DataReceived?.Invoke(this, sb.ToString());
                        }
                    }
                    finally
                    {
                        client.Close();
                        client.Dispose();
                    }
                }, token);

            }
        }

        public event EventHandler<string> DataReceived;
    }
}

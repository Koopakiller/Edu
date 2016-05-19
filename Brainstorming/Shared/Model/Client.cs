using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Koopakiller.Apps.Brainstorming.Shared.Model
{
    public class Client : IDisposable
    {
        private readonly TcpClient _client;
        private readonly IPAddress _serverAddress;
        private readonly int _port;

        public Client(IPAddress serverAddress, int port)
        {
            this._client = new TcpClient(AddressFamily.InterNetwork);
            this._serverAddress = serverAddress;
            this._port = port;
        }

        public async Task<string> Connect()
        {
            await this._client.ConnectAsync(this._serverAddress, this._port);

            var stream = this._client.GetStream();
            var bytes = new byte[this._client.ReceiveBufferSize];
            int i;
            var sb = new StringBuilder();
            if ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
            {
                sb.Append(Constants.Encoding.GetString(bytes, 0, i));
            }

            await Task.Run(() =>
            {
                var reader = new StreamReader(stream);
                var text = reader.ReadToEndAsync();
            });

            return sb.ToString();



        }

        public void SendString(string message)
        {
            var data = Constants.Encoding.GetBytes(message);

            var stream = this._client.GetStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public void Dispose()
        {
            this._client.GetStream().Close();
            this._client.Close();
        }
    }
}

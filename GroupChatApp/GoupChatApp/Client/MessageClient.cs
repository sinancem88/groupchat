using System;

using System.IO;

using System.Net.Sockets;

using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class MessageClient
    {

        public event Action<string>? MessageReceived;

        private TcpClient _client;

        private BinaryReader _Reader;

        private BinaryWriter _Writer;



        public MessageClient(TcpClient client)
        {
            _client = client;

            _Reader = new BinaryReader(_client.GetStream());

            _Writer = new BinaryWriter(_client.GetStream());

            Task.Factory.StartNew(HandleIncomingMessages, TaskCreationOptions.LongRunning);


        }

        public MessageClient(string host, int port) : this(new TcpClient(host, port))
        {

        }


        public void HandleIncomingMessages()
        {
            while (_client.Connected)

            {
                if (_client.Available > 0)
                {
                    var message = _Reader.ReadString();
                    MessageReceived?.Invoke(message);

                }

                else
                {
                    Thread.Sleep(1);
                }
            }
        }



        public void SendMessage(string message)
        {
            _Writer.Write(message);
        }


    }
}

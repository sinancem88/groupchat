using Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Server
{
    internal class Client
    {
        public event Action<string>? MessageReceived;

        private TcpClient _client;
        
        private BinaryReader _Reader;
        
        private BinaryWriter _Writer;



        public Client(TcpClient client)
        {
            _client = client;

            _Reader = new BinaryReader(_client.GetStream());

            _Writer = new BinaryWriter(_client.GetStream());

            Task.Factory.StartNew(HandleIncomingMessages, TaskCreationOptions.LongRunning);


        }

        public Client(string host, int port) : this(new TcpClient(host, port))
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

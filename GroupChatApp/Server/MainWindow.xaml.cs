using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Secure;


namespace Server
{

    //Quellen
    //  Alexander Schwahl Networking Vorlesungen
    // Support Marcus 

    public partial class MainWindow : Window
    {
        string messageIDAdmin;

        string messageAdminKey;

        #region fields

        public event Action<string>? MessageReceived;

        private Client client;

        private List<Client> clientList = new List<Client>();

        private TcpListener _server;

        int portNr;

        IPAddress localAddr;

        private string _name = "Server";

        DBKeyUser dataBase = null;


        #endregion


        #region properties



        public IPAddress LocalAddr
        {
            get => localAddr;
            set
            {

                localAddr = value;
                _IPText.Text = localAddr.ToString();
            }
        }



        public int PortNr
        {
            get => portNr;
            set
            {
                portNr = value;
                _PortText.Text = portNr.ToString();

            }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string AdminID { get; set; } = string.Empty;
        public string AdminKey { get; set; } = string.Empty;



        #endregion


        public MainWindow()
        {
            InitializeComponent();

            LocalAddr = IPAddress.Any;
            PortNr = 8080;

            dataBase = new DBKeyUser("localhost", 5432, "postgres", "mec887062", "UserData");
        }



        private void OnServerStartButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, PortNr);

                _server.Start();

                _LogText.Text = "server started...";


                Task.Factory.StartNew(ServerIncomingMessageHandler, TaskCreationOptions.LongRunning);


            }

            catch (Exception ex)
            {

                LogText(ex.Message);
            }


        }



        private void ServerIncomingMessageHandler()
        {

            while (_server.Server.IsBound)
            {

                if (_server.Pending())
                {
                    var newClient = new Client(_server.AcceptTcpClient());


                    clientList.Add(newClient);

                    newClient.MessageReceived += OnClientMessageReceived;

                }

                else
                {
                    Thread.Sleep(1);
                }
            }
        }


        private void OnClientMessageReceived(string message)
        {
            foreach (var clients in clientList)
            {

                clients.SendMessage(message);
            }
        }




        private void LogText(params object[] logText)
        {
            _LogText.Text = string.Join(' ', logText.Append(Environment.NewLine).Prepend(_LogText));
        }


        private async void _AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            

            //_server = new TcpListener(IPAddress.Any, PortNr);

            //_server.Start();

            //await Task.Factory.StartNew(ServerIncomingMessageHandler, TaskCreationOptions.LongRunning);

            //client.SendMessage(_AdminID.Text.ToString());

            //void OnIDRecieved(string msg)
            //{
            //    client.MessageReceived -= OnIDRecieved;
            //    messageIDAdmin = msg;
            //}
            //client.MessageReceived += OnIDRecieved;


            //client.SendMessage(_AdminKey.Text.ToString());

            //client.MessageReceived += (msg) => { messageAdminKey = msg; };
            
            //_server.Stop();
            
            using var hashAlgorythm = SHA512.Create();

            dataBase.InteractToDataBase(_AdminID.Text, hashAlgorythm.ComputeHash(Encoding.UTF8.GetBytes(_AdminKey.Text)).ToString());

            AdminID = _AdminID.Text;
            AdminKey = _AdminKey.Text;

            LogText("keyuser successfully added to database");

        }


    }
}

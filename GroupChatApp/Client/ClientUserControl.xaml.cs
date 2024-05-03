using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Secure;
using Npgsql;
using System.Net.NetworkInformation;

namespace Client
{
    /// <summary>
    /// Interaction logic for ClientUserControl.xaml
    /// </summary>

    //Quellen
    //  Alexander Schwahl Networking Vorlesungen
    // Support Marcus und Stefan


    public partial class ClientUserControl : UserControl
    {

        #region fields

        public event Action<string>? MessageReceived;

        private MessageClient client;

        private List<MessageClient> clientList = new List<MessageClient>();

        //private TcpListener _server;

        int portNr;

        IPAddress localAddr;

        private string _name = "Client";

        #endregion



        #region Properties

   

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


        #endregion

        public ClientUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            LocalAddr = IPAddress.Any;
            PortNr = 8080;
            
        }

        //private void About_Click(object sender, RoutedEventArgs e)
        //{
        //    info.UUID = Guid.NewGuid();
          
        //    MessageBox.Show(info.UUID.ToString(), "UUID: ");
        //    MessageBox.Show(info.Version, "versionNr: ");
        //}

     
        //private void Questions_Click(object sender, RoutedEventArgs e)
        //{

            
        //    MessageBox.Show("for further information visit: google.com");

        //}

        //private void Exit_Click(object sender, RoutedEventArgs e)
        //{

          
        //    System.Windows.Application.Current.Shutdown();
        //}

        private void OnConnectToServerButtonClicked(object sender, RoutedEventArgs e)
        {

            client = new MessageClient("localhost", PortNr);

            client.MessageReceived += HandleMessageReceived;

            LogText("client is connected");

        }


        private void HandleMessageReceived(string message)
        {
            Dispatcher.Invoke(new Action(() => MessageBoxText(message)));
        }

        private void LogText(params object[] logText)
        {
            _Status.Text = string.Join(' ', logText.Append(Environment.NewLine).Prepend(_Status));
        }




        private void MessageBoxText(params object[] messageText)
        {
            _MessageBox.Text = string.Join(' ', messageText.Append(Environment.NewLine).Prepend(_MessageBox.Text));
        }



        private void OnSendMessageClicked(object sender, RoutedEventArgs e)
        {

            try
            {
                client.SendMessage($"{_UserName.Text}: {_MessageText.Text} [{DateTime.Now}]");

            }
            catch (Exception ex)
            {

                LogText(ex.Message);
            }
            _MessageText.Text = string.Empty;

        }

        private void OnAddUserButtonClicked(object sender, RoutedEventArgs e)
        {



        }
    }
}

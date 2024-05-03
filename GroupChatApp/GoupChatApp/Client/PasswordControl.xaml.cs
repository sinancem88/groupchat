
using System.Windows;
using System.Windows.Controls;

using Secure;

namespace Client
{
    
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl
    {
        private readonly Authenticator _Authenticator;

        public User? ValidateUser { get; private set; }

        private const string _FILE = "auth.bin";

        public PasswordControl()
        {
            
            _Authenticator = new Authenticator();
            InitializeComponent();
            //CreateFakeUsers();
         
        }


        private void OnOk(object sender, RoutedEventArgs e)
        {
            
            //this.Content = null;
            //DataContext = this;
            //Window parentWindow = Window.GetWindow(this);
            //parentWindow.Title = "client";
            //this.Content = new ClientUserControl();

            var user = _Authenticator.Authenticate(_UserBox.Text, _PWBox.Password);

            if (user != null && user.IsAuthenticated)
            {
                
                ValidateUser = user;
                Window parentWindow = Window.GetWindow(this);
                parentWindow.Title = "client";
                this.Content = new ClientUserControl();

            }

            else
            {
                _PWBox.Clear();
                _UserBox.Clear();
            }


        }


        private void OnCancel(object sender, RoutedEventArgs e)
        {
            MainWindow.GetWindow(this).Close();
        }


        //private void CreateFakeUsers()
        //{
        //    Authenticator _Autheticator = new Authenticator();
        //    _Authenticator.CreateNewUser("boss", "123",
        //        new string[]
        //        {
        //            "admin",
        //            "special"
        //        });

        //    _Authenticator.CreateNewUser("user2", "123", new string[]
        //    {"regular"});

        //    _Authenticator.SaveUserFile(_FILE);
        //}
    }
}

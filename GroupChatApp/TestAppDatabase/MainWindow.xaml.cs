using System;
using System.Collections.Generic;
using System.Linq;
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
using Npgsql;

namespace TestAppDatabase
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Config
        public static string Server { get; set; } = "localhost";

        public static int Port { get; set; } = 5432;

        public static string UserId { get; set; } = "postgres";

        public static string Password { get; set; } = "mec887062";

        public static string DataBase { get; set; } = "UserData";


        #endregion
        public MainWindow()
        {
            InitializeComponent();

            var con = new NpgsqlConnection(
            connectionString: $"Server={Server};Port={Port};User Id={UserId};Password={Password};Database={DataBase};");

            con.Open();


            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"DROP TABLE IF EXISTS ChatUsers";

            cmd.ExecuteNonQuery();



            cmd.CommandText = "CREATE TABLE ChatUsers (id SERIAL PRIMARY KEY, name VARCHAR(255),password VARCHAR(255))";

            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO ChatUsers(name, password) VALUES('Hans','12345')";

            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO ChatUsers(name, password) VALUES('Peter','54321')";

            cmd.ExecuteNonQuery();

            Console.WriteLine("table for users created");

            cmd.CommandText = "DROP TABLE IF EXISTS ChatUsers";

            cmd.ExecuteNonQuery();
        }

        
    }
}

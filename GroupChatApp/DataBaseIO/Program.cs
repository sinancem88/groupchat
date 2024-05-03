using Npgsql;
using Secure;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

//https://zetcode.com/csharp/postgresql/#google_vignette

/*********************************************nur für testzwecke**********************************************************************/


namespace DataBaseIO
{ 
    internal class Program
    {
        //// TODO: Region -> Struct
        //#region Config
        //public static string Server { get; set; } = "localhost";

        //public static int Port { get; set; } = 5432;

        //public static string UserId { get; set; } = "postgres";

        //public static string Password { get; set; } = "mec887062";

        //public static string DataBase { get; set; } = "UserData";


        //#endregion



        //static void Main(string[] args)
        //{

        //    InteractToDataBase();

        //}

        //public static void InteractToDataBase()
        //{
        //    // später 

        //    var con = new NpgsqlConnection(
        //    connectionString: $"Server={Server};Port={Port};User Id={UserId};Password={Password};Database={DataBase};");


        //    con.Open();


        //    using var cmd = new NpgsqlCommand();
        //    cmd.Connection = con;

        //    cmd.CommandText = $"DROP TABLE IF EXISTS ChatUsers";

        //    cmd.ExecuteNonQuery();

        //    //        cmd.CommandText = @"CREATE TABLE ChatUsers(id SERIAL PRIMARY KEY,
        //    //name VARCHAR(255), password VarChar(255))";

        //    cmd.CommandText = "CREATE TABLE ChatUsers (username VARCHAR(255),password VARCHAR(255))";

        //    cmd.ExecuteNonQuery();

        //    cmd.CommandText = "INSERT INTO ChatUsers(username, password) VALUES('Hans','12345')";

        //    cmd.ExecuteNonQuery();

        //    cmd.CommandText = "INSERT INTO ChatUsers(username, password) VALUES('Peter','54321')";

        //    cmd.ExecuteNonQuery();

        //    Console.WriteLine("table for users created");

        //    //cmd.CommandText = "DROP TABLE IF EXISTS ChatUsers";

        //    //cmd.ExecuteNonQuery();




        //}




    }


}
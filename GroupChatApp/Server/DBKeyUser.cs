using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Npgsql;
using Secure;

namespace Server
{
    //Quellen
    //  https://michaelscodingspot.com/postgres-in-csharp/, : https://michaelscodingspot.com/postgres-in-csharp/
    // https://www.npgsql.org/doc/basic-usage.html
    // Support Marcus und Stefan 

    public class DBKeyUser
    {

        #region Config

        private Queue<NpgsqlCommand> commandQueue = new Queue<NpgsqlCommand>();
        public string Server { get; set; }

        public int Port { get; set; }

        public  string UserId { get; set; }

        public  string Password { get; set; }
        public  string DataBase { get; set; }

        NpgsqlConnection con = null;
       
        #endregion


        public DBKeyUser()
        {
            
        }

        public DBKeyUser(string server, int port, string userId, string password, string dataBase)
        {
          
            Server = server;
            
            Port = port;
            
            UserId = userId;
            
            Password = password;
            
            DataBase = dataBase;

            con = new NpgsqlConnection(connectionString: $" Server = {Server}; Port= {Port}; User Id={UserId}; Password = {Password}; Database ={DataBase};");

            con.Open();

            ExecuteCommandForNonQuery($"DROP TABLE IF EXISTS ChatUsers");

            ExecuteCommandForNonQuery("CREATE TABLE ChatUsers (id SERIAL PRIMARY KEY, user_name VARCHAR(255),password VARCHAR(255))");

        }





        public async void ExecuteCommandForNonQuery(string commandText)
        {
            var cmd = new NpgsqlCommand(commandText, con);
            commandQueue.Enqueue(cmd);

            if (commandQueue.Count > 1) return;

            while(commandQueue.Count > 0)
            {
                var toExecute = commandQueue.Peek();
                await toExecute.ExecuteNonQueryAsync();
                commandQueue.Dequeue();
                
            }

        }


        public void InteractToDataBase(string username, string password)
        {
           

            ExecuteCommandForNonQuery($"INSERT INTO ChatUsers(user_name, password) VALUES('{username}','{password}')");

        
        }
    }
}

using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Npgsql;

// Quelle Alexander Schwahl Authentication Vorlesung
//Support Marcus und Stefan
namespace Secure
{
    public class Authenticator
    {
        public string Server { get; set; } = "localhost";

        public int Port { get; set; } = 5432;

        public string UserId { get; set; } = "postgres";

        public string Password { get; set; } = "mec887062";
        public string DataBase { get; set; } = "UserData";

        NpgsqlConnection con = null;

        private List<User> _AllUsers = new List<User>();

        
        //public IEnumerable<User> AllUsers => _AllUsers;
        public Authenticator()
        { }

        


        /// <summary>
        /// this function builds an user object with null value, sets the hash of typed password from textbox as value, compares each user name with the typed user name in textbox if they exist, substetutes the current object with the object from the user list, and returns an user object
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User? Authenticate(string userName, string password)
        {
            var hashUserData = CreateHashFromPassword(password);
            
            con = new NpgsqlConnection(connectionString: $" Server = {Server}; Port= {Port}; User Id={UserId}; Password = {Password}; Database ={DataBase};");

            con.Open();

            string sql = $"SELECT * FROM chatusers WHERE user_name = '{userName}' AND password = '{hashUserData}'";

            var cmd = new NpgsqlCommand(sql, con);

            var matchingUserCount = 0;
            using (NpgsqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    matchingUserCount = rdr.GetInt32(0);
                }
            }



            if (matchingUserCount == 1) // wenn es einen passenden User gibt
            {
                User? loginUser = new User(userName, password /*null, null*/);
                loginUser.IsAuthenticated = true;

                return loginUser;
            }
            else // wenn es keinen oder mehrere passende User gibt
                return null;

                //foreach (var user in _AllUsers)
                //{
                //    if (user.Name == userName)
                //    {
                //        loginUser = user;
                //        loginUser.IsAuthenticated = user.Password.SequenceEqual(hashUserData);
                //    }
                //}
            
        }

        
        /// <summary>
        /// creates a UTF8 binary hash from a typed string and returns the value
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private byte[] CreateHashFromPassword(string password)
        {
            using var hashAlgorythm = SHA512.Create();

            return hashAlgorythm.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        //public User CreateNewUser(string UserName, string password, string[] roles)
        //{
        //    var user = new User(UserName, CreateHashFromPassword(password), roles);
        //    _AllUsers.Add(user);
        //    return user;
        //}

        //public void LoadUserFile(string file)
        //{
        //    using var fs = new FileStream(file, FileMode.Open);
        //    var reader = new BinaryReader(fs);
        //    var length = reader.ReadInt32();
        //    _AllUsers = new List<User>(length);

        //    for (int i = 0; i < length; i++)
        //    {
        //        _AllUsers.Add(User.Deserialize(reader));
        //    }
        //}

        //public void SaveUserFile(string file)
        //{
        //    using var fs = new FileStream(file, FileMode.Create);
        //    var writer = new BinaryWriter(fs);
        //    writer.Write(_AllUsers.Count);
        //    foreach (var user in _AllUsers)
        //    {
        //        user.Serialize(writer);
        //    }
        //}

    }
}
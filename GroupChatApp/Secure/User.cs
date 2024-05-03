using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Quelle Alexander Schwahl Authentication Vorlesung
//Support Marcus und Stefan
namespace Secure
{
    public class User
    {
        

        public string Name { get; }
        public /*byte[]*/ string Password { get; }
        public bool IsAuthenticated { get; internal set; }

        //public string[] Roles { get; }

        public User(string name, /*byte[]*/ string pw /*string[] data*/)
        {
            Name = name;
            Password = pw;
            //Roles = data;
        }

        //public bool IsInRole(params string[] requiredRoles)
        //{
        //    return Roles.Intersect(requiredRoles).Any();
        //}


        //public void Serialize(BinaryWriter writer)
        //{
        //    writer.Write(Name);
        //    writer.Write(Password.Length);
        //    foreach (var passPart in Password)
        //    {
        //        writer.Write(passPart);
        //    }

        //    writer.Write(Roles.Length);
        //    foreach (var item in Roles)
        //    {
        //        writer.Write(item);
        //    }

        //}

        //public static User Deserialize(BinaryReader reader)
        //{
        //    var name = reader.ReadString();
        //    var length = reader.ReadInt32();
        //    var password = new byte[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        password[i] = reader.ReadByte();
        //    }

        //    length = reader.ReadInt32();
        //    var data = new string[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        data[i] = reader.ReadString();
        //    }

        //    return new User(name, password, data);
        //}
    }
}

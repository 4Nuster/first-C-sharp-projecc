using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test2.MVVM.Model
{
    public class User
    {
        public string phone { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public static int idCounter = 0;
        public int id { get; }

        public User(string phone, string password, string email = "")
        {
            this.phone = phone;
            this.password = password;
            this.email = email;
            //id = idCounter;
            //idCounter++;
        }

        public User(System.Int64 id, System.String phone, System.String email, System.String password)
        {
            this.id = (int) id;
            this.phone = phone;
            this.email = email;
            this.password = password;
        }
    }
}

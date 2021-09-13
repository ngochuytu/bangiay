using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website
{
    public class User
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string repassword { get; set; }

        public User(string fullname, string username, string password, string repassword)
        {
            this.fullname = fullname;
            this.username = username;
            this.password = password;
            this.repassword = repassword;
        }
    }
}
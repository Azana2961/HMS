using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    class Admin 
    {
        private string username;
        private string password;
        public Admin(string eusername, string epassword) 
        { 
            password = epassword;
            username = eusername;
        }
    }
}

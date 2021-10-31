using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class AdminAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AdminAccount()
        {

        }
        public AdminAccount(string name, string psw)
        {
            Username = name;
            Password = psw;
        }
    }
}

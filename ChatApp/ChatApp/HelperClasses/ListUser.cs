using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ChatApp.HelperClasses
{
    class ListUser
    {
        private IPAddress address;
        private string userName;

        public string Username { get { return userName; } }

        public IPAddress Address { get { return address; } }

        public ListUser(string name, IPAddress address)
        {
            this.userName = name;
            this.address = address;
        }

        public override string ToString()
        {
            return Username;
        }
    }
}

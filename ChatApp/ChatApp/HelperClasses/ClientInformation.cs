using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.HelperClasses
{

    /// <summary>
	/// Unelegant, muss noch wegrationalisiert werden!
	/// Hält allgemeine Informationen über den verwendeten Port und den gesetzten Nickname des Benutzers fest.
    /// </summary>
    public static class ClientInformation
    {
        static int port;

        static string nickname;

        public static int Port { get { return port; } set { port = value; } }

        public static string Nickname { get { return nickname; } set { nickname = value; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
	public static class ClientInformation
	{
		static int port;

		static string nickname;

		public static int Port { get { return port; } set { port = value; } }

		public static string Nickname { get { return nickname; } set { nickname = value; } }
	}
}

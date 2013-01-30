using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;

namespace ChatApp
{
    class Client
    {

		TcpClient connection;

		IPAddress targetAddress;

		string nickName;

		public IPAddress TargetAddress { get { return targetAddress; } }
		public string NickName { get { return nickName; } }

		public Client(string nickName, IPAddress targetAddress)
		{
			this.targetAddress = targetAddress;

			this.nickName = nickName;

			connection = new TcpClient();
		}

		public bool Connect()
		{
			try
			{
				connection.Connect(targetAddress, ClientInformation.Port);

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine("Fehler beim Verbinen mit: " + targetAddress.ToString());
				Console.WriteLine("Fehler: " + e.Message);
			}
			finally
			{
				connection.Close();
			}

			return false;
		}

        
    }
}

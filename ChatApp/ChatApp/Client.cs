using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChatApp
{
    class Client
    {

		TcpClient connection;

		StreamReader reader;

		StreamWriter writer;

		IPAddress targetAddress;

		bool connected;

		string nickName;

		Thread thr_ReceiveMessages;

		public IPAddress TargetAddress { get { return targetAddress; } }
		public string NickName { get { return nickName; } }

		public Client(string nickName, IPAddress targetAddress)
		{
			this.targetAddress = targetAddress;

			this.nickName = nickName;

			connection = new TcpClient();

			thr_ReceiveMessages = new Thread(KeepListening);
			thr_ReceiveMessages.IsBackground = true;
			thr_ReceiveMessages.Name = "ReceiverThread for " + nickName;
		}

		public bool Connect()
		{
			try
			{
				connection.Connect(targetAddress, ClientInformation.Port);
				connected = true;

				thr_ReceiveMessages.Start();

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine("Fehler beim Verbinden mit: " + targetAddress.ToString());
				Console.WriteLine("Fehler: " + e.Message);

				connected = false;
			}

			return false;
		}

		public void CloseConnection()
		{
			if (connected)
			{
				connected = false;
			}
		}

		private void KeepListening()
		{
			string message;

			reader = new StreamReader(connection.GetStream());

			while (connected)
			{
				message = reader.ReadToEnd();

				Console.WriteLine("Message from " + nickName + ": " + message);
			}

			writer.Close();
			reader.Close();
			connection.Close();
		}

		public void SendMessage(Message msg)
		{
			if (connected)
			{
				writer = new StreamWriter(connection.GetStream());

				writer.WriteLine(msg.ToString());
				writer.Flush();

			}
		}


        
    }
}

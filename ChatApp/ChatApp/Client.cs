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

		int port;

		bool connected;

		string nickName;

		Thread thr_ReceiveMessages;

		public IPAddress TargetAddress { get { return targetAddress; } }
		public string NickName { get { return nickName; } }

		public Client(string nickName, IPAddress targetAddress, int port)
		{
			this.targetAddress = targetAddress;
			this.port = port;

			this.nickName = nickName;

			connection = new TcpClient();

			thr_ReceiveMessages = new Thread(KeepListening);
			thr_ReceiveMessages.IsBackground = true;
			thr_ReceiveMessages.Name = "ReceiverThread for " + nickName;
		}

		public Client(string nickName, TcpClient client)
		{
			this.targetAddress = ((IPEndPoint)(client.Client.RemoteEndPoint)).Address;
			this.port = ((IPEndPoint)(client.Client.RemoteEndPoint)).Port;

			this.nickName = nickName;

			connection = client;

			thr_ReceiveMessages = new Thread(KeepListening);
			thr_ReceiveMessages.IsBackground = true;
			thr_ReceiveMessages.Name = "ReceiverThread for " + nickName;
		}

		public bool Connect()
		{
			try
			{
				connection.Connect(targetAddress, port);
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

			while ((message = reader.ReadLine()) != "")
			{
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

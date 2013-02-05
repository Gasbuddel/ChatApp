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
    public class Client
    {

		public delegate void DelegateClientMessageReceived(Message msg);

		public DelegateClientMessageReceived DelClientMessageReceived;

		TcpClient connection;

		StreamReader reader;

		StreamWriter writer;

		IPAddress targetAddress;

		int port;

		bool connected = false;

		public bool Connected 
		{
			get { return connected; } 
		}


		string nickName = "";

		Thread thr_ReceiveMessages;

		public IPAddress TargetAddress { get { return targetAddress; } }
		public string NickName { get { return nickName; } }

		ChatWindow chWindow;

		/// <summary>
		/// Erstellt einen neuen Client und baut die TCP-Verbindung mit den Angaben auf
		/// </summary>
		/// <param name="nickName">Name des Clients</param>
		/// <param name="targetAddress">Ziel-IP Adresse</param>
		/// <param name="port">Zielport</param>
		public Client(string nickName, IPAddress targetAddress, int port)
		{
			this.targetAddress = targetAddress;
			this.port = port;

			this.nickName = nickName;

			connection = new TcpClient();

			thr_ReceiveMessages = new Thread(KeepListening);
			thr_ReceiveMessages.IsBackground = true;
			thr_ReceiveMessages.Name = "ReceiverThread for " + nickName;

			connected = true;
		}

		/// <summary>
		/// Stellt eine Verbindung mit einem TCP-Client her.
		/// Dabei wird versucht mittels der gesendeten Informationen, einen Benutzernamen ausfindig zu machen
		/// </summary>
		/// <param name="client">Zielclient</param>
		public Client(TcpClient client)
		{
			try
			{
				Message clientResp = TryReceiving(client);

				if (clientResp.Type != "ERR")
				{
					this.targetAddress = ((IPEndPoint)(client.Client.RemoteEndPoint)).Address;
					this.port = ((IPEndPoint)(client.Client.RemoteEndPoint)).Port;

					this.nickName = clientResp.Nickname;

					connection = client;

					thr_ReceiveMessages = new Thread(KeepListening);
					thr_ReceiveMessages.IsBackground = true;
					thr_ReceiveMessages.Name = "ReceiverThread for " + nickName;

					connected = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				this.connected = false;
			}
		}

		/// <summary>
		/// Nimmt eine Nachricht auf, um sicherzustellen, dass es sich um eine Protokollkonorme Kommunikation
		/// </summary>
		/// <param name="client">Zielclient</param>
		/// <returns>Empfangene Nachricht</returns>
		private Message TryReceiving(TcpClient client)
		{
			StreamReader reader = new StreamReader(client.GetStream());

			string receivedLine = reader.ReadLine();

			Message receivedMessage = new Message(receivedLine);

			return receivedMessage;
		}

		/// <summary>
		/// Verbindet den Client, um auf einkommende Nachrichten zu lauschen
		/// </summary>
		/// <returns>Verbindung erfolgreich</returns>
		public bool Connect()
		{
			try
			{
				//TODO!!
				//connection.BeginConnect(targetAddress, port);
				if (connection.Connected)
				{
					connected = true;

					chWindow = new ChatWindow(this);
					this.DelClientMessageReceived += chWindow.AktualisiereNachrichten;

					chWindow.Text = "Chat mit " + NickName;

					chWindow.Show();

					thr_ReceiveMessages.Start();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Fehler beim Verbinden mit: " + targetAddress.ToString());
				Console.WriteLine("Fehler: " + e.Message);

				connected = false;

				return false;
			}
			return false;

		}

		/// <summary>
		/// Verbindung aufrecht erhalten
		/// </summary>
		private void KeepListening()
		{
			string message;

			reader = new StreamReader(connection.GetStream());

			while ((message = reader.ReadLine()) != "")
			{
				if(DelClientMessageReceived != null)
					DelClientMessageReceived(new Message(message));

				Console.WriteLine("Message from " + nickName + ": " + message);
			}

			writer.Close();
			reader.Close();
			connection.Close();
		}

		/// <summary>
		/// Verbindung abbrechen
		/// </summary>
		public void CloseConnection()
		{
			if (connected)
			{
				connected = false;
			}
		}

		/// <summary>
		/// Sende eine Nachricht an den Clienten
		/// </summary>
		/// <param name="msg"></param>
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

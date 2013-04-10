using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using ChatApp.HelperClasses;

namespace ChatApp
{
	/// <summary>
	/// Diese Klasse hält die Verbindung zu einem TCP-Client.
	/// Sie empfängt TCP-Nachrichten und kann Nachrichten senden.
	/// Empfangene TCP-Nachrichten werden über einen Delegaten weitergereicht.
	/// Die Kommunikation erfolgt über Streamwriter und Streamreader. Mit diesen Klassen kann man getrennte Pakete schicken,
	/// was es mir ermöglicht, Protokollgerechte, getrennte Pakete zu schicken, ohne dass ich den Networkstream lesen muss und
	/// diesen aufteilen muss.
	/// </summary>
    public class Client
    {
		//Nachricht empfangen
		public delegate void DelegateClientMessageReceived(Message msg);
		public DelegateClientMessageReceived DelClientMessageReceived;

		//Verbindung zum Client geschlossen
        public delegate void DelegateConnectionClosed();
        public DelegateConnectionClosed DelConnectionClosed;

		TcpClient connection;

		StreamReader reader;

		StreamWriter writer;

		IPAddress targetAddress;

		int port;

        /// <summary>
        /// Besteht eine aktive TCP Verbindung?
        /// </summary>
		public bool Connected 
		{
			get { return connection.Connected; } 
		}


		string nickName = "";

		Thread thr_ReceiveMessages;

		public IPAddress TargetAddress { get { return targetAddress; } }
		public string NickName { get { return nickName; } }

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

            DelClientMessageReceived += delegate(Message msg) { };

            DelConnectionClosed += delegate() { };

            Connect();

		}

		/// <summary>
		/// Stellt eine Verbindung mit einem TCP-Client her.
		/// </summary>
		/// <param name="client">Zielclient</param>
		public Client(string nickName ,TcpClient client)
		{
			try
			{

				this.targetAddress = ((IPEndPoint)(client.Client.RemoteEndPoint)).Address;
				this.port = ((IPEndPoint)(client.Client.RemoteEndPoint)).Port;

                this.nickName = nickName;

				connection = client;

				thr_ReceiveMessages = new Thread(KeepListening);
				thr_ReceiveMessages.IsBackground = true;
				thr_ReceiveMessages.Name = "ReceiverThread for " + nickName;

                DelClientMessageReceived += delegate(Message msg) { };

                DelConnectionClosed += delegate() { };

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
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
                if (!Connected)
                {
                    connection.Connect(targetAddress, port);

                    thr_ReceiveMessages.Start();

                    return true;
                }

                thr_ReceiveMessages.Start();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim Verbinden mit: " + targetAddress.ToString());
                Console.WriteLine("Fehler: " + e.Message);

                DelConnectionClosed();

                return false;
            }

        }

        /// <summary>
        /// Verbindung aufrecht erhalten und auf Nachrichten horchen
        /// </summary>
        private void KeepListening()
        {
            if (Connected)
            {
                string message;

                reader = new StreamReader(connection.GetStream(),Encoding.Unicode);

                writer = new StreamWriter(connection.GetStream(), Encoding.Unicode);
                writer.AutoFlush = true;

                while (Connected)
                {
                    try
                    {
                        message = reader.ReadLine();
                        DelClientMessageReceived(new Message(message));

                        Console.WriteLine("Message from " + nickName + ": " + message);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Verbindung zu Client unterbrochen");
                    }
                }
                if (writer != null)
                    writer.Close();
                if (reader != null)
                    reader.Close();
                connection.Close();

                DelConnectionClosed();
            }
        }

		/// <summary>
		/// Nimmt eine Nachricht auf, um sicherzustellen, dass es sich um eine Protokollkonorme Kommunikation handelt
		/// </summary>
		/// <param name="client">Zielclient</param>
		/// <returns>Empfangene Nachricht</returns>
		//private Message TryReceiving(TcpClient client)
		//{
		//	StreamReader reader = new StreamReader(client.GetStream());

		//	string receivedLine = reader.ReadLine();

		//	Message receivedMessage = new Message(receivedLine);

		//	return receivedMessage;
		//}



		/// <summary>
		/// Verbindung abbrechen
		/// </summary>
		public void CloseConnection()
		{
			if (Connected)
			{
                connection.Close();
                DelConnectionClosed();
			}
		}

		/// <summary>
		/// Sende eine Nachricht an den Clienten
		/// </summary>
		/// <param name="msg">Nachricht</param>
		public void SendMessage(Message msg)
		{
			if (connection.Connected)
			{
				writer.WriteLine(msg.ToString());
			}
		}     
    }
}

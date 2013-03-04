using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace ChatApp
{
	/// <summary>
	/// Behandelt ein- und ausgehende UDP-Kommunikationen (Broadcasts)
	/// </summary>
    class UDPHandler
    {
		int port;
		UdpClient bcClient;

		//Thread zum Handlen des UDP-Listening Prozesses
		private Thread thr_UDPListen;

		//Delegat, das ausgelöst wird, wenn eine UDP-Nachricht empfangen wurde
		public delegate void DelegateBroadcastReceived(Message msg, IPAddress source);

		/// <summary>
		/// UDP-Nachricht wurde Empfangen
		/// </summary>
		public DelegateBroadcastReceived delBroadcast;

		static UDPHandler instance;

		public static UDPHandler GetInstance(int udpPort)
		{
			if (instance == null)
				instance = new UDPHandler(udpPort);
			return instance;
		}

		/// <summary>
		/// Klasse zum Handlen von UDP Nachrichten (Senden + Empfangen)
		/// </summary>
		/// <param name="listenerPort">Port, auf dem gesendet und gehorcht werden soll</param>
        private UDPHandler(int port)
        {
			this.port = port;
			bcClient = new UdpClient(new IPEndPoint(IPAddress.Any, port));
			//Thread zum horchen auf UDP Nachrichten vorbereiten
			thr_UDPListen = new Thread(startListeningForBroadcast);
			//Sorgt dafür, dass der Thread bei Beendigung des Programms auch geschlossen wird
			thr_UDPListen.IsBackground = true;

			thr_UDPListen.Name = "UDP ListeningThread";

        }

		/// <summary>
		/// Sende eine Broadcastnachricht an alle Teilnehmer im Netzwerk
		/// </summary>
		/// <param name="msg">Messageobjekt für die Nachricht</param>
        public void SendBroadCast(Message msg)
        {
            //EndPoint definieren bzw. Ziel des Broadcastes
            IPEndPoint broadcastTarget = new IPEndPoint(IPAddress.Broadcast, port);
			
			//Nachricht senden
			bcClient.Send(msg.CreateByteArray(), msg.CreateByteArray().Length, broadcastTarget);

            //Für Debug - Zwecke
            Console.WriteLine("Broadcast sent : " + IPAddress.Broadcast.ToString() + " : " + port);
        }

		/// <summary>
		/// Sendet eine UDP Nachricht an ein angegebenes Ziel
		/// </summary>
		/// <param name="msg">Message, die gesendet werden soll</param>
		/// <param name="target">Zieladresse</param>
		/// <param name="senderPort">Port zum Senden der Nachricht</param>
		public void SendMessage(Message msg, IPAddress target)
		{
			//Selbes Prinzip wie oben, nur dass die IPAddresse angegeben wird.
			//UdpClient udpMsgSendClient = new UdpClient();

			IPEndPoint msgTarget = new IPEndPoint(target, port);

			bcClient.Send(msg.CreateByteArray(), msg.CreateByteArray().Length, msgTarget);

			Console.WriteLine("Nachricht: '" + msg.ToString() + "' an " + target.ToString() + " gesendet.");

			//udpMsgSendClient.Close();
		}
		
		/// <summary>
		/// Startet den Thread zum empfangen von Broadcasts
		/// </summary>
		public void StartListening()
		{
			//Absichern, ob der Thread bereits läuft
			if (!thr_UDPListen.IsAlive)
			{
				thr_UDPListen.Start();
			}
		}

		/// <summary>
		/// Empfange Broadcasts. Fängt Nachrichten ab, packt diese in ein Message Objekt und löst mit diesem und der Quelladresse
		/// das Delegat für den Broadcast aus.
		/// </summary>
		private void startListeningForBroadcast()
		{
			//Endpunkt zum Empfangen von Broadcasts
			IPEndPoint broadCastEP = new IPEndPoint(IPAddress.Any, port);
			try
			{
				while (true)
				{
					Console.WriteLine("Waiting for broadcast");
					//Empfange Nachrichten
					byte[] bytes = bcClient.Receive(ref broadCastEP);
					Message msg;

					//Übertrage die Nachricht in ein Message Objekt
					msg = new Message(bytes);
					//Löse das Delegat aus und geben die Message, sowie IP weiter.
					delBroadcast(msg,broadCastEP.Address);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
            //finally
            //{
            //    bcClient.Close();
            //}
		}


    }
}

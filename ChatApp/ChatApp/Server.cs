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
    class Server
    {
		//Port zum horchen und senden
        private int port;
		//Thread zum Handlen des UDP-Listening Prozesses
		private Thread thr_UDPListen;

		//Delegat, das ausgelöst wird, wenn eine UDP-Nachricht empfangen wurde
		public delegate void DelegateBroadcastReceived(Message msg, IPAddress source);

		/// <summary>
		/// UDP-Nachricht wurde Empfangen
		/// </summary>
		public DelegateBroadcastReceived delBroadcast;

		/// <summary>
		/// Klasse zum Handlen von UDP Nachrichten (Senden + Empfangen)
		/// </summary>
		/// <param name="listenerPort">Port, auf dem gesendet und gehorcht werden soll</param>
        public Server(int listenerPort)
        {
            port = listenerPort;
			//Thread zum horchen auf UDP Nachrichten vorbereiten
			thr_UDPListen = new Thread(startListeningForBroadcast);

        }

		/// <summary>
		/// Sende eine Broadcastnachricht an alle Teilnehmer im Netzwerk
		/// </summary>
		/// <param name="msg">Messageobjekt für die Nachricht</param>
        public void SendBroadCast(Message msg)
        {
			//UDP Client für das Senden eines Broadcasts
			UdpClient udpBCSendClient = new UdpClient();

            //EndPoint definieren bzw. Ziel des Broadcastes
            IPEndPoint broadcastTarget = new IPEndPoint(IPAddress.Broadcast, port);
			
			//Nachricht senden
			udpBCSendClient.Send(msg.CreateByteArray(), msg.CreateByteArray().Length, broadcastTarget);

            //Für Debug - Zwecke
            Console.WriteLine("Broadcast sent : " + IPAddress.Broadcast.ToString() + " : " + port.ToString());

			//Verbindung schließen nicht vergessen
			udpBCSendClient.Close();
        }
		
		/// <summary>
		/// Startet den Thread zum empfangen von Broadcasts
		/// </summary>
		public void ReceiveBroadcast()
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
			UdpClient udpListener = new UdpClient(port);
			//Endpunkt zum Empfangen von Broadcasts
			IPEndPoint broadCastEP = new IPEndPoint(IPAddress.Any, port);
			try
			{
				while (true)
				{
					Console.WriteLine("Waiting for broadcast");
					//Empfange Nachrichten
					byte[] bytes = udpListener.Receive(ref broadCastEP);
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
			finally
			{
				udpListener.Close();
			}

		}
    }
}

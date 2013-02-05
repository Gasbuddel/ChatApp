using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace ChatApp
{
	/// <summary>
	/// Dient zum Akzeptieren und weiterleiten von TCP Clients, sowies zum TCP-Verbindungsaufbau
	/// </summary>
	class TCPHandler
	{
		int port;

		public int Port { get { return port; } }

		/// <summary>
		/// Ein Client wurde akzeptiert
		/// </summary>
		/// <param name="client">Neuer Client</param>
		public delegate void DelegateClientAccepted(TcpClient client);

		public DelegateClientAccepted DelClientAccepted;

		//Thread für das Empfangen von Verbindungsanfragen
		Thread thr_TcpListen;

		//Server läuft (bei false wird der Listeningprozess beendet)
		bool servRunning;

		/// <summary>
		/// Hört auf eingehende TCP-Anfragen auf dem angegebenen Port und leitet diese via Delegat weiter
		/// </summary>
		/// <param name="port">Hörerport</param>
		public TCPHandler(int port)
		{
			this.port = port;
			thr_TcpListen = new Thread(keepListening);
			thr_TcpListen.IsBackground = true;
			thr_TcpListen.Name = "TCP Listening Thread";
		}

		/// <summary>
		/// Initialisiert den Listeningvorgang
		/// </summary>
		public void StartListening()
		{
			if (!thr_TcpListen.IsAlive)
			{
				servRunning = true;
				thr_TcpListen.Start();
			}
		}

		/// <summary>
		/// Listening aufrechterhalten und eingehende Clientanfragen weiterleiten
		/// </summary>
		private void keepListening()
		{
			TcpListener listener = new TcpListener(IPAddress.Any, port);

			listener.Start();

			Console.WriteLine("Listener Started");

			TcpClient newClient;

			try
			{

				while (servRunning)
				{
					newClient = listener.AcceptTcpClient();

					if (DelClientAccepted != null)
						DelClientAccepted(newClient);

					Console.WriteLine("Client akzeptiert: " + newClient.Client.LocalEndPoint.ToString());
				}
				listener.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine("Socketexcpetion: " + e.Message);
			}
			finally
			{
				listener.Stop();
			}
		}

		/// <summary>
		/// Mit dem Listeningvorgang aufhören
		/// </summary>
		public void StopListening()
		{
			if (servRunning)
			{
				servRunning = false;
			}
		}
	}
}

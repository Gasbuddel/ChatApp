using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace ChatApp
{
	/// <summary>
	/// Dient zum Akzeptieren und weiterleiten von TCP Clients, sowies zum TCP-Verbindungsaufbau
	/// </summary>
	class TCPHandler
	{
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

		public TCPHandler()
		{
			thr_TcpListen = new Thread(keepListening);
			thr_TcpListen.IsBackground = true;
			thr_TcpListen.Name = "TCP Listening Thread";
		}

		public void StartListening()
		{
			if (!thr_TcpListen.IsAlive)
			{
				servRunning = true;
				thr_TcpListen.Start();
			}
		}

		private void keepListening()
		{
			TcpListener listener = new TcpListener(IPAddress.Any, ClientInformation.Port);

			listener.Start();

			Console.WriteLine("Listener Started");

			TcpClient newClient;

			try
			{

				while (servRunning)
				{
					newClient = listener.AcceptTcpClient();

					//DelClientAccepted(newClient);

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

		public void StopListening()
		{
			if (servRunning)
			{
				servRunning = false;
			}
		}
	}
}

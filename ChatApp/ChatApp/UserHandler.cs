using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
	/// <summary>
	/// Verwaltet gefundene Chatteilnehmer und deren Verbindungen
	/// </summary>
	class UserHandler
	{
		/// <summary>
		/// Delegat zur Benachrichtigung, dass sich in der Teilnehmerliste etwas geändert hat
		/// </summary>
		/// <param name="newList">Neue Teilnehmerliste</param>
		public delegate void DelegateUserListChanged(List<string> newList);

		public DelegateUserListChanged DelUserListChanged;

		private Dictionary<string, IPAddress> users;

		private Dictionary<IPAddress, string> addresses;

		private Dictionary<string, Client> connections;

		public Dictionary<string, IPAddress> Users { get { return users; } }

		public Dictionary<string, Client> Connections { get { return connections; } }

		Thread tcpAcceptThread;

		private string nickname;

		UDPHandler udpHandle;

		int port;

		public UserHandler(string nickname, int port)
		{
			users = new Dictionary<string, IPAddress>();

			addresses = new Dictionary<IPAddress, string>();

			connections = new Dictionary<string, Client>();

			this.nickname = nickname;

			this.port = port;

			udpHandle = UDPHandler.GetInstance(port);
		}

		/// <summary>
		/// Übeprüft eingehende Requests, SOL werde beantwortet, ACK werden nur der Liste hinzugefügt
		/// </summary>
		/// <param name="msg">Eingegangene Nachricht</param>
		/// <param name="address">Quelladresse</param>
		public void CheckRequest(Message msg, IPAddress address)
		{
			//Im Fallse SOL: Hinzufügen + ACK senden
			if (msg.Type == "SOL")
			{
				AddNewUser(msg.Nickname, address);

				//ACK zurücksenden
				udpHandle.SendMessage(MessageCreator.CreateACK(nickname), address);
			}
			//Im Falle ACK nur hinzufügen
			else if (msg.Type == "ACK")
			{
				AddNewUser(msg.Nickname, address);
			}
		}

		/// <summary>
		/// Fügt der Liste einen neuen User hinzu, falls dieser noch nicht eingetragen ist. Sendet ein ListChanged Delegat
		/// </summary>
		/// <param name="name">Name des Users</param>
		/// <param name="address">Quelladresse</param>
		/// <returns></returns>
		public bool AddNewUser(string name, IPAddress address)
		{
			if (!users.ContainsKey(name))
			{
				users.Add(name, address);
				addresses.Add(address, name);

				Console.WriteLine("Benutzer: " + name + " wurde hinzugefügt");

				//Informiere, dass sich die Liste geändert hat
				DelUserListChanged(users.Keys.ToList<String>());
				return true;
			}
			Console.WriteLine("Benutzer: " + name + " ist schon vorhanden.");
			return false;
		}

		/// <summary>
		/// Entfernt einen User und informiert dafüber
		/// </summary>
		/// <param name="name">Name des zu löschenden Users</param>
		/// <returns></returns>
		public bool DeleteUser(string name)
		{
			if(users.ContainsKey(name))
			{
				users.Remove(name);
				DelUserListChanged(users.Keys.ToList<String>());
				return true;
			}
			return false;
		}

		public bool OpenConnection(string clientNickName)
		{
			if (users.ContainsKey(clientNickName))
			{
				if (!connections.ContainsKey(nickname))
				{
					connections.Add(clientNickName, new Client(clientNickName, users[clientNickName], port));

					connections[clientNickName].Connect();
				}
			}
			return false;
		}

		public void AcceptConnection(TcpClient client)
		{
			bool isVorhanden = false;
			IPAddress clientAddress = ((IPEndPoint)(client.Client.RemoteEndPoint)).Address;

			foreach (Client cl in connections.Values)
			{
				if (cl.TargetAddress == clientAddress)
				{
					isVorhanden = true;
				}
			}

			if (!isVorhanden)
			{
				if(addresses.ContainsKey(clientAddress))
				{
					connections.Add(addresses[clientAddress],new Client(addresses[clientAddress],client));
					connections[addresses[clientAddress]].Connect();
				}
			}
		}

		private void ListenForMessage(TcpClient client)
		{
			StreamReader listen = new StreamReader(client.GetStream());

			string response;

			while ((response = listen.ReadLine()) != "")
			{
				Console.WriteLine("TCP: " + response);
			}
		}
	}
}

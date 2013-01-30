using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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

		private Dictionary<string, Client> connections;

		public Dictionary<string, IPAddress> Users { get { return users; } }

		public Dictionary<string, Client> Connections { get { return connections; } }

		public UserHandler()
		{
			users = new Dictionary<string, IPAddress>();

			connections = new Dictionary<string, Client>();
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
				Message acknowledged = new Message();

				acknowledged.Type = "ACK";
				acknowledged.Status = "ONL";
				acknowledged.Nickname = ClientInformation.Nickname;

				UDPHandler.SendMessage(acknowledged, address);
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

		public bool OpenConnection(string nickName)
		{
			if (users.ContainsKey(nickName))
			{
				connections.Add(nickName,new Client(nickName, users[nickName]));

				connections[nickName].Connect();
			}
			return false;
		}
	}
}

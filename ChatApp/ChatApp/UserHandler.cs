﻿using System;
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

        //Über UDP gefundene Benutzer
		private Dictionary<string, IPAddress> users;

        //Offene TCP-Verbindungen
		private Dictionary<string, ClientControl> connections;

		public Dictionary<string, IPAddress> Users { get { return users; } }

		public Dictionary<string, ClientControl> Connections { get { return connections; } }

		private string nickname;

		UDPHandler udpHandle;

		int port;

        /// <summary>
        /// Verwaltet Benutzer, die sich über Broadcasts gemeldet haben und baut Verbindungen zu diesen auf
        /// </summary>
        /// <param name="nickname">Nickname des Clients</param>
        /// <param name="port">Port des Clients</param>
		public UserHandler(string nickname, int port)
		{
			users = new Dictionary<string, IPAddress>();

			connections = new Dictionary<string, ClientControl>();

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
				//addresses.Add(address, name);

				Console.WriteLine("Benutzer: " + name + " wurde hinzugefügt");

				//Informiere, dass sich die Liste geändert hat
                DelUserListChanged(users.Keys.ToList<String>());
                //List<string> result = new List<string>();
                //foreach(string nick in users.Keys.ToList<string>())
                //{
                //    result.Add(nick + " " + users[nick]);
                //}
                //DelUserListChanged(result);
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

		/// <summary>
		/// Stellt eine Verbindung zu einem Client aus der Liste her
		/// </summary>
		/// <param name="clientNickName">Client, zu dem verbunden werden soll</param>
		/// <returns>Verbindung erfolgreich</returns>
		public bool OpenConnection(string clientNickName)
		{
			if (users.ContainsKey(clientNickName))
			{
				if (!connections.ContainsKey(nickname))
				{
					connections.Add(clientNickName, new ClientControl(clientNickName, users[clientNickName], port));

					connections[clientNickName].Connect();
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Akzeptiert eingehende TCP-Anfragen und erstellt einen dazugehörigen CLient, sofern noch keiner vorhanden
		/// ist
		/// </summary>
		/// <param name="client">Neuer Client</param>
		public void AcceptConnection(TcpClient client)
		{
			ClientControl newClient = new ClientControl(client);
			if (newClient.Connected)
			{
				if(!connections.ContainsKey(newClient.NickName))
				{
					connections.Add(newClient.NickName, newClient);
					newClient.Connect();
				}
			}
			
		}

	}
}

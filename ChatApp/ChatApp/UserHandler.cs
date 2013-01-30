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
	/// Verwaltet gefundene Chatteilnehmer
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

		public UserHandler()
		{
			users = new Dictionary<string, IPAddress>();
		}

		public bool AddNewUser(string name, IPAddress address)
		{
			if (!users.ContainsKey(name))
			{
				users.Add(name, address);

				DelUserListChanged(users.Keys.ToList<String>());
				return true;
			}

			return false;
		}

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
	}
}

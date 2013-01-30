using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
	/// <summary>
	/// Behandelt ankommende Nachrichten und leitet die Ergebnisse an Delegaten weiter
	/// </summary>
	class MessageHandler
	{
		public delegate bool DelegateUserJoined(string nickName, IPAddress address);

		public delegate bool DelegateUserLeft(string nickName);

		public DelegateUserJoined DelUserJoined;

		public DelegateUserLeft DelUserLeft;

		public MessageHandler()
		{
			
		}

		public void ProcessMessage(Message msg, IPAddress source)
		{
			switch (msg.Type)
			{
				case "SOL":
					DelUserJoined(msg.Nickname, source);
					break;
				case "SOD":
					DelUserLeft(msg.Nickname);
					break;
				default:
					break;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatApp.HelperClasses;

namespace ChatApp
{
	/// <summary>
	/// Behandelt ankommende Nachrichten und leitet die Ergebnisse an Delegaten weiter
	/// </summary>
	class MessageHandler
	{
		public delegate void DelegateUserJoined(Message msg, IPAddress address);

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
					Console.WriteLine(msg.Nickname + " ist da.");
					DelUserJoined(msg, source);
					break;
				case "SOD":
					Console.WriteLine(msg.Nickname + " hat uns verlassen");
					DelUserLeft(msg.Nickname);
					break;
				case "ACK":
					Console.WriteLine(msg.Nickname + " hat uns geantwortet");
					DelUserJoined(msg, source);
					break;
				case "MSG":
					Console.WriteLine("Nachricht erhalten: " + msg.Body);
					break;
				case "ERR":
					Console.WriteLine("Fehlerhafte Nachricht erhalten: " + msg.ToString());
					break;
				default:
					break;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.HelperClasses
{
	/// <summary>
	/// Eine Hilfsklasse, die bestimmte Messagetypen vorbereitet für leichtere Bedienung
	/// </summary>
    public static class MessageCreator
    {

		public static Message CreateSOL(string nickname)
		{
			Message sol = new Message();
			sol.Type = "SOL";
			sol.Status = "ONL";
			sol.Nickname = nickname;
			return sol;
		}

		public static Message CreateACK(string nickname)
		{
			Message ack = new Message();
			ack.Type = "ACK";
			ack.Status = "ONL";
			ack.Nickname = nickname;
			return ack;
		}

		public static Message CreateSOD(string nickname)
		{
			Message bye = new Message();
			bye.Type = "SOD";
			bye.Status = "OFF";
			bye.Nickname = nickname;
			return bye;
		}

        public static Message CreateMSG(string nickname,string body)
        {
            Message msg = new Message();
            msg.Type = "MSG";
			msg.Status = "ONL";
			msg.Nickname = nickname;
			msg.Body = body;
            return msg;
        }
    }
}

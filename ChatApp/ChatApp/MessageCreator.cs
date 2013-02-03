using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public static class MessageCreator
    {
        public static Message SOL 
        {
            get
            {
                Message sol = new Message();
                sol.Type = "SOL";
                sol.Status = "ONL";
                sol.Nickname = ClientInformation.Nickname;
                return sol;
            } 
        }

        public static Message ACK 
        {
            get
            {
                Message ack = new Message();
                ack.Type = "ACK";
                ack.Status = "ONL";
                ack.Nickname = ClientInformation.Nickname;
                return ack;
            }
        }

        public static Message BYE 
        {
            get
            {
                Message bye = new Message();
                bye.Type = "BYE";
                bye.Status = "OFF";
                bye.Nickname = ClientInformation.Nickname;
                return bye;
            }
        }

        public static Message CreateMSG(string body)
        {
            Message sol = new Message();
            sol.Type = "MSG";
            sol.Status = "ONL";
            sol.Nickname = ClientInformation.Nickname;
            sol.Body = body;
            return sol;
        }
    }
}

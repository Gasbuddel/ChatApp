using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ChatApp
{
	public class Message
	{
        //Variablen
		//Typ der Nachricht (SOL, MSG, ACK, SOD erlaubt)
		private string type;
		//Timestamp zur Zeit des Sendens (Format: Sekunden seit 1970)
		//private double time = 0;
		private int time = 0;
		//Version der Nachricht (z.B. 1.0.0.0)
		private string version;
		//Nickname des Senders
		private string status;
		//Länge der Nachricht
		private string nickname;
		//Status des Senders
		private int length = 0;
		//Nachrichtenkörper
		private string body;
		//Zeichen zum Trennen der Message festlegen
		private char[] delimiter = { '|' };

        //Felderdeklaration
		public string Type 
		{ 
			get { return type; }
			set
			{
				if (value == "SOL" || value == "SOD" || value == "BYE" || value == "ACK")
				{
					type = value;
				}
				else
					type = "ERR";
			}
		}

        //Setzen der Zeit als Timestamp Objekt ebenfalls möglich
		public DateTime TimeStamp
        {
            get
            {
                return UnixTimeFormater.UnixToDateTime(time);
            }
            set
            {
                time = UnixTimeFormater.DateTimeToUnix(value);
            }
        }

		//public double SecTime 
		//{
		//	get { return time; }
		//	set { time = value; } 
		//}

		public int SecTime
		{
			get { return time; }
			set { time = value; }
		}


		public string Version 
        {
            get { return version; }
            set { version = value; }
        }

		public string Status 
        { 
            get { return status; }
			set
			{
				if (value == "ONL" || value == "OFF" || value == "AFK" || value == "DND")
				{
					status = value;
				}
				else
					status = "ERR";
			}
        }

		public string Nickname 
        {
            get { return nickname; }
            set { nickname = value; } 
        }

        //Length wird nicht vom Nutzer gesetzt, sondern automatisch bei Setzen des Bodys
		public int Length { get { return length; } }

		public string Body 
		{ 
			get { return body; }
			set 
			{
				body = value;
				length = value.Length;
			}
		}

		//Eine Message mit Default Werten
		public Message()
		{
			Type = "";
			TimeStamp = DateTime.Now;
			Version = "0.0.1.0";
			Status = "";
			Nickname = "";
			Body = "0";

		}

		//Message direkt mit Nachrichtenstring befüllen
		public Message(string input)
		{
			ReadMessage(input);
		}

		//Message aus einem byte-Array erstellen
		public Message(byte[] input)
		{
			UnicodeEncoding encoder = new UnicodeEncoding();
			ReadMessage(encoder.GetString(input,0,input.Length));
		}

		//Lese einen Messagestring ein und speichere die Bestandteile in den Feldern ab
		public void ReadMessage(string input)
		{
			List<string> parts = input.Split(delimiter,7).ToList<string>();

            //Eine MSG sollte aus 6 Teilen bestehen
			if (parts.Count == 7)
			{
				Type = parts[0];
				//SecTime = Convert.ToDouble(parts[1]);
				SecTime = Convert.ToInt32(parts[1]);
				Version = parts[2];
				Status = parts[3];
				Nickname = parts[4];
				Body = parts[6];
			}
			else
			{
				Type = "ERR";
				TimeStamp = DateTime.Now;
				Version = "0.0.1.0";
				Status = "";
				Nickname = "";
				Body = "Wrong Format: " + input;
			}
		}

		public void ReadMessage(byte[] input)
		{
			UnicodeEncoding encoder = new UnicodeEncoding();
			ReadMessage(encoder.GetString(input, 0, input.Length));
		}

        //Aus den bestandteilen der Message einen String zum Versenden bauen
		public string CreateMessageString()
		{
            string output = "";

            if (type != "" && time != 0 && version != "" && status != "" && nickname != "")
            {
                output = output + type + "|";
                output = output + time + "|";
                output = output + version + "|";
                output = output + status + "|";
                output = output + nickname + "|";
                if (body == "")
                    Body = "0";
                output = output + length + "|";
                output = output + body;
            }			

			return output;
		}

        public byte[] CreateByteArray()
        {
            UnicodeEncoding encoder = new UnicodeEncoding();
            return encoder.GetBytes(CreateMessageString());
        }

		public override string ToString()
		{
			return CreateMessageString();
		}

	}
}

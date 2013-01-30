﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ChatApp
{
	//Enum für die verschiedenen Messagetypen
	public enum Types { SOL,MSG,ACK,SOD};
	//Enum für die diversen Stati
	public enum States { ONL, AFK, DND };

	class Message
	{
        //Variablen
		//Typ der Nachricht (SOL, MSG, ACK, SOD erlaubt)
		private string type;
		//Timestamp zur Zeit des Sendens (Format: Sekunden seit 1970)
		private double time = 0;
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
		//Variable zum Überprüfen, ob genügend Werte vorhanden sind, um einen gültigen String zu erzeugen
		private int valid;

        //Felderdeklaration
		public string Type 
		{ 
			get { return type; }
			set
			{
				if (value == "MSG" || value == "SOL" || value == "SOD" || value == "ACK")
				{
					type = value;
					valid++;
				}
				else
					value = "ERR";
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

        public double SecTime 
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
            set { status = value; } 
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
			Version = "1.0.0.0";
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
			ASCIIEncoding encoder = new ASCIIEncoding();
			ReadMessage(encoder.GetString(input,0,input.Length));
		}

		//Lese einen Messagestring ein und speichere die Bestandteile in den Feldern ab
		public void ReadMessage(string input)
		{
			List<string> parts = input.Split('|').ToList<string>();

            //Eine MSG sollte aus 6 Teilen bestehen
			if (parts.Count == 6)
			{
				Type = parts[0];
				SecTime = Convert.ToDouble(parts[1]);
				Version = parts[2];
				Status = parts[3];
				Nickname = parts[4];
				//Die Nachricht befindet sich im letzten Teil der Liste und wird durch ein ":" getrennt
				string msg = parts[5].Split(':')[1];
				Body = msg;
			}
			else
			{
				Type = "ERR";
				TimeStamp = DateTime.Now;
				Version = "1.0.0.0";
				Status = "";
				Nickname = "";
				Body = "Wrong Format: " + input;
			}
		}

		public void ReadMessage(byte[] input)
		{
			ASCIIEncoding encoder = new ASCIIEncoding();
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
                output = output + length + ":";
                output = output + body;
            }			

			return output;
		}

        public byte[] CreateByteArray()
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            return encoder.GetBytes(CreateMessageString());
        }

		public override string ToString()
		{
			return CreateMessageString();
		}

	}
}
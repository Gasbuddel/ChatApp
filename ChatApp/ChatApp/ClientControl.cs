﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace ChatApp
{
    /// <summary>
    /// Diese Klasse dient als Controller für TCP-Verbindungen zu einzelnen Chatpartnern
    /// Jede Kommunikation besteht aus diesem Controller, einem Client Objekt und einer Chatform
	/// Hier wird das MVC Prinzip angewendet, um die Client Klasse von der Form zu trennen.
    /// </summary>
    class ClientControl
    {
	
		public delegate void DelegateClientDisconnected();

		public DelegateClientDisconnected DelClientDisconnected;

        Client client;
        ChatWindow clientWindow;

        public bool Connected 
        {
            get { return client.Connected; } 
        }

        public string NickName 
        {
            get { return client.NickName; }
        }

        public IPAddress TargetAddress 
        {
            get { return client.TargetAddress; } 
        }

        /// <summary>
        /// Erstelle einen Clientcontroll, um eine neue Verbindung zu öffnen
        /// </summary>
        /// <param name="nickName">Nickname des Users</param>
        /// <param name="targetAddress">Zieladresse</param>
        /// <param name="port">Zielport</param>
        public ClientControl(string nickName, IPAddress targetAddress, int port)
        {
            client = new Client(nickName, targetAddress, port);

            clientWindow = new ChatWindow();

            clientWindow.Text = "Chat mit " + nickName;

			DelClientDisconnected += delegate() { };

            //Aboniere die Delegaten
            client.DelClientMessageReceived += postMessage;
            client.DelConnectionClosed += ConnectionClosed;
            clientWindow.DelSendMessage += SendMessage;
			clientWindow.DelWindowClosed += CloseConnection;
        }

        /// <summary>
        /// Öffne einen neuen Client, um eine eingehende Verbindung zu verarbeiten
        /// </summary>
        /// <param name="connection">Zielverbindung</param>
        public ClientControl(string nickName,TcpClient connection)
        {
            client = new Client(nickName,connection);

            clientWindow = new ChatWindow();

            clientWindow.Text = "Chat mit " + nickName;

            clientWindow.DelSendMessage += SendMessage;

            DelClientDisconnected += delegate() { };

            //Aboniere die Delegaten
            client.DelClientMessageReceived += postMessage;
            client.DelConnectionClosed += ConnectionClosed;
            clientWindow.DelSendMessage += SendMessage;
            clientWindow.DelWindowClosed += CloseConnection;
        }

        /// <summary>
        /// Verbinde den Client und starte den Listeningthread
        /// </summary>
        public void Connect()
        {
            if (!client.Connected)
            {
                client.Connect();

                if (client.Connected)
                {
                    clientWindow.SystemMessage("Client verbunden");
                    clientWindow.ShowDialog();
                }
            }
            else
            {
                client.Connect();

                clientWindow.SystemMessage("Client verbunden");
                clientWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Sende eine Nachricht an den Client
        /// </summary>
        /// <param name="msg">Nachricht</param>
        public void SendMessage(Message msg)
        {
            client.SendMessage(msg);
        }

        /// <summary>
        /// Poste eine eingehende Nachricht auf der Form
        /// </summary>
        /// <param name="msg">Nachricht</param>
        private void postMessage(Message msg)
        {
            clientWindow.AktualisiereNachrichten(msg);
        }

		/// <summary>
		/// Wird das Fenster geschlossen, sollte die Verbindung zu dem Client auch geschlossen werden.
		/// </summary>
		private void CloseConnection()
		{
			if(client.Connected)
				client.CloseConnection();
			DelClientDisconnected();
		}

        /// <summary>
        /// Ist die Verbindung zum Client geschlossen worden, muss dies angezeigt werden
        /// </summary>
        private void ConnectionClosed()
        {
            clientWindow.SystemMessage("Verbindung zum Client geschlossen");
        }

    }
}

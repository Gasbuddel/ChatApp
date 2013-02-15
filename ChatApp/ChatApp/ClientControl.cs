﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    class ClientControl
    {
        Client client;
        ChatWindow clientWindow;

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

            //Aboniere die Delegaten
            client.DelClientMessageReceived += postMessage;
            clientWindow.DelSendMessage += SendMessage;
            
        }

        /// <summary>
        /// Öffne einen neuen Client, um eine eingehende Verbindung zu verarbeiten
        /// </summary>
        /// <param name="connection">Zielverbindung</param>
        public ClientControl(TcpClient connection)
        {
            client = new Client(connection);

            clientWindow = new ChatWindow();

            clientWindow.DelSendMessage += SendMessage;
        }

        /// <summary>
        /// Verbinde den Client
        /// </summary>
        public void Connect()
        {
            if (!client.Connected)
            {
                client.Connect();

                clientWindow.SystemMessage("Client verbunden");
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

    }
}

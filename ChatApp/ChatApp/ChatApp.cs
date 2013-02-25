using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ChatApp.HelperClasses;

namespace ChatApp
{
	public partial class ChatApp : Form
	{
        UDPHandler udpHandle;
		TCPHandler tcpHandle;

		MessageHandler msgHandle;
		UserHandler userHandle;

        int port;
        string nickName;

		delegate void userListChanged_Callback(List<string> newList);

		public ChatApp()
		{
			InitializeComponent();
		}

        //Initialisiere den Chatclient und starte die Listener
        private void InitializeClient()
        {
            //Globale Clientinformationen initialisieren
            port = 1234;

            nickName = tb_NickName.Text;

            //Handlerklassen erstellen
            udpHandle = UDPHandler.GetInstance(port);
            tcpHandle = new TCPHandler(port);
            msgHandle = new MessageHandler();
            userHandle = new UserHandler(nickName,port);

            //Delegaten verbinden
            udpHandle.delBroadcast += msgHandle.ProcessMessage;
            msgHandle.DelUserJoined += userHandle.CheckRequest;
            msgHandle.DelUserLeft += userHandle.DeleteUser;
            userHandle.DelUserListChanged += aktualisiereListe;
			tcpHandle.DelClientAccepted += userHandle.AcceptConnection;

            //Listeningvorgänge starten
            udpHandle.StartListening();
            tcpHandle.StartListening();

            //Es darf kein neuer Nickname vergeben werden.
            tb_NickName.Enabled = false;
            btn_Login.Enabled = false;

            btn_SendSol.Enabled = true;
            btn_SendAck.Enabled = true;
            btn_SendUDP.Enabled = true;
            btn_SendBye.Enabled = true;
            tb_TCPMessage.Enabled = true;
            btn_Connect.Enabled = true;

            //Wir sind online, als senden wir ein SOL
            udpHandle.SendBroadCast(MessageCreator.CreateSOL(nickName));
        }

        /// <summary>
        /// Aktualisiere die ClientListe
        /// </summary>
        /// <param name="newList">Neue Liste von Clients</param>
		private void aktualisiereListe(List<string> newList)
		{
			if (InvokeRequired)
			{
				userListChanged_Callback uListCallback = new userListChanged_Callback(aktualisiereListe);
				this.Invoke(uListCallback,new object[]{newList});
			}
			else
			{
				lb_Clients.Items.Clear();
				lb_Clients.Items.AddRange(newList.ToArray());
			}
		}

        //Benutzer anmelden
        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (tb_NickName.Text != "" && !tb_NickName.Text.Contains('|'))
            {
                InitializeClient();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie zuerst einen Nickname an.");
            }
        }

        //TCP-Verbindung mit Benutzer aufbauen
		private void btn_Connect_Click(object sender, EventArgs e)
		{
			if(lb_Clients.SelectedIndex != -1)
				userHandle.OpenConnection(lb_Clients.SelectedItem.ToString());
		}

        //Einen SOL senden
        private void btn_SendSol_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateSOL(nickName));
        }

        //Ein ACK senden
        private void btn_SendAck_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateACK(nickName));
        }


        //Ein SOL senden
        private void btn_SendBye_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateSOD(nickName));
        }

        //Eine UDP-MSG senden
        private void btn_SendUDP_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateMSG(nickName,tb_TCPMessage.Text));
        }

        //Beim beenden noch ein SOD senden, damit man anzeigt, dass man auch weg ist
		private void ChatApp_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (nickName != null)
				udpHandle.SendBroadCast(MessageCreator.CreateSOD(nickName));
		}

		private void lb_Clients_DoubleClick(object sender, EventArgs e)
		{
			btn_Connect_Click(sender, e);
		}

	}
}

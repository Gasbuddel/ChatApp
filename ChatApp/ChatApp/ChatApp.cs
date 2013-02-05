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

namespace ChatApp
{
	public partial class ChatApp : Form
	{
        UDPHandler udpHandle;
		TCPHandler tcpHandle;

		MessageHandler msgHandle;
		UserHandler userHandle;

		delegate void userListChanged_Callback(List<string> newList);

		public ChatApp()
		{
			InitializeComponent();
		}

        //Initialisiere den Chatclient und starte die Listener
        private void InitializeClient()
        {
            //Globale Clientinformationen initialisieren
            ClientInformation.Port = 1234;

            ClientInformation.Nickname = tb_NickName.Text;

            //MessageCreator = new MessageCreator();

            //Handlerklassen erstellen
            udpHandle = UDPHandler.GetInstance(ClientInformation.Port);
            tcpHandle = new TCPHandler(ClientInformation.Port);
            msgHandle = new MessageHandler();
            userHandle = new UserHandler(ClientInformation.Nickname,ClientInformation.Port);

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
            button2.Enabled = true;

            //Wir sind online, als senden wir ein SOL
            udpHandle.SendBroadCast(MessageCreator.CreateSOL(ClientInformation.Nickname));
        }

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

		private void btn_Connect_Click(object sender, EventArgs e)
		{
			userHandle.OpenConnection(lb_Clients.SelectedItem.ToString());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			userHandle.Connections[lb_Clients.SelectedItem.ToString()].SendMessage(MessageCreator.CreateMSG(ClientInformation.Nickname,tb_TCPMessage.Text));
		}

        private void btn_SendSol_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateSOL(ClientInformation.Nickname));
        }

        private void btn_SendAck_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateACK(ClientInformation.Nickname));
        }

        private void btn_SendBye_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateSOD(ClientInformation.Nickname));
        }

        private void btn_SendUDP_Click(object sender, EventArgs e)
        {
            udpHandle.SendBroadCast(MessageCreator.CreateMSG(ClientInformation.Nickname,tb_TCPMessage.Text));
        }

		private void ChatApp_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (ClientInformation.Nickname != null)
				udpHandle.SendBroadCast(MessageCreator.CreateSOD(ClientInformation.Nickname));
		}

	}
}

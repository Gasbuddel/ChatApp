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
	public partial class Form1 : Form
	{
		Message msg;
        UDPHandler serv;
		TCPHandler tcpHandle;


		MessageHandler msgHandle;
		UserHandler userHandle;

		delegate void userListChanged_Callback(List<string> newList);

		public Form1()
		{
			InitializeComponent();

			ClientInformation.Port = 3333;

			ClientInformation.Nickname = "Horst";

            serv = new UDPHandler();

			tcpHandle = new TCPHandler();

			msgHandle = new MessageHandler();

			userHandle = new UserHandler();

			serv.delBroadcast += msgHandle.ProcessMessage;

			msgHandle.DelUserJoined += userHandle.CheckRequest;

			msgHandle.DelUserLeft += userHandle.DeleteUser;

			userHandle.DelUserListChanged += aktualisiereListe;
           
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			string test = "";
			test = "SOL|";
			test = test + UnixTimeFormater.DateTimeToUnix(DateTime.Now) + "|";
			test = test + "1.0.0.0|ONL|Horst|0:0";

			testBox.Text = test;
		}


		private void testButton_Click(object sender, EventArgs e)
		{
			msg = new Message(testBox.Text);

			resultBox.Text = msg.CreateMessageString();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(msg.TimeStamp.ToString());
        }

        private void btn_BroadcastTest_Click(object sender, EventArgs e)
        {
            serv.SendBroadCast(new Message(testBox.Text));
        }

        private void btn_ReceiveBroadcast_Click(object sender, EventArgs e)
        {
            serv.ReceiveBroadcast();
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

		private void btn_StartTCPListening_Click(object sender, EventArgs e)
		{
			tcpHandle.StartListening();
		}

		private void btn_Connect_Click(object sender, EventArgs e)
		{
			userHandle.OpenConnection(lb_Clients.SelectedItem.ToString());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Message msg = new Message();
			msg.Type = "MSG";
			msg.Nickname = ClientInformation.Nickname;
			msg.Status = "ONL";

			userHandle.Connections[lb_Clients.SelectedItem.ToString()].SendMessage(msg);
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
	public partial class ChatWindow : Form
	{
		Client client;

		delegate void AktualisiereNachrichtenCallback(Message msg);

		public ChatWindow(Client newClient)
		{
			InitializeComponent();

			client = newClient;
		}

		private void btn_Send_Click(object sender, EventArgs e)
		{
			if (tb_Send.Text != "")
			{
				Message msg = MessageCreator.CreateMSG(ClientInformation.Nickname, tb_Send.Text);
				client.SendMessage(msg);

				tb_Send.Text = "";
				
			}
		}

		public void AktualisiereNachrichten(Message msg)
		{
			if (InvokeRequired)
			{
				AktualisiereNachrichtenCallback refreshCallback = new AktualisiereNachrichtenCallback(AktualisiereNachrichten);
				this.Invoke(refreshCallback, new object[] { msg });
			}
			else
			{
				tb_Receive.Text += msg.Body;
			}
		}
	}
}

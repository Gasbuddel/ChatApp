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

		delegate void AktualisiereNachrichtenCallback(Message msg);

        public delegate void DelegateSendMessage(Message msg);

        public DelegateSendMessage DelSendMessage;

		public ChatWindow()
		{
			InitializeComponent();

            DelSendMessage += delegate(Message msg) { };

		}

        /// <summary>
        /// Informiert alle, die das Delegat aboniert haben darüber, dass eine Nachricht gesendet wurde
        /// </summary>
		private void btn_Send_Click(object sender, EventArgs e)
		{
			if (tb_Send.Text != "")
			{
				Message msg = MessageCreator.CreateMSG(ClientInformation.Nickname, tb_Send.Text);

                DelSendMessage(msg);

				tb_Send.Text = "";
			}
		}

        /// <summary>
        /// Zeige neue Nachricht an
        /// </summary>
        /// <param name="msg">Neue Nachricht</param>
		public void AktualisiereNachrichten(Message msg)
		{
			if (InvokeRequired)
			{
				AktualisiereNachrichtenCallback refreshCallback = new AktualisiereNachrichtenCallback(AktualisiereNachrichten);
				this.Invoke(refreshCallback, new object[] { msg });
			}
			else
			{
				tb_Receive.Text += msg.TimeStamp + " " +msg.Nickname + ": " + msg.Body;
			}
		}

        /// <summary>
        /// Postet eine Systemnachricht auf der Form
        /// </summary>
        /// <param name="message">Text, der erscheinen soll</param>
        public void SystemMessage(string message)
        {
            if (message != "")
            {
                tb_Receive.Text += "System: " + message;
            }
        }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatApp.HelperClasses;

namespace ChatApp
{
	/// <summary>
	/// Fenster für die Anzeige eines Chats mit einem Chatpartner
	/// </summary>
	public partial class ChatWindow : Form
	{

		delegate void AktualisiereNachrichtenCallback(Message msg);

        delegate void SystemMessageCallback(string msg);

		public delegate void DelegateWindowClosed();

        public delegate void DelegateSendMessage(Message msg);

		public DelegateWindowClosed DelWindowClosed;

        public DelegateSendMessage DelSendMessage;

		public ChatWindow()
		{
			InitializeComponent();

            DelSendMessage += delegate(Message msg) { };

			DelWindowClosed += delegate() { };
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

                tb_Receive.Text += "Ich: " + tb_Send.Text + Environment.NewLine; 

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
				this.Text = "Konversation mit " + msg.Nickname;
				tb_Receive.Text += msg.TimeStamp + " " +msg.Nickname + ": " + msg.Body + Environment.NewLine;
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
                if (InvokeRequired)
                {
                    SystemMessageCallback sysMesCallback = new SystemMessageCallback(SystemMessage);
                    this.Invoke(sysMesCallback, new object[] { message });
                }
                else
                {
                    tb_Receive.Text += "System: " + message + Environment.NewLine;
                }
            }
        }

		//Wenn die Form geschlossen wird, wird darüber informiert.
		private void ChatWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			DelWindowClosed();
		}
	}
}

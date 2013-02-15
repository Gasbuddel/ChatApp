namespace ChatApp
{
	partial class ChatApp
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatApp));
            this.lb_Clients = new System.Windows.Forms.ListBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.tb_TCPMessage = new System.Windows.Forms.TextBox();
            this.tb_NickName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_SendSol = new System.Windows.Forms.Button();
            this.btn_SendBye = new System.Windows.Forms.Button();
            this.btn_SendAck = new System.Windows.Forms.Button();
            this.btn_SendUDP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_Clients
            // 
            this.lb_Clients.FormattingEnabled = true;
            this.lb_Clients.Location = new System.Drawing.Point(392, 11);
            this.lb_Clients.Name = "lb_Clients";
            this.lb_Clients.Size = new System.Drawing.Size(288, 251);
            this.lb_Clients.TabIndex = 6;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Enabled = false;
            this.btn_Connect.Location = new System.Drawing.Point(571, 279);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(109, 23);
            this.btn_Connect.TabIndex = 8;
            this.btn_Connect.Text = "Mit Client verbinden";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // tb_TCPMessage
            // 
            this.tb_TCPMessage.Enabled = false;
            this.tb_TCPMessage.Location = new System.Drawing.Point(15, 77);
            this.tb_TCPMessage.Name = "tb_TCPMessage";
            this.tb_TCPMessage.Size = new System.Drawing.Size(354, 20);
            this.tb_TCPMessage.TabIndex = 10;
            // 
            // tb_NickName
            // 
            this.tb_NickName.Location = new System.Drawing.Point(77, 13);
            this.tb_NickName.Name = "tb_NickName";
            this.tb_NickName.Size = new System.Drawing.Size(211, 20);
            this.tb_NickName.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nickname:";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(294, 11);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 13;
            this.btn_Login.Text = "Anmelden";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_SendSol
            // 
            this.btn_SendSol.Enabled = false;
            this.btn_SendSol.Location = new System.Drawing.Point(15, 47);
            this.btn_SendSol.Name = "btn_SendSol";
            this.btn_SendSol.Size = new System.Drawing.Size(75, 23);
            this.btn_SendSol.TabIndex = 14;
            this.btn_SendSol.Text = "SOL senden";
            this.btn_SendSol.UseVisualStyleBackColor = true;
            this.btn_SendSol.Click += new System.EventHandler(this.btn_SendSol_Click);
            // 
            // btn_SendBye
            // 
            this.btn_SendBye.Enabled = false;
            this.btn_SendBye.Location = new System.Drawing.Point(177, 47);
            this.btn_SendBye.Name = "btn_SendBye";
            this.btn_SendBye.Size = new System.Drawing.Size(85, 23);
            this.btn_SendBye.TabIndex = 15;
            this.btn_SendBye.Text = "SOD senden";
            this.btn_SendBye.UseVisualStyleBackColor = true;
            this.btn_SendBye.Click += new System.EventHandler(this.btn_SendBye_Click);
            // 
            // btn_SendAck
            // 
            this.btn_SendAck.Enabled = false;
            this.btn_SendAck.Location = new System.Drawing.Point(96, 47);
            this.btn_SendAck.Name = "btn_SendAck";
            this.btn_SendAck.Size = new System.Drawing.Size(75, 23);
            this.btn_SendAck.TabIndex = 16;
            this.btn_SendAck.Text = "ACK senden";
            this.btn_SendAck.UseVisualStyleBackColor = true;
            this.btn_SendAck.Click += new System.EventHandler(this.btn_SendAck_Click);
            // 
            // btn_SendUDP
            // 
            this.btn_SendUDP.Enabled = false;
            this.btn_SendUDP.Location = new System.Drawing.Point(15, 103);
            this.btn_SendUDP.Name = "btn_SendUDP";
            this.btn_SendUDP.Size = new System.Drawing.Size(151, 23);
            this.btn_SendUDP.TabIndex = 17;
            this.btn_SendUDP.Text = "Broadcastnachricht senden";
            this.btn_SendUDP.UseVisualStyleBackColor = true;
            this.btn_SendUDP.Click += new System.EventHandler(this.btn_SendUDP_Click);
            // 
            // ChatApp
            // 
            this.AcceptButton = this.btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 378);
            this.Controls.Add(this.btn_SendUDP);
            this.Controls.Add(this.btn_SendAck);
            this.Controls.Add(this.btn_SendBye);
            this.Controls.Add(this.btn_SendSol);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_NickName);
            this.Controls.Add(this.tb_TCPMessage);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.lb_Clients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChatApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatApp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatApp_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ListBox lb_Clients;
        private System.Windows.Forms.Button btn_Connect;
		private System.Windows.Forms.TextBox tb_TCPMessage;
        private System.Windows.Forms.TextBox tb_NickName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_SendSol;
        private System.Windows.Forms.Button btn_SendBye;
        private System.Windows.Forms.Button btn_SendAck;
        private System.Windows.Forms.Button btn_SendUDP;
	}
}


namespace ChatApp
{
	partial class Form1
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
			this.testBox = new System.Windows.Forms.TextBox();
			this.resultBox = new System.Windows.Forms.TextBox();
			this.testButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.btn_BroadcastTest = new System.Windows.Forms.Button();
			this.btn_ReceiveBroadcast = new System.Windows.Forms.Button();
			this.lb_Clients = new System.Windows.Forms.ListBox();
			this.btn_StartTCPListening = new System.Windows.Forms.Button();
			this.btn_Connect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// testBox
			// 
			this.testBox.Location = new System.Drawing.Point(30, 35);
			this.testBox.Name = "testBox";
			this.testBox.Size = new System.Drawing.Size(351, 20);
			this.testBox.TabIndex = 0;
			// 
			// resultBox
			// 
			this.resultBox.Location = new System.Drawing.Point(30, 92);
			this.resultBox.Name = "resultBox";
			this.resultBox.Size = new System.Drawing.Size(351, 20);
			this.resultBox.TabIndex = 1;
			// 
			// testButton
			// 
			this.testButton.Location = new System.Drawing.Point(30, 177);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(75, 23);
			this.testButton.TabIndex = 2;
			this.testButton.Text = "String testen";
			this.testButton.UseVisualStyleBackColor = true;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(144, 177);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(109, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Datum anzeigen";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btn_BroadcastTest
			// 
			this.btn_BroadcastTest.Location = new System.Drawing.Point(30, 221);
			this.btn_BroadcastTest.Name = "btn_BroadcastTest";
			this.btn_BroadcastTest.Size = new System.Drawing.Size(132, 23);
			this.btn_BroadcastTest.TabIndex = 4;
			this.btn_BroadcastTest.Text = "Broadcast senden";
			this.btn_BroadcastTest.UseVisualStyleBackColor = true;
			this.btn_BroadcastTest.Click += new System.EventHandler(this.btn_BroadcastTest_Click);
			// 
			// btn_ReceiveBroadcast
			// 
			this.btn_ReceiveBroadcast.Location = new System.Drawing.Point(30, 250);
			this.btn_ReceiveBroadcast.Name = "btn_ReceiveBroadcast";
			this.btn_ReceiveBroadcast.Size = new System.Drawing.Size(147, 23);
			this.btn_ReceiveBroadcast.TabIndex = 5;
			this.btn_ReceiveBroadcast.Text = "Broadcast Empfangen";
			this.btn_ReceiveBroadcast.UseVisualStyleBackColor = true;
			this.btn_ReceiveBroadcast.Click += new System.EventHandler(this.btn_ReceiveBroadcast_Click);
			// 
			// lb_Clients
			// 
			this.lb_Clients.FormattingEnabled = true;
			this.lb_Clients.Location = new System.Drawing.Point(388, 35);
			this.lb_Clients.Name = "lb_Clients";
			this.lb_Clients.Size = new System.Drawing.Size(288, 251);
			this.lb_Clients.TabIndex = 6;
			// 
			// btn_StartTCPListening
			// 
			this.btn_StartTCPListening.Location = new System.Drawing.Point(226, 250);
			this.btn_StartTCPListening.Name = "btn_StartTCPListening";
			this.btn_StartTCPListening.Size = new System.Drawing.Size(116, 23);
			this.btn_StartTCPListening.TabIndex = 7;
			this.btn_StartTCPListening.Text = "TCP Listener starten";
			this.btn_StartTCPListening.UseVisualStyleBackColor = true;
			this.btn_StartTCPListening.Click += new System.EventHandler(this.btn_StartTCPListening_Click);
			// 
			// btn_Connect
			// 
			this.btn_Connect.Location = new System.Drawing.Point(388, 293);
			this.btn_Connect.Name = "btn_Connect";
			this.btn_Connect.Size = new System.Drawing.Size(109, 23);
			this.btn_Connect.TabIndex = 8;
			this.btn_Connect.Text = "Connect";
			this.btn_Connect.UseVisualStyleBackColor = true;
			this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(688, 345);
			this.Controls.Add(this.btn_Connect);
			this.Controls.Add(this.btn_StartTCPListening);
			this.Controls.Add(this.lb_Clients);
			this.Controls.Add(this.btn_ReceiveBroadcast);
			this.Controls.Add(this.btn_BroadcastTest);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.testButton);
			this.Controls.Add(this.resultBox);
			this.Controls.Add(this.testBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox testBox;
		private System.Windows.Forms.TextBox resultBox;
		private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_BroadcastTest;
        private System.Windows.Forms.Button btn_ReceiveBroadcast;
		private System.Windows.Forms.ListBox lb_Clients;
		private System.Windows.Forms.Button btn_StartTCPListening;
		private System.Windows.Forms.Button btn_Connect;
	}
}


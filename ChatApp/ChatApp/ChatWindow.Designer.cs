namespace ChatApp
{
	partial class ChatWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tb_Receive = new System.Windows.Forms.TextBox();
            this.tb_Send = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_Receive
            // 
            this.tb_Receive.Location = new System.Drawing.Point(13, 13);
            this.tb_Receive.Multiline = true;
            this.tb_Receive.Name = "tb_Receive";
            this.tb_Receive.ReadOnly = true;
            this.tb_Receive.Size = new System.Drawing.Size(415, 280);
            this.tb_Receive.TabIndex = 0;
            // 
            // tb_Send
            // 
            this.tb_Send.Location = new System.Drawing.Point(13, 300);
            this.tb_Send.Multiline = true;
            this.tb_Send.Name = "tb_Send";
            this.tb_Send.Size = new System.Drawing.Size(415, 81);
            this.tb_Send.TabIndex = 1;
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(13, 388);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(415, 41);
            this.btn_Send.TabIndex = 2;
            this.btn_Send.Text = "Senden";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 441);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.tb_Send);
            this.Controls.Add(this.tb_Receive);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChatWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tb_Receive;
		private System.Windows.Forms.TextBox tb_Send;
		private System.Windows.Forms.Button btn_Send;
	}
}
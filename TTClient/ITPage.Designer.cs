namespace TTClient
{
    partial class ITPage
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sendTB = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.ReceiveButton = new System.Windows.Forms.Button();
            this.receiveTB = new System.Windows.Forms.TextBox();
            this.usersCB = new System.Windows.Forms.ComboBox();
            this.ticketsCB = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(432, 31);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(948, 692);
            this.dataGridView1.TabIndex = 2;
            // 
            // sendTB
            // 
            this.sendTB.Location = new System.Drawing.Point(32, 31);
            this.sendTB.Name = "sendTB";
            this.sendTB.Size = new System.Drawing.Size(391, 31);
            this.sendTB.TabIndex = 3;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(151, 85);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(118, 53);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.sendMsg);
            // 
            // ReceiveButton
            // 
            this.ReceiveButton.Location = new System.Drawing.Point(151, 302);
            this.ReceiveButton.Name = "ReceiveButton";
            this.ReceiveButton.Size = new System.Drawing.Size(118, 55);
            this.ReceiveButton.TabIndex = 5;
            this.ReceiveButton.Text = "Receive";
            this.ReceiveButton.UseVisualStyleBackColor = true;
            this.ReceiveButton.Click += new System.EventHandler(this.receiveMsg);
            // 
            // receiveTB
            // 
            this.receiveTB.Location = new System.Drawing.Point(32, 248);
            this.receiveTB.Name = "receiveTB";
            this.receiveTB.Size = new System.Drawing.Size(391, 31);
            this.receiveTB.TabIndex = 6;
            // 
            // usersCB
            // 
            this.usersCB.FormattingEnabled = true;
            this.usersCB.Location = new System.Drawing.Point(94, 445);
            this.usersCB.Name = "usersCB";
            this.usersCB.Size = new System.Drawing.Size(230, 33);
            this.usersCB.TabIndex = 7;
            // 
            // ticketsCB
            // 
            this.ticketsCB.FormattingEnabled = true;
            this.ticketsCB.Location = new System.Drawing.Point(91, 512);
            this.ticketsCB.Name = "ticketsCB";
            this.ticketsCB.Size = new System.Drawing.Size(233, 33);
            this.ticketsCB.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 588);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 49);
            this.button1.TabIndex = 9;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.onSubmit);
            // 
            // ITPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1404, 738);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ticketsCB);
            this.Controls.Add(this.usersCB);
            this.Controls.Add(this.receiveTB);
            this.Controls.Add(this.ReceiveButton);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.sendTB);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ITPage";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox sendTB;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button ReceiveButton;
        private System.Windows.Forms.TextBox receiveTB;
        private System.Windows.Forms.ComboBox usersCB;
        private System.Windows.Forms.ComboBox ticketsCB;
        private System.Windows.Forms.Button button1;
    }
}
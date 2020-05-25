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
            this.usersCB = new System.Windows.Forms.ComboBox();
            this.ticketsCB = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.answerBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selectLabel = new System.Windows.Forms.Label();
            this.answerButton = new System.Windows.Forms.Button();
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
            this.dataGridView1.Location = new System.Drawing.Point(219, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(647, 548);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectCell);
            // 
            // usersCB
            // 
            this.usersCB.FormattingEnabled = true;
            this.usersCB.Location = new System.Drawing.Point(6, 444);
            this.usersCB.Margin = new System.Windows.Forms.Padding(2);
            this.usersCB.Name = "usersCB";
            this.usersCB.Size = new System.Drawing.Size(208, 21);
            this.usersCB.TabIndex = 7;
            // 
            // ticketsCB
            // 
            this.ticketsCB.FormattingEnabled = true;
            this.ticketsCB.Location = new System.Drawing.Point(6, 488);
            this.ticketsCB.Margin = new System.Windows.Forms.Padding(2);
            this.ticketsCB.Name = "ticketsCB";
            this.ticketsCB.Size = new System.Drawing.Size(208, 21);
            this.ticketsCB.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 513);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 25);
            this.button1.TabIndex = 9;
            this.button1.Text = "Submit Solver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.onSubmit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 468);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ticket ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 427);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Assign a solver";
            // 
            // answerBox
            // 
            this.answerBox.Location = new System.Drawing.Point(18, 208);
            this.answerBox.Margin = new System.Windows.Forms.Padding(2);
            this.answerBox.Name = "answerBox";
            this.answerBox.Size = new System.Drawing.Size(176, 139);
            this.answerBox.TabIndex = 12;
            this.answerBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 165);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Answer";
            // 
            // selectLabel
            // 
            this.selectLabel.AutoSize = true;
            this.selectLabel.Location = new System.Drawing.Point(15, 184);
            this.selectLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.selectLabel.Name = "selectLabel";
            this.selectLabel.Size = new System.Drawing.Size(75, 13);
            this.selectLabel.TabIndex = 14;
            this.selectLabel.Text = "Select a ticket";
            // 
            // answerButton
            // 
            this.answerButton.Location = new System.Drawing.Point(58, 362);
            this.answerButton.Margin = new System.Windows.Forms.Padding(2);
            this.answerButton.Name = "answerButton";
            this.answerButton.Size = new System.Drawing.Size(73, 29);
            this.answerButton.TabIndex = 15;
            this.answerButton.Text = "Submit";
            this.answerButton.UseVisualStyleBackColor = true;
            this.answerButton.Click += new System.EventHandler(this.SubmitAnswer);
            // 
            // ITPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 571);
            this.Controls.Add(this.answerButton);
            this.Controls.Add(this.selectLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.answerBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ticketsCB);
            this.Controls.Add(this.usersCB);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ITPage";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox usersCB;
        private System.Windows.Forms.ComboBox ticketsCB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox answerBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label selectLabel;
        private System.Windows.Forms.Button answerButton;
    }
}
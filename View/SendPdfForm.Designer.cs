namespace AbstractPizzeriaView
{
    partial class SendPdfForm
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
            this.comboBoxMails = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSendMail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxMails
            // 
            this.comboBoxMails.FormattingEnabled = true;
            this.comboBoxMails.Location = new System.Drawing.Point(127, 46);
            this.comboBoxMails.Name = "comboBoxMails";
            this.comboBoxMails.Size = new System.Drawing.Size(263, 24);
            this.comboBoxMails.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "E-mail";
            // 
            // buttonSendMail
            // 
            this.buttonSendMail.Location = new System.Drawing.Point(127, 102);
            this.buttonSendMail.Name = "buttonSendMail";
            this.buttonSendMail.Size = new System.Drawing.Size(175, 36);
            this.buttonSendMail.TabIndex = 2;
            this.buttonSendMail.Text = "Отправить";
            this.buttonSendMail.UseVisualStyleBackColor = true;
            this.buttonSendMail.Click += new System.EventHandler(this.buttonSendMail_Click);
            // 
            // SendPdfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 171);
            this.Controls.Add(this.buttonSendMail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMails);
            this.Name = "SendPdfForm";
            this.Text = "Отправка PDF";
            this.Load += new System.EventHandler(this.SendPdfForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSendMail;
    }
}
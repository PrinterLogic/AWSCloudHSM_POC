
namespace AWSCloudHSM_POC
{
    partial class Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.fdFile = new System.Windows.Forms.OpenFileDialog();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_pkcs = new System.Windows.Forms.TextBox();
            this.btn_pkcs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_certificate = new System.Windows.Forms.TextBox();
            this.btn_certificate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "File to Encrypt:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(117, 24);
            this.txtFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(444, 27);
            this.txtFile.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(572, 24);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 31);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(271, 199);
            this.btnEncrypt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(133, 40);
            this.btnEncrypt.TabIndex = 3;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "PIN:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPin
            // 
            this.txtPin.Location = new System.Drawing.Point(117, 147);
            this.txtPin.Name = "txtPin";
            this.txtPin.PasswordChar = '*';
            this.txtPin.Size = new System.Drawing.Size(444, 27);
            this.txtPin.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "PKCS11 Library:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_pkcs
            // 
            this.txt_pkcs.Location = new System.Drawing.Point(117, 64);
            this.txt_pkcs.Name = "txt_pkcs";
            this.txt_pkcs.Size = new System.Drawing.Size(444, 27);
            this.txt_pkcs.TabIndex = 7;
            // 
            // btn_pkcs
            // 
            this.btn_pkcs.Location = new System.Drawing.Point(572, 63);
            this.btn_pkcs.Name = "btn_pkcs";
            this.btn_pkcs.Size = new System.Drawing.Size(81, 29);
            this.btn_pkcs.TabIndex = 8;
            this.btn_pkcs.Text = "Browse...";
            this.btn_pkcs.UseVisualStyleBackColor = true;
            this.btn_pkcs.Click += new System.EventHandler(this.btn_pkcs_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Certificate:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_certificate
            // 
            this.txt_certificate.Location = new System.Drawing.Point(118, 104);
            this.txt_certificate.Name = "txt_certificate";
            this.txt_certificate.Size = new System.Drawing.Size(443, 27);
            this.txt_certificate.TabIndex = 10;
            // 
            // btn_certificate
            // 
            this.btn_certificate.Location = new System.Drawing.Point(572, 101);
            this.btn_certificate.Name = "btn_certificate";
            this.btn_certificate.Size = new System.Drawing.Size(81, 29);
            this.btn_certificate.TabIndex = 11;
            this.btn_certificate.Text = "Browse...";
            this.btn_certificate.UseVisualStyleBackColor = true;
            this.btn_certificate.Click += new System.EventHandler(this.btn_certificate_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 267);
            this.Controls.Add(this.btn_certificate);
            this.Controls.Add(this.txt_certificate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_pkcs);
            this.Controls.Add(this.txt_pkcs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog fdFile;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_pkcs;
        private System.Windows.Forms.Button btn_pkcs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_certificate;
        private System.Windows.Forms.Button btn_certificate;
    }
}
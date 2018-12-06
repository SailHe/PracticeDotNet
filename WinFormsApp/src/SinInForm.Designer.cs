namespace WinFormsApp.src
{
    partial class SinInForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.stuSignIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.signPswTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(410, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.signButton_Click);
            // 
            // stuSignIdTextBox
            // 
            this.stuSignIdTextBox.Location = new System.Drawing.Point(342, 224);
            this.stuSignIdTextBox.Name = "stuSignIdTextBox";
            this.stuSignIdTextBox.Size = new System.Drawing.Size(228, 25);
            this.stuSignIdTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "学号";
            // 
            // signPswTextBox
            // 
            this.signPswTextBox.Location = new System.Drawing.Point(342, 272);
            this.signPswTextBox.Name = "signPswTextBox";
            this.signPswTextBox.PasswordChar = '*';
            this.signPswTextBox.Size = new System.Drawing.Size(228, 25);
            this.signPswTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码";
            // 
            // SinInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 540);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.signPswTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stuSignIdTextBox);
            this.Controls.Add(this.button1);
            this.Name = "SinInForm";
            this.Text = "SinInForm";
            this.Load += new System.EventHandler(this.SinInForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox stuSignIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox signPswTextBox;
        private System.Windows.Forms.Label label2;
    }
}
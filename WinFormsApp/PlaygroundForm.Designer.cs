namespace WinFormsApp
{
    partial class PlaygroundForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bigNumPlushbutton = new System.Windows.Forms.Button();
            this.lhsBigNum_textBox = new System.Windows.Forms.TextBox();
            this.rhsBigNum_textBox = new System.Windows.Forms.TextBox();
            this.bigNumSum_textBox = new System.Windows.Forms.TextBox();
            this.bigNumRadix_textBox = new System.Windows.Forms.TextBox();
            this.APItestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bigNumPlushbutton
            // 
            this.bigNumPlushbutton.Location = new System.Drawing.Point(211, 50);
            this.bigNumPlushbutton.Name = "bigNumPlushbutton";
            this.bigNumPlushbutton.Size = new System.Drawing.Size(103, 23);
            this.bigNumPlushbutton.TabIndex = 0;
            this.bigNumPlushbutton.Text = "大数加法";
            this.bigNumPlushbutton.UseVisualStyleBackColor = true;
            this.bigNumPlushbutton.Click += new System.EventHandler(this.bigNumPlushbutton_Click);
            // 
            // lhsBigNum_textBox
            // 
            this.lhsBigNum_textBox.Location = new System.Drawing.Point(12, 34);
            this.lhsBigNum_textBox.Name = "lhsBigNum_textBox";
            this.lhsBigNum_textBox.Size = new System.Drawing.Size(179, 25);
            this.lhsBigNum_textBox.TabIndex = 1;
            this.lhsBigNum_textBox.TextChanged += new System.EventHandler(this.lhsNum_TextChanged);
            // 
            // textBox2
            // 
            this.rhsBigNum_textBox.Location = new System.Drawing.Point(13, 65);
            this.rhsBigNum_textBox.Name = "textBox2";
            this.rhsBigNum_textBox.Size = new System.Drawing.Size(178, 25);
            this.rhsBigNum_textBox.TabIndex = 2;
            this.rhsBigNum_textBox.TextChanged += new System.EventHandler(this.rhsNum_TextChanged);
            // 
            // bigNumSum_textBox
            // 
            this.bigNumSum_textBox.Location = new System.Drawing.Point(343, 48);
            this.bigNumSum_textBox.Name = "bigNumSum_textBox";
            this.bigNumSum_textBox.Size = new System.Drawing.Size(445, 25);
            this.bigNumSum_textBox.TabIndex = 3;
            // 
            // bigNumRadix_textBox
            // 
            this.bigNumRadix_textBox.Location = new System.Drawing.Point(127, 4);
            this.bigNumRadix_textBox.Name = "bigNumRadix_textBox";
            this.bigNumRadix_textBox.Size = new System.Drawing.Size(64, 25);
            this.bigNumRadix_textBox.TabIndex = 5;
            this.bigNumRadix_textBox.Text = "10";
            // 
            // APItestButton
            // 
            this.APItestButton.Location = new System.Drawing.Point(13, 3);
            this.APItestButton.Name = "APItestButton";
            this.APItestButton.Size = new System.Drawing.Size(106, 23);
            this.APItestButton.TabIndex = 6;
            this.APItestButton.Text = "API test";
            this.APItestButton.UseVisualStyleBackColor = true;
            this.APItestButton.Click += new System.EventHandler(this.APItestButton_Click);
            // 
            // PlaygroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 450);
            this.Controls.Add(this.APItestButton);
            this.Controls.Add(this.bigNumRadix_textBox);
            this.Controls.Add(this.bigNumSum_textBox);
            this.Controls.Add(this.rhsBigNum_textBox);
            this.Controls.Add(this.lhsBigNum_textBox);
            this.Controls.Add(this.bigNumPlushbutton);
            this.Name = "PlaygroundForm";
            this.Text = "ShellForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bigNumPlushbutton;
        private System.Windows.Forms.TextBox lhsBigNum_textBox;
        private System.Windows.Forms.TextBox rhsBigNum_textBox;
        private System.Windows.Forms.TextBox bigNumSum_textBox;
        private System.Windows.Forms.TextBox bigNumRadix_textBox;
        private System.Windows.Forms.Button APItestButton;
    }
}


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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bigNumSum_textBox = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.APItestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bigNumPlushbutton
            // 
            this.bigNumPlushbutton.Location = new System.Drawing.Point(334, 36);
            this.bigNumPlushbutton.Name = "bigNumPlushbutton";
            this.bigNumPlushbutton.Size = new System.Drawing.Size(103, 23);
            this.bigNumPlushbutton.TabIndex = 0;
            this.bigNumPlushbutton.Text = "大数加法";
            this.bigNumPlushbutton.UseVisualStyleBackColor = true;
            this.bigNumPlushbutton.Click += new System.EventHandler(this.bigNumPlushbutton_Click);
            // 
            // textBox1
            // 
            this.lhsBigNum_textBox.Location = new System.Drawing.Point(67, 34);
            this.lhsBigNum_textBox.Name = "textBox1";
            this.lhsBigNum_textBox.Size = new System.Drawing.Size(100, 25);
            this.lhsBigNum_textBox.TabIndex = 1;
            this.lhsBigNum_textBox.TextChanged += new System.EventHandler(this.lhsNum_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(201, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 25);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextChanged += new System.EventHandler(this.rhsNum_TextChanged);
            // 
            // bigNumSum_textBox
            // 
            this.bigNumSum_textBox.Location = new System.Drawing.Point(472, 34);
            this.bigNumSum_textBox.Name = "bigNumSum_textBox";
            this.bigNumSum_textBox.Size = new System.Drawing.Size(100, 25);
            this.bigNumSum_textBox.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(67, 105);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 25);
            this.textBox4.TabIndex = 5;
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
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.APItestButton);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.bigNumSum_textBox);
            this.Controls.Add(this.textBox2);
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox bigNumSum_textBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button APItestButton;
    }
}


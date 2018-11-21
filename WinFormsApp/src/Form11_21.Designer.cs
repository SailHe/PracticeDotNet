namespace WinFormsApp.src
{
    partial class Form11_21
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
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.textBox_nameSearch = new System.Windows.Forms.TextBox();
            this.button_nameSearch = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_stuNum = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_sex = new System.Windows.Forms.TextBox();
            this.textBox_birthDay = new System.Windows.Forms.TextBox();
            this.textBox_grade = new System.Windows.Forms.TextBox();
            this.label_stuNum = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.label_sex = new System.Windows.Forms.Label();
            this.label_birthDay = new System.Windows.Forms.Label();
            this.label_nameSearch = new System.Windows.Forms.Label();
            this.label_grade = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainTextBox
            // 
            this.mainTextBox.Location = new System.Drawing.Point(96, 78);
            this.mainTextBox.Multiline = true;
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.Size = new System.Drawing.Size(1052, 394);
            this.mainTextBox.TabIndex = 0;
            // 
            // textBox_nameSearch
            // 
            this.textBox_nameSearch.Location = new System.Drawing.Point(504, 26);
            this.textBox_nameSearch.Name = "textBox_nameSearch";
            this.textBox_nameSearch.Size = new System.Drawing.Size(175, 25);
            this.textBox_nameSearch.TabIndex = 1;
            this.textBox_nameSearch.Text = "输入姓名";
            // 
            // button_nameSearch
            // 
            this.button_nameSearch.BackColor = System.Drawing.Color.Red;
            this.button_nameSearch.Location = new System.Drawing.Point(795, 26);
            this.button_nameSearch.Name = "button_nameSearch";
            this.button_nameSearch.Size = new System.Drawing.Size(95, 23);
            this.button_nameSearch.TabIndex = 2;
            this.button_nameSearch.Text = "按姓名搜索";
            this.button_nameSearch.UseVisualStyleBackColor = false;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(456, 646);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(113, 31);
            this.button_add.TabIndex = 3;
            this.button_add.Text = "增加成员";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(677, 646);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(121, 31);
            this.button_save.TabIndex = 4;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            // 
            // textBox_stuNum
            // 
            this.textBox_stuNum.Location = new System.Drawing.Point(392, 524);
            this.textBox_stuNum.Name = "textBox_stuNum";
            this.textBox_stuNum.Size = new System.Drawing.Size(139, 25);
            this.textBox_stuNum.TabIndex = 5;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(559, 524);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(141, 25);
            this.textBox_name.TabIndex = 6;
            // 
            // textBox_sex
            // 
            this.textBox_sex.Location = new System.Drawing.Point(729, 524);
            this.textBox_sex.Name = "textBox_sex";
            this.textBox_sex.Size = new System.Drawing.Size(141, 25);
            this.textBox_sex.TabIndex = 7;
            // 
            // textBox_birthDay
            // 
            this.textBox_birthDay.Location = new System.Drawing.Point(392, 599);
            this.textBox_birthDay.Name = "textBox_birthDay";
            this.textBox_birthDay.Size = new System.Drawing.Size(247, 25);
            this.textBox_birthDay.TabIndex = 8;
            // 
            // textBox_grade
            // 
            this.textBox_grade.Location = new System.Drawing.Point(645, 599);
            this.textBox_grade.Name = "textBox_grade";
            this.textBox_grade.Size = new System.Drawing.Size(225, 25);
            this.textBox_grade.TabIndex = 9;
            // 
            // label_stuNum
            // 
            this.label_stuNum.AutoSize = true;
            this.label_stuNum.Location = new System.Drawing.Point(414, 506);
            this.label_stuNum.Name = "label_stuNum";
            this.label_stuNum.Size = new System.Drawing.Size(37, 15);
            this.label_stuNum.TabIndex = 10;
            this.label_stuNum.Text = "学号";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(601, 506);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(37, 15);
            this.label_name.TabIndex = 11;
            this.label_name.Text = "姓名";
            // 
            // label_sex
            // 
            this.label_sex.AutoSize = true;
            this.label_sex.Location = new System.Drawing.Point(792, 506);
            this.label_sex.Name = "label_sex";
            this.label_sex.Size = new System.Drawing.Size(37, 15);
            this.label_sex.TabIndex = 12;
            this.label_sex.Text = "性别";
            // 
            // label_birthDay
            // 
            this.label_birthDay.AutoSize = true;
            this.label_birthDay.Location = new System.Drawing.Point(466, 578);
            this.label_birthDay.Name = "label_birthDay";
            this.label_birthDay.Size = new System.Drawing.Size(67, 15);
            this.label_birthDay.TabIndex = 13;
            this.label_birthDay.Text = "出生日期";
            // 
            // label_nameSearch
            // 
            this.label_nameSearch.AutoSize = true;
            this.label_nameSearch.Location = new System.Drawing.Point(556, 9);
            this.label_nameSearch.Name = "label_nameSearch";
            this.label_nameSearch.Size = new System.Drawing.Size(37, 15);
            this.label_nameSearch.TabIndex = 14;
            this.label_nameSearch.Text = "姓名";
            // 
            // label_grade
            // 
            this.label_grade.AutoSize = true;
            this.label_grade.Location = new System.Drawing.Point(690, 578);
            this.label_grade.Name = "label_grade";
            this.label_grade.Size = new System.Drawing.Size(37, 15);
            this.label_grade.TabIndex = 15;
            this.label_grade.Text = "班级";
            // 
            // Form11_21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 707);
            this.Controls.Add(this.label_grade);
            this.Controls.Add(this.label_nameSearch);
            this.Controls.Add(this.label_birthDay);
            this.Controls.Add(this.label_sex);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_stuNum);
            this.Controls.Add(this.textBox_grade);
            this.Controls.Add(this.textBox_birthDay);
            this.Controls.Add(this.textBox_sex);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.textBox_stuNum);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.button_nameSearch);
            this.Controls.Add(this.textBox_nameSearch);
            this.Controls.Add(this.mainTextBox);
            this.Name = "Form11_21";
            this.Text = "Form11_21";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mainTextBox;
        private System.Windows.Forms.TextBox textBox_nameSearch;
        private System.Windows.Forms.Button button_nameSearch;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textBox_stuNum;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_sex;
        private System.Windows.Forms.TextBox textBox_birthDay;
        private System.Windows.Forms.TextBox textBox_grade;
        private System.Windows.Forms.Label label_stuNum;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_sex;
        private System.Windows.Forms.Label label_birthDay;
        private System.Windows.Forms.Label label_nameSearch;
        private System.Windows.Forms.Label label_grade;
    }
}
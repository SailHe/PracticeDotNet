namespace WinFormsApp.src
{
    partial class Form12_6
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
            this.button_delete = new System.Windows.Forms.Button();
            this.listBox_main = new System.Windows.Forms.ListBox();
            this.label_grade = new System.Windows.Forms.Label();
            this.label_nameSearch = new System.Windows.Forms.Label();
            this.label_birthDay = new System.Windows.Forms.Label();
            this.label_sex = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.label_stuNum = new System.Windows.Forms.Label();
            this.textBox_grade = new System.Windows.Forms.TextBox();
            this.textBox_birthDay = new System.Windows.Forms.TextBox();
            this.textBox_sex = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_stuNum = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.button_nameSearch = new System.Windows.Forms.Button();
            this.textBox_nameSearch = new System.Windows.Forms.TextBox();
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.editButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(1300, 744);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(110, 32);
            this.button_delete.TabIndex = 35;
            this.button_delete.Text = "删除";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // listBox_main
            // 
            this.listBox_main.FormattingEnabled = true;
            this.listBox_main.ItemHeight = 15;
            this.listBox_main.Location = new System.Drawing.Point(88, 65);
            this.listBox_main.Name = "listBox_main";
            this.listBox_main.Size = new System.Drawing.Size(1252, 469);
            this.listBox_main.TabIndex = 34;
            this.listBox_main.SelectedIndexChanged += new System.EventHandler(this.listBox_main_SelectedIndexChanged);
            // 
            // label_grade
            // 
            this.label_grade.AutoSize = true;
            this.label_grade.Location = new System.Drawing.Point(1195, 619);
            this.label_grade.Name = "label_grade";
            this.label_grade.Size = new System.Drawing.Size(37, 15);
            this.label_grade.TabIndex = 33;
            this.label_grade.Text = "班级";
            // 
            // label_nameSearch
            // 
            this.label_nameSearch.AutoSize = true;
            this.label_nameSearch.Location = new System.Drawing.Point(577, -1);
            this.label_nameSearch.Name = "label_nameSearch";
            this.label_nameSearch.Size = new System.Drawing.Size(37, 15);
            this.label_nameSearch.TabIndex = 32;
            this.label_nameSearch.Text = "姓名";
            // 
            // label_birthDay
            // 
            this.label_birthDay.AutoSize = true;
            this.label_birthDay.Location = new System.Drawing.Point(971, 619);
            this.label_birthDay.Name = "label_birthDay";
            this.label_birthDay.Size = new System.Drawing.Size(67, 15);
            this.label_birthDay.TabIndex = 31;
            this.label_birthDay.Text = "出生日期";
            // 
            // label_sex
            // 
            this.label_sex.AutoSize = true;
            this.label_sex.Location = new System.Drawing.Point(1297, 547);
            this.label_sex.Name = "label_sex";
            this.label_sex.Size = new System.Drawing.Size(37, 15);
            this.label_sex.TabIndex = 30;
            this.label_sex.Text = "性别";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(1106, 547);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(37, 15);
            this.label_name.TabIndex = 29;
            this.label_name.Text = "姓名";
            // 
            // label_stuNum
            // 
            this.label_stuNum.AutoSize = true;
            this.label_stuNum.Location = new System.Drawing.Point(919, 547);
            this.label_stuNum.Name = "label_stuNum";
            this.label_stuNum.Size = new System.Drawing.Size(37, 15);
            this.label_stuNum.TabIndex = 28;
            this.label_stuNum.Text = "学号";
            // 
            // textBox_grade
            // 
            this.textBox_grade.Location = new System.Drawing.Point(1150, 640);
            this.textBox_grade.Name = "textBox_grade";
            this.textBox_grade.Size = new System.Drawing.Size(225, 25);
            this.textBox_grade.TabIndex = 27;
            // 
            // textBox_birthDay
            // 
            this.textBox_birthDay.Location = new System.Drawing.Point(897, 640);
            this.textBox_birthDay.Name = "textBox_birthDay";
            this.textBox_birthDay.Size = new System.Drawing.Size(247, 25);
            this.textBox_birthDay.TabIndex = 26;
            // 
            // textBox_sex
            // 
            this.textBox_sex.Location = new System.Drawing.Point(1234, 565);
            this.textBox_sex.Name = "textBox_sex";
            this.textBox_sex.Size = new System.Drawing.Size(141, 25);
            this.textBox_sex.TabIndex = 25;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(1064, 565);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(141, 25);
            this.textBox_name.TabIndex = 24;
            // 
            // textBox_stuNum
            // 
            this.textBox_stuNum.Location = new System.Drawing.Point(897, 565);
            this.textBox_stuNum.Name = "textBox_stuNum";
            this.textBox_stuNum.Size = new System.Drawing.Size(139, 25);
            this.textBox_stuNum.TabIndex = 23;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(1289, 687);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(121, 31);
            this.button_save.TabIndex = 22;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(1003, 687);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(113, 31);
            this.button_add.TabIndex = 21;
            this.button_add.Text = "增加";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_nameSearch
            // 
            this.button_nameSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button_nameSearch.Location = new System.Drawing.Point(817, 18);
            this.button_nameSearch.Name = "button_nameSearch";
            this.button_nameSearch.Size = new System.Drawing.Size(95, 23);
            this.button_nameSearch.TabIndex = 20;
            this.button_nameSearch.Text = "按姓名搜索";
            this.button_nameSearch.UseVisualStyleBackColor = false;
            // 
            // textBox_nameSearch
            // 
            this.textBox_nameSearch.Location = new System.Drawing.Point(513, 19);
            this.textBox_nameSearch.Name = "textBox_nameSearch";
            this.textBox_nameSearch.Size = new System.Drawing.Size(175, 25);
            this.textBox_nameSearch.TabIndex = 19;
            this.textBox_nameSearch.Text = "输入姓名";
            // 
            // mainTextBox
            // 
            this.mainTextBox.Location = new System.Drawing.Point(43, 547);
            this.mainTextBox.Multiline = true;
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.Size = new System.Drawing.Size(804, 179);
            this.mainTextBox.TabIndex = 18;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(1150, 687);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(113, 31);
            this.editButton.TabIndex = 36;
            this.editButton.Text = "编辑";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // Form12_6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1437, 788);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.listBox_main);
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
            this.Name = "Form12_6";
            this.Text = "Form12_6";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.ListBox listBox_main;
        private System.Windows.Forms.Label label_grade;
        private System.Windows.Forms.Label label_nameSearch;
        private System.Windows.Forms.Label label_birthDay;
        private System.Windows.Forms.Label label_sex;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_stuNum;
        private System.Windows.Forms.TextBox textBox_grade;
        private System.Windows.Forms.TextBox textBox_birthDay;
        private System.Windows.Forms.TextBox textBox_sex;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_stuNum;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_nameSearch;
        private System.Windows.Forms.TextBox textBox_nameSearch;
        private System.Windows.Forms.TextBox mainTextBox;
        private System.Windows.Forms.Button editButton;
    }
}
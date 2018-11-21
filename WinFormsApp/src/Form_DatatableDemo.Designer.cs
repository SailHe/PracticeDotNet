using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.SqlClient;


namespace WinFormsApp.src
{
    partial class Form_DatatableDemo
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form_DatatableDemo";
        }

        #endregion
    }
}
/*
namespace WinFormsApp.src
{
    public partial class Frm_Main : Form
    {
        public static string str = "";
        public Frm_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示员工信息
        /// </summary>
        private void showinf()
        {
            using (SqlConnection con = new SqlConnection(//创建数据连接对象
@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\DataBase\db_TomeTwo.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
            {
                DataTable dt = new DataTable();//创建数据表
                SqlDataAdapter da = new SqlDataAdapter(//创建数据适配器对象
                    "select * from 员工表", con);
                da.Fill(dt);//填充数据表
                this.dgv_Message.DataSource = dt.DefaultView;//设置数据源
            }
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            showinf();//显示员工信息
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要删除本条信息吗？", "提示",//判断是否删除员工信息
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (str != "")
                {
                    using (SqlConnection con = new SqlConnection(//创建数据库连接对象
@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\DataBase\db_TomeTwo.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
                    {
                        con.Open();//打开数据库连接
                        SqlCommand cmd = new SqlCommand(//创建命令对象
                            "delete from 员工表 where 员工编号='" + str + "'", con);
                        cmd.Connection = con;//设置连接属性
                        cmd.ExecuteNonQuery();//执行SQL语句
                        con.Close();//关闭数据库连接
                        showinf();//显示员工信息
                        MessageBox.Show("删除成功");//弹出消息对话框
                    }
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();//关闭窗体
        }

        private void dgv_Message_Click(object sender, EventArgs e)
        {
            str = this.dgv_Message.SelectedCells[0].Value.ToString();//得到员工编号
        }
    }
}
*/
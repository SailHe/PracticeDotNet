﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.App_Start.App_Pages
{
    public partial class crudWebForm : System.Web.UI.Page
    {
        /// @see https://blog.csdn.net/lqadam/article/details/50865024
        /// 设计时在*.aspx下的design右键选择查看设计器即可(Shift + F7)
        //数据连接最基本需要的两个对象
        private MySqlConnection conn = null;
        private MySqlCommand cmd = null;
        //private SqlDataAdapter adapter = null;
        //为了方便，设为全局对象的sql语句
        private string sql = null;

        private void InitializeComponent()
        {

        }

        //页面加载时ASP.NET首先会调用这个方法
        protected void Page_Load(object sender, EventArgs e)
        {
            //如果页面不是刷新，则执行，这个很重要
            if (!IsPostBack) {
                load();
            }
        }

        //公用 打开数据库的方法
        public void openDatabase()
        {
            conn = new MySqlConnection("server=localhost;user id=sailhe;password=123456@password;database=sail_he");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                Response.Write("<script>console.log('Connected!');</script>");
            }
        }
        //封装的数据库语句执行的方法
        public void execute(String sql)
        {
            openDatabase();
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //默认加载页面的方法 找到年龄最大的加载
        //有些问题，年龄不能相同，加载中前台的textbox里只能显示一条记录，数据拿到之后有多条只显示一条
        public void load()
        {
            openDatabase();
            cmd = new MySqlCommand("select user_name, user_age from sys_user where user_age=(select max(user_age) from sys_user)", conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                nameTextBox.Text = (String)dr[0].ToString().Trim();
                ageTextBox.Text = (String)dr[1].ToString().Trim();
            }
            conn.Close();
        }
        //根据sql语句加载信息，重载两个textbox
        public void load(String sql)
        {
            openDatabase();
            cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nameTextBox.Text = (String)dr[0].ToString().Trim();
                ageTextBox.Text = (String)dr[1].ToString().Trim();
            }
            conn.Close();
        }

        //四个按钮的方法，增删改查
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            sql = "insert into sys_user(user_name, user_age) values('" + nameTextBox.Text.ToString().Trim() + "','" + ageTextBox.Text.ToString().Trim() + "')";
            execute(sql);
            Response.Write("<script>console.log('添加成功');</script>");
            getData();
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            sql = "delete from sys_user where user_name='" + nameTextBox.Text.ToString().Trim() + "' and user_age='" + ageTextBox.Text.ToString().Trim() + "'";
            execute(sql);
            load();
            Response.Write("<script>console.log('删除成功');</script>");
            getData();
        }
        
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            sql = "update sys_user set user_age='" + ageTextBox.Text.ToString().Trim() + "' where user_name='" + nameTextBox.Text.ToString().Trim() + "'";
            execute(sql);
            Response.Write("<script>console.log('更新成功');</script>");
            getData();
        }
        
        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            sql = "select user_name, user_age from sys_user where user_name='" + nameTextBox.Text.ToString().Trim() + "'";
            load(sql);
            Response.Write("<script>console.log('查询成功');</script>");
        }

        protected void ageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void getData()
        {
            openDatabase();
            sql = "select * from sys_user where user_name like '%" + queryTextBox.Text.ToString().Trim() + "%'";
            cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    nameTextBox.Text = (String)dr[1].ToString().Trim();
            //    ageTextBox.Text = (String)dr[4].ToString().Trim();
            //}
            // 不能先read
            GridView1.DataSource = dr;
            GridView1.DataSourceID = "";
            GridView1.DataBind();
            conn.Clone();
            Response.Write("<script>console.log('查询成功');</script>");
        }

        protected void queryButton_Click(object sender, EventArgs e)
        {
            getData();
        }
    }
}
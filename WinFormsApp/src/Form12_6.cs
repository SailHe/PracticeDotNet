using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Entity.Infrastructure;
using SailHeCSharpClassLib;

namespace WinFormsApp.src
{
    public partial class Form12_6 : Form
    {
        // partial 局部类型的概念。局部类型允许我们将一个类、结构或接口分成几个部分，分别实现在几个不同的.cs文件中。
        class StuDTO
        {
            public string sname { get; set; }
            public int sid { get; set; }
        }

        class StuScoreDTO
        {
            // 妈哒(mada) ! 中文编程!
            public string sname { get; set; }
            public int sid { get; set; }
            public string 姓名 { get; set; }
            public string 学号 { get; set; }
            public string 性别 { get; set; }
            public string 出生日期 { get; set; }
            public string 班级 { get; set; }
            public string 电话 { get; set; }
            public string 成绩 { get; set; }

            override
            public string ToString()
            {
                // StudentInfo;
                return 姓名 + "\t"
                + (学号 == null ? "---------" : 学号.ToString())
                + "\t" + 性别
                + "\t\t" + 出生日期 
                + "\t" + 班级
                + "\t" + 电话
                + "\t" + 成绩;
            }
        }

        private int stuCount = 0;

        public Form12_6()
        {
            InitializeComponent();

            initMainListBox();
        }

        private void initMainListBox()
        {
            string sql = "SELECT stu.sid AS 学号, stu.sname AS 姓名, stu.ssexy AS 性别" +
                ", stu.sbdate AS 出生日期, ug.gname AS 班级, stu.stele AS 电话" +
                ", sc.score1 AS 成绩 FROM ustudent stu INNER JOIN ugrade ug USING(gid) LEFT JOIN usc sc USING(sid)";

            DbRawSqlQuery<StuScoreDTO> results = PlaygroundForm.dbContext.Database.SqlQuery<StuScoreDTO>(
                    sql, new MySqlParameter("@sid", 32006005)
                );
            var resultList = results.ToList<StuScoreDTO>();
            stuCount = resultList.Count;
            foreach (var r in results)
            {
                // 显示时会调用ToString方法
                listBox_main.Items.Add(r);
            }
        }

        private void listBox_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            object temp = listBox_main.SelectedItems[0];
            mainTextBox.AppendText(temp.ToString() + "\r\n");
            int index = (sender as ListBox).SelectedIndex;
            if (index >= stuCount || index < 0)
            {
                // do nothing
            }
            else
            {
                StuScoreDTO seItem = temp as StuScoreDTO;
                textBox_name.Text = seItem.姓名;
                textBox_grade.Text = seItem.班级;
                textBox_birthDay.Text = seItem.出生日期;
                textBox_stuNum.Text = seItem.学号;
                textBox_sex.Text = seItem.性别;
            }
        }

        /// <summary>
        /// Shell输出换行
        /// </summary>
        /// <param name="msg"></param>
        void TipsWriteLine(string msg)
        {
            mainTextBox.AppendText(msg + "\r\n");
        }

        /// <summary>
        /// 清空mainTextBox
        /// </summary>
        void Clear()
        {
            listBox_main.Items.Clear();
            mainTextBox.Text = "";
        }

        void LINKQSample1()
        {
            // lambda表达式版 https://zh.wikipedia.org/wiki/%E8%AF%AD%E8%A8%80%E9%9B%86%E6%88%90%E6%9F%A5%E8%AF%A2
            new LINQEF.LINQExam().queryScoreWithFunc("12005001");

            IQueryable<ustudent> custQuery =
            from row in PlaygroundForm.dbContext.ustudent
            where row.sid == 32006005
            select row;

            foreach (var r in custQuery)
            {
                Console.WriteLine(r.sname);
            }

            // 官网的蜜汁版本
            // 需添加 System.Data.Linq 程序集引用 @see https://social.msdn.microsoft.com/Forums/zh-TW/16be9e61-651b-4f08-b718-906ba6223446/linq252141998121040systemdatalinqmapping?forum=238s
            // var db = new System.Data.Linq.DataContext(@"./LearnDB_Data.MDF");
            // IEnumerable<ustudent> results = db.ExecuteQuery<ustudent>(@"SELECT * FROM ustudent");
            // @see https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/sql/linq/walkthrough-simple-object-model-and-query-csharp
        }
        /**
         * EF提供一组方法用来执行原生的SQL.
         * 原生SQL执行查询有以下三种:
         * 1.DbSet.SqlQuery -> 需要返回实体模型（context会跟踪，等效于LINQ方式）
         * 2.Database.SqlQuery -> 需要返回其他类型
         * 3.Database.ExecuteSqlCommand -> 原生SQL执行更新
         * 来源@see http://www.voidcn.com/article/p-qjdrcntb-xv.html
         * 详情参考VS本地API说明
         * 以下分别演示三种用法
        **/
        void LINKQ_NaiveSQL_Sample1()
        {
            // 只能返回ustudent实体
            var results = PlaygroundForm.dbContext.ustudent.SqlQuery
                ("SELECT * FROM ustudent");
            foreach (var r in results)
            {
                Console.WriteLine(r.ToString());
            }
        }
        void LINKQ_NaiveSQL_Sample2()
        {
            // 如果返回复杂的结构需要自行定义
            DbRawSqlQuery<string> results = PlaygroundForm.dbContext.Database.SqlQuery<string>(
                    "SELECT sname FROM ustudent WHERE sid = @sid", new MySqlParameter("@sid", 32006005)
                );
            foreach (var r in results)
            {
                Console.WriteLine(r.ToString());
            }

            // 自定义类型需要geter和seter
            DbRawSqlQuery<StuDTO> resultDataSet = PlaygroundForm.dbContext.Database.SqlQuery<StuDTO>(
                    "SELECT sname, sid FROM ustudent WHERE sid = @sid", new MySqlParameter("@sid", 32006005)
                );
            var temp = resultDataSet.ToList<StuDTO>();
            // 若包涵多个元素 会抛异常 详参API
            // var temp1 = resultDataSet.SingleOrDefault();

            foreach (var r in resultDataSet)
            {
                Console.WriteLine(r.ToString());
            }
        }

        ustudent readAStudent()
        {
            bool isVerifyed = true;
            string sex;
            DateTime birthDay;
            string className, phone = "15258989595", stuNameBuffer, birthDayBuffer;

            stuNameBuffer = textBox_name.Text;
            className = textBox_grade.Text;
            birthDayBuffer = textBox_birthDay.Text;
            sex = textBox_sex.Text;

            // 这个返回的是个list<bool>貌似
            // var  selectGrade = PlaygroundForm.dbContext.ugrade.Select(ele => ele.gname == className).ToList();
            IQueryable<ugrade> queryResult =
                from row in PlaygroundForm.dbContext.ugrade
                where row.gname == className
                select row;
            List<ugrade> selectGrade = queryResult.ToList();
            if (selectGrade.Count == 0)
            {
                TipsWriteLine("班级不存在!请重输");
                isVerifyed = false;
            }
            else
            {
                // donothing
            }
            
            if (Verify.IsDateTime(birthDayBuffer))
            {
                //break;
            }
            else
            {
                TipsWriteLine("生日格式错误!请重输");
                isVerifyed = false;
            }
            birthDay = DateTime.Parse(birthDayBuffer);

            if (Verify.IsPhoneNumber(phone))
            {
                //break;
            }
            else
            {
                TipsWriteLine("电话格式错误!请重输");
                isVerifyed = false;
            }
            ustudent result = null;
            if (isVerifyed)
            {
                TipsWriteLine("添加输入成功!");
                result = new ustudent();
                result.gid = selectGrade.First().gid.ToString();
                result.sname = stuNameBuffer;
                result.ssexy = sex;
                result.sbdate = birthDay.ToLongDateString();
                result.stele = phone;
            }
            else
            {
                // do nothing
            }
            TipsWriteLine("================");
            return result;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            ustudent delete = new ustudent();
            delete.sid = int.Parse(textBox_stuNum.Text);
            try
            {
                PlaygroundForm.dbContext.Entry(delete).State = System.Data.Entity.EntityState.Deleted;
            }
            catch {
                MessageBox.Show("不要对同一个实体 同时进行编辑或者删除操作! 请先提交!");
            }
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            var temp = readAStudent();
            if (temp == null)
            {
                // do nothing
            }
            else
            {
                var editEntity = PlaygroundForm.dbContext.ustudent
                    .Where<ustudent>(ele => ele.sid.ToString() == textBox_stuNum.Text).SingleOrDefault();
                editEntity.gid = temp.gid;
                // editEntity.sid = temp.sid;
                editEntity.sname = temp.sname;
                editEntity.sbdate = temp.sbdate;
                editEntity.ssexy = temp.ssexy;
                editEntity.stele = temp.stele;
                TipsWriteLine("已暂存更改!");
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            var temp = readAStudent();
            if(temp == null)
            {
                // do nothing
            }
            else
            {
                PlaygroundForm.dbContext.ustudent.Add(temp);
                TipsWriteLine("已暂存更改!");
            }
        }

        private void button_submit_Click(object sender, EventArgs e)
        {
            Clear();
            PlaygroundForm.dbContext.SaveChanges();
            initMainListBox();
            TipsWriteLine("已提交所有更改 并重载!");
        }
    }
}

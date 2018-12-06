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

        public Form12_6()
        {
            InitializeComponent();
            
            // 需添加 System.Data.Linq 程序集引用 @see https://social.msdn.microsoft.com/Forums/zh-TW/16be9e61-651b-4f08-b718-906ba6223446/linq252141998121040systemdatalinqmapping?forum=238s
            // var db = new System.Data.Linq.DataContext(@"./LearnDB_Data.MDF");
            // IEnumerable<ustudent> results = db.ExecuteQuery<ustudent>(@"SELECT * FROM ustudent");
            
            string sql = "SELECT stu.sid AS 学号, stu.sname AS 姓名, stu.ssexy AS 性别" +
                ", stu.sbdate AS 出生日期, ug.gname AS 班级, stu.stele AS 电话" +
                ", sc.score1 AS 成绩 FROM ustudent stu INNER JOIN ugrade ug USING(gid) JOIN usc sc USING(sid)";
            // 自定义类型需要geter和seter
            DbRawSqlQuery<StuDTO> results = PlaygroundForm.dbContext.Database.SqlQuery<StuDTO>(
                    "SELECT sname, sid FROM ustudent WHERE sid = @sid", new MySqlParameter("@sid", 32006005)
                );
            var temp = results.ToList<StuDTO>();
            // 若包涵多个元素 会抛异常 详参API
            // var temp1 = results.SingleOrDefault();

            foreach (var r in results)
            {
                Console.WriteLine(r.ToString());
            }
            new LINQEF.LINQExam().queryScoreWithFunc("12005001");
            listBox_main.Items.Add("ssss");
        }

        void LINKQSample1()
        {
            IQueryable<ustudent> custQuery =
            from row in PlaygroundForm.dbContext.ustudent
            where row.sid == 32006005
            select row;

            foreach (var r in custQuery)
            {
                Console.WriteLine(r.sname);
            }
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

    }
}

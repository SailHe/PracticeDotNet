using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.src
{
    using System;
    using System.Linq;
    using System.Data;

    namespace LINQEF
    {
        public class LINQExam
        {
            sail_heEntities learndb = new sail_heEntities();
            /*功能：调用LINQ方法，从数据库查询学生成绩并输出
             *参数：查询关键字；查询类型，值为0时按课程名查询，值为1时按班级名查询，其他按学号查询 
             * */
            public void queryScoreWithFunc(string key)
            {
                var result = learndb.usc.Where(o => o.sid == key);//.Select(o => new { sid = o.sid, sname = o.sname, cname = o.cname, score = o.score });
                
                Console.WriteLine("学号\t\t姓名\t\t课程名\t\t\t成绩");
                Console.WriteLine("--------------------------------------------------------------------");
                foreach (var r in result)
                {
                    // Console.WriteLine("{0}\t{1}\t{2}\t\t\t{3}", r.sid, r.sname, r.cname.Trim(), r.score);
                    Console.WriteLine(r.ToString());
                }
            }

            /*功能：插入一条新记录
             *参数： 
             * */
            public void EFInsertExam()
            {
                ustudent student = new ustudent();
                student.sname = "张言成";
                student.ssexy = "男";
                student.sbdate = Convert.ToDateTime("1990-10-2").ToLongDateString();
                student.gid = "02";
                student.stele = "678909";
                learndb.ustudent.Add(student);
                try
                {
                    learndb.SaveChanges();
                    Console.WriteLine("新同学插入成功");
                }
                catch (DBConcurrencyException dbe)
                {
                    Console.WriteLine("错误信息：{0}", dbe.Message);
                }
                catch (Exception dbe)
                {
                    Console.WriteLine("错误信息：{0}", dbe.Message);
                }
            }
            //读取当前对象的属性值
            public void EFReadEnitityInfo()
            {
                var result = learndb.ustudent.Where(o => o.gid == "02").First();
                result.ssexy = "无";
                var originalValue = learndb.Entry(result).Property(c => c.ssexy).OriginalValue;
                var currentValue = learndb.Entry(result).Property(c => c.ssexy).CurrentValue;
                var databaseValue = learndb.Entry(result).GetDatabaseValues().GetValue<string>("ssexy");
                Console.WriteLine("初始值为：{0}，当前值为：{1}，数据库中值为：{2}", originalValue, currentValue, databaseValue);
            }
            //修改记录
            public void EFUpdateExam()
            {
                var result = learndb.ustudent.Where(o => o.gid == "02").First();
                //result.sid = "NIT150401";
                result.sname = "李言";
                result.sbdate = Convert.ToDateTime("1999-10-2").ToLongDateString();
                result.stele = "677788";
                try
                {
                    learndb.SaveChanges();
                    Console.WriteLine("修改成功");
                }
                catch (DBConcurrencyException dbe)
                {
                    Console.WriteLine("错误信息：{0}", dbe.Message);
                }
                catch (Exception dbe)
                {
                    Console.WriteLine("错误信息：{0}", dbe.Message);
                }
            }
            //删除记录
            public void EFDeleteExam()
            {
                var result = learndb.ustudent.Where(o => o.gid == "02");
                learndb.Entry(result).State = System.Data.Entity.EntityState.Deleted;
                try
                {
                    learndb.SaveChanges();
                    Console.WriteLine("删除成功");
                }
                catch (DBConcurrencyException dbe)
                {
                    Console.WriteLine("错误信息：{0}", dbe.Message);
                }
                catch (Exception dbe)
                {
                    Console.WriteLine("错误信息：{0}", dbe.Message);
                }
            }

        }
    }

}

using SailHeCSharpClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data.Linq;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using LearnDotNet;

//@see  https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.controls.listbox?view=netframework-4.6.1
namespace WinFormsApp.src
{
    using StringMapInt = Dictionary<string, int>;
    using intMapString = Dictionary<int, string>;

    public partial class Form11_21 : Form
    {
        private List<StudentInfo> studentS;
        private StringMapInt gnameMapGid;
        private intMapString gidMapGname;

        public Form11_21()
        {
            InitializeComponent();

            // List<StudentInfo> studentS = new List<StudentInfo>();
            // intMapString gnameMapGid = null;
            gnameMapGid = calcAllClass(out this.gidMapGname);
            studentS = calcAll(this.gidMapGname);

            /// var gredeIdMapName = LearnDotNet.Program.calcAllClass(out gnameMapGid);
            /// studentS = LearnDotNet.Program.calcAll(gnameMapGid);

            ShellFor_11_21(studentS, gnameMapGid);
        }

        /// <summary>
        /// List输出换行
        /// </summary>
        /// <param name="msg"></param>
        void WriteLine(string msg)
        {
            listBox_main.Items.Add(msg);
        }

        /// <summary>
        /// Shell输出换行
        /// </summary>
        /// <param name="msg"></param>
        void TipsWriteLine(string msg)
        {
            mainTextBox.AppendText(msg + "\r\n");
        }


        string ReadLine()
        {
            return textBox_name.Text;
        }

        /// <summary>
        /// 清空mainTextBox
        /// </summary>
        void Clear()
        {
            listBox_main.Items.Clear();
            mainTextBox.Text = "";
        }

        public StringMapInt calcAllClass(out intMapString gidMapGname)
        {
            StringMapInt result = new StringMapInt();
            intMapString gidMapGnameBuffer = new intMapString();
            using (var context = new sail_heEntities())
            {
                var ugradeList = context.ugrade.ToList();
                ugradeList.ForEach(ele => {
                    string gname = ele.gname;
                    gidMapGnameBuffer.Add(ele.gid, ele.gname);
                    result.Add(gname, ele.gid);
                });
            }
            gidMapGname = gidMapGnameBuffer;
            return result;
        }

        public List<StudentInfo> calcAll(intMapString gidMapGname)
        {
            List<StudentInfo> studentS = new List<StudentInfo>();

            StringMapInt result = new StringMapInt();
            using (var context = new sail_heEntities())
            {
                var resultList = context.ustudent.ToList();
                resultList.ForEach(ele => {
                    StudentInfo temp = new StudentInfo(
                    ele.sname
                    , (ele.ssexy) == "男" ? EnumGender.MALE : EnumGender.WOMAN
                    , DateTime.Parse(ele.sbdate)
                    , gidMapGname[int.Parse(ele.gid)]
                    , ele.stele
                    );
                    temp.setSduId(ele.sid);
                    temp.ClassId = int.Parse(ele.gid);
                    studentS.Add(temp);
                });
            }
            return studentS;
        }

        StudentInfo readAStudent(StringMapInt gnameMapGid = null)
        {
            bool isVerifyed = true;
            EnumGender enumGender;
            DateTime birthDay;
            string className, phone = "15258989595", stuNameBuffer, birthDayBuffer;

            stuNameBuffer = textBox_name.Text;
            className = textBox_grade.Text;
            birthDayBuffer = textBox_birthDay.Text;
            //textBox_stuNum.Text;
            enumGender = textBox_sex.Text == "男" ? EnumGender.WOMAN : EnumGender.MALE;
            if (gnameMapGid != null)
            {
                if (gnameMapGid.ContainsKey(className))
                {
                    //break;
                }
                else
                {
                    string allInMap = "";
                    int i = -1;
                    foreach (KeyValuePair<string, int> kv in gnameMapGid)
                    {
                        allInMap += ++i == 0 ? "" : "; ";
                        allInMap += kv.Key;// + ": " + kv.Value.ToString("0.") + "号";
                    }
                    TipsWriteLine("班级不存在!请重输; 可用班级:");

                    TipsWriteLine(allInMap);
                    isVerifyed = false;
                }
            }
            
            /*switch (ReadLine())
            {
                case "男": enumGender = EnumGender.MALE; break;
                case "女": enumGender = EnumGender.WOMAN; break;
                default: enumGender = EnumGender.UNKNOWN; break;
            }*/
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
            StudentInfo result = null;
            if (isVerifyed)
            {
                TipsWriteLine("添加输入成功!");
                result = new StudentInfo(stuNameBuffer, enumGender, birthDay, className, phone);
                result.ClassId = gnameMapGid[className];
            }
            else
            {
                // do nothing
            }
            TipsWriteLine("================");
            return result;
        }

        // https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/sql/linq/how-to-delete-rows-from-the-database
        // https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/
        void ShellFor_11_21(List<StudentInfo> studentS, StringMapInt gnameMapGid = null)
        {

        //查询
        start:
            string input = "- ";
            do
            {
                Clear();
                TipsWriteLine("姓名\t学号\t性别\t\t\t生日\t\t班级名\t联系电话");
                if (input.Length < 2)
                {
                    input += " ";
                }
                switch (input[0])
                {
                    // 按姓名查询
                    case '0':
                        studentS.FindAll(ele => ele.getName().Contains(input.Substring(2)))
                      .ForEach(ele => WriteLine(ele.tabString())); break;
                    // 按班级查询
                    case '1':
                        studentS.FindAll(ele => ele.ClassName.Contains(input.Substring(2)))
                      .ForEach(ele => WriteLine(ele.tabString())); break;
                    // 新增
                    case '2':
                        {
                            var temp = readAStudent(gnameMapGid);
                            temp.resetSduId();
                            studentS.Add(temp);
                            goto start;
                        }
                    // 更改
                    case '3':
                        {

                            string sidBuffer = "";
                            int index = -1;
                            while (true)
                            {
                                WriteLine("输入需要更改者的学号 直接回车返回:");
                                sidBuffer = ReadLine();
                                index = -1;
                                StudentInfo studentInfo = studentS.Find(ele => {
                                    ++index;
                                    return ele.getStuId().Equals(sidBuffer);
                                });
                                if (studentInfo != null)
                                {
                                    break;
                                }
                                else if (sidBuffer == "")
                                {
                                    goto start;
                                }
                                else
                                {
                                    WriteLine("学号不存在!请重输 或回车返回");
                                }
                            }
                            WriteLine("原信息: ");
                            WriteLine(studentS[index].tabString());
                            var temp = readAStudent(gnameMapGid);
                            temp.setSduId(int.Parse(studentS[index].getStuId()));
                            studentS[index] = temp;
                            goto start;
                        }
                    // 按学号查询
                    case '4':
                        studentS.FindAll(ele => ele.getStuId().Contains(input.Substring(2)))
                      .ForEach(ele => WriteLine(ele.tabString())); break;
                    // 显示所有
                    default: studentS.ForEach(ele => WriteLine(ele.tabString())); break;
                }
                /*TipsWriteLine(
                    "Shell提示: \n\r" +
                    " 0 name: 按姓名查询;" +
                    " 1 className: 按班级查询;" +
                    " 2: 新增;" +
                    " 3: 更改;" +
                    " 4 学号: 按学号查询;" +
                    " else: 显示所有;" +
                    " 回车: 保存并退出"
                    );*/

            } while ((input = ReadLine()) != string.Empty);
        }
        
        void saveAll(List<StudentInfo> studentS)
        {
            studentS.ForEach(ele => {
                if (ele.getStuId() == null)
                {
                    using (var context = new sail_heEntities())
                    {
                        ustudent temp = new ustudent();
                        temp.gid = ele.ClassId.ToString();
                        //temp.sid = int.Parse(ele.getStuId());
                        temp.sname = ele.getName().ToString();
                        temp.sbdate = ele.BirthDay.ToString();
                        temp.ssexy = (ele.Gender == EnumGender.MALE.ToString() ? "男" : "女");
                        temp.stele = ele.Phone.ToString();
                        context.ustudent.Add(temp);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new sail_heEntities())
                    {
                        int sid = int.Parse(ele.getStuId());
                        var stuBuffer = context.ustudent.Where(e => e.sid == sid).First();
                        ustudent temp = stuBuffer;
                        temp.gid = ele.ClassId.ToString();
                        temp.sid = int.Parse(ele.getStuId());
                        temp.sname = ele.getName().ToString();
                        temp.sbdate = ele.BirthDay.ToString();
                        temp.ssexy = (ele.Gender == EnumGender.MALE.ToString() ? "男" : "女");
                        temp.stele = ele.Phone.ToString();
                        //stuBuffer = temp;
                        context.SaveChanges();
                    }
                }
            });
        }
        
        private void button_add_Click(object sender, EventArgs e)
        {
            var temp = readAStudent(gnameMapGid);
            if(temp != null)
            {
                temp.resetSduId();
                studentS.Add(temp);
                WriteLine(temp.tabString());
            }
        }
        
        private void button_nameSearch_Click(object sender, EventArgs e)
        {
            Clear();
            studentS.FindAll(ele => ele.getName().Contains(textBox_nameSearch.Text))
                      .ForEach(ele => WriteLine(ele.tabString()));
        }

        private void listBox_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string
            object temp = listBox_main.SelectedItems[0];
            mainTextBox.AppendText(temp.ToString() + "\r\n");
            int index = (sender as ListBox).SelectedIndex;
            //检索此属性的值的运算复杂度为 O(1)。
            //@see https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.generic.list-1.count?view=netframework-4.7.2
            if (index >= studentS.Count() || index < 0)
            {
                // do nothing
            }
            else
            {
                StudentInfo seItem = studentS[(sender as ListBox).SelectedIndex] as StudentInfo;
                textBox_name.Text = seItem.getName();
                textBox_grade.Text = seItem.ClassName;
                textBox_birthDay.Text = seItem.BirthDay;
                textBox_stuNum.Text = seItem.getStuId();
                textBox_sex.Text = (seItem.Gender == EnumGender.MALE.ToString() ? "男" : "女");
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            saveAll(studentS);
            TipsWriteLine("保存成功!");
        }

        // @see search "EF模型增删改查"
        private void button_delete_Click(object sender, EventArgs e)
        {
            //lqRecCustTransDataContext
            sail_heEntities db = new sail_heEntities();

            ustudent delete = new ustudent();
            delete.sid = int.Parse(studentS[listBox_main.SelectedIndex].getStuId());
            db.Entry(delete).State = System.Data.Entity.EntityState.Deleted;
            studentS.RemoveAt(listBox_main.SelectedIndex);
            Clear();
            studentS.ForEach(ele => WriteLine(ele.tabString()));
            db.SaveChanges();
            /*
            var q = (from c in db.ustudent
                     where c.sid == tb_UserInf.CreateUser && c.AutoId == iAid
                     select c).First();
            db.ustudent.DeleteOnSubmit(q);
            db.ustudent.delete

            var q2 = db.ustudent.First(c => c.AutoId == iAid && c.CreateUser == tb_UserInf.CreateUser);
            db.ustudent.DeleteOnSubmit(q2);

            @see https://docs.microsoft.com/zh-cn/dotnet/framework/data/adonet/sql/linq/how-to-delete-rows-from-the-database
            // Query the database for the rows to be deleted.
            var deleteOrderDetails =
                from details in db.ustudent
                where details.sid == 11000
                select details;

            foreach (var detail in deleteOrderDetails)
            {
                db.ustudent.DeleteOnSubmit(detail);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }*/
        }
    }


    public enum EnumGender { UNKNOWN, MALE, WOMAN }

    // 性别, 出生日期, 班级名称, 联系电话
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class StudentInfo : BaseStudent
    {
        //按住ctrl+R不动，然后再按E
        private string gender = EnumGender.UNKNOWN.ToString();
        private string birthDay;
        private int classId;
        private string className;
        private string phone;
        public StudentInfo() : base("unknown") { }
        public StudentInfo(string name) : base(name) { }
        public StudentInfo(string name, EnumGender enumGender, DateTime birthDay, string className, string phone) : this(name)
        {
            this.gender = enumGender.ToString();
            this.birthDay = birthDay.ToString();

            this.className = className;
            this.phone = phone;
        }

        public string Gender { get => gender; set => gender = value; }
        public string BirthDay { get => birthDay; set => birthDay = value; }
        public string ClassName { get => className; set => className = value; }
        public string Phone { get => phone; set => phone = value; }
        public int ClassId { get => classId; set => classId = value; }

        override
        public string ToString()
        {
            return base.ToString() + "; 性别: " + gender + "; 生日: " + birthDay + "; 班级名: " + className + "; 联系电话: " + phone;
        }
        public string tabString()
        {
            return getName() + "\t"
                + (getStuId() == null ? "---------" : getStuId())
                + "\t" + (gender == EnumGender.MALE.ToString() ? "男" : "女") + "\t\t" + birthDay + "\t" + className + "\t" + phone;
        }
        
    }

}

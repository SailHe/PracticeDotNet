using SailHeCSharpClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using LearnDotNet;

namespace WinFormsApp.src
{
    using StringMapInt = Dictionary<string, int>;
    using intMapString = Dictionary<int, string>;

    public partial class Form11_21 : Form
    {
        
        public Form11_21()
        {
            InitializeComponent();

            studentS = new List<StudentInfo>();
            intMapString gidMapGname = null;
            var gredeIdMapName = calcAllClass(out gidMapGname);
            studentS = calcAll(gidMapGname);

            /// var gredeIdMapName = LearnDotNet.Program.calcAllClass(out gidMapGname);
            /// studentS = LearnDotNet.Program.calcAll(gidMapGname);

            ShellFor_11_21(studentS, gredeIdMapName);
        }

        /// <summary>
        /// Shell输出换行
        /// </summary>
        /// <param name="msg"></param>
        void WriteLine(string msg)
        {
            listBox_main.Items.Add(msg);
            //mainTextBox.AppendText(msg + "\r\n");
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
            EnumGender enumGender;
            DateTime birthDay;
            string className, phone, stuNameBuffer, birthDayBuffer;
            WriteLine("输入学生姓名:");
            stuNameBuffer = ReadLine();
            if (gnameMapGid != null)
            {
                string allInMap = "";
                int i = -1;
                foreach (KeyValuePair<string, int> kv in gnameMapGid)
                {
                    allInMap += ++i == 0 ? "" : "; ";
                    allInMap += kv.Key;// + ": " + kv.Value.ToString("0.") + "号";
                }
                while (true)
                {
                    WriteLine("输入班级名:");
                    WriteLine(allInMap);
                    className = ReadLine();
                    if (gnameMapGid.ContainsKey(className))
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("班级不存在!请重输");
                    }
                }
            }
            else
            {
                WriteLine("输入班级名:");
                className = ReadLine();
            }
            WriteLine("输入性别: '男' '女' 其余视为'未知'");
            switch (ReadLine())
            {
                case "男": enumGender = EnumGender.MALE; break;
                case "女": enumGender = EnumGender.WOMAN; break;
                default: enumGender = EnumGender.UNKNOWN; break;
            }
            while (true)
            {
                WriteLine("输入生日:");
                birthDayBuffer = ReadLine();
                if (Verify.IsDateTime(birthDayBuffer))
                {
                    break;
                }
                else
                {
                    WriteLine("格式错误!请重输");
                }
            }
            birthDay = DateTime.Parse(birthDayBuffer);

            while (true)
            {
                WriteLine("输入联系方式(电话,手机号):");
                phone = ReadLine();
                if (Verify.IsPhoneNumber(phone))
                {
                    break;
                }
                else
                {
                    WriteLine("格式错误!请重输");
                }
            }
            var result = new StudentInfo(stuNameBuffer, enumGender, birthDay, className, phone);
            result.ClassId = gnameMapGid[className];
            return result;
        }

        void ShellFor_11_21(List<StudentInfo> studentS, StringMapInt gidMapGname = null)
        {

        //查询
        start:
            string input = "- ";
            do
            {

                Clear();
                WriteLine("姓名\t学号\t性别\t\t\t生日\t\t班级名\t联系电话");
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
                            var temp = readAStudent(gidMapGname);
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
                                    return ele.getSduId().Equals(sidBuffer);
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
                            var temp = readAStudent(gidMapGname);
                            temp.setSduId(int.Parse(studentS[index].getSduId()));
                            studentS[index] = temp;
                            goto start;
                        }
                    // 按学号查询
                    case '4':
                        studentS.FindAll(ele => ele.getSduId().Contains(input.Substring(2)))
                      .ForEach(ele => WriteLine(ele.tabString())); break;
                    // 显示所有
                    default: studentS.ForEach(ele => WriteLine(ele.tabString())); break;
                }
                WriteLine(
                    "Shell提示: \n\r" +
                    " 0 name: 按姓名查询;" +
                    " 1 className: 按班级查询;" +
                    " 2: 新增;" +
                    " 3: 更改;" +
                    " 4 学号: 按学号查询;" +
                    " else: 显示所有;" +
                    " 回车: 保存并退出"
                    );

            } while ((input = ReadLine()) != string.Empty);
        }
        
        void saveAll(List<StudentInfo> studentS)
        {
            studentS.ForEach(ele => {
                if (ele.getSduId() == null)
                {
                    using (var context = new sail_heEntities())
                    {
                        ustudent temp = new ustudent();
                        temp.gid = ele.ClassId.ToString();
                        //temp.sid = int.Parse(ele.getSduId());
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
                        int sid = int.Parse(ele.getSduId());
                        var stuBuffer = context.ustudent.Where(e => e.sid == sid).First();
                        ustudent temp = stuBuffer;
                        temp.gid = ele.ClassId.ToString();
                        temp.sid = int.Parse(ele.getSduId());
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
            saveAll(studentS);
        }

        private List<StudentInfo> studentS;
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
                + (getSduId() == null ? "---------" : getSduId())
                + "\t" + gender + "\t\t" + birthDay + "\t" + className + "\t" + phone;
        }
        
    }

}

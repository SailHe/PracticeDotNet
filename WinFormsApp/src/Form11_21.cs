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

    public partial class Form11_21 : Form
    {
        public Form11_21()
        {
            InitializeComponent();
            mainTextBox.AppendText("123456");
        }


        public static StringMapInt calcAllClass(out intMapString gidMapGname)
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

        public static List<StudentInfo> calcAll(intMapString gidMapGname)
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


        static void solve1_11_21()
        {
            List<StudentInfo> studentS = new List<StudentInfo>();
            intMapString gidMapGname = null;
            var gredeIdMapName = calcAllClass(out gidMapGname);
            studentS = calcAll(gidMapGname);
            /// var gredeIdMapName = LearnDotNet.Program.calcAllClass(out gidMapGname);
            /// studentS = LearnDotNet.Program.calcAll(gidMapGname);
            
            // ShellFor_11_15(studentS, gredeIdMapName);
            // saveAll(studentS);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            solve1_11_21();
        }
    }
}

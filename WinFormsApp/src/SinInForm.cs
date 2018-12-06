using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.src
{

    using StringMapInt = Dictionary<string, int>;
    using intMapString = Dictionary<int, string>;

    public partial class SinInForm : Form
    {
        private List<StudentInfo> studentS;
        private StringMapInt gnameMapGid;
        private intMapString gidMapGname;
        private sail_heEntities dbContext = new sail_heEntities();

        public bool CloseFlag { get; internal set; }

        public SinInForm()
        {
            InitializeComponent();
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

        private void SinInForm_Load(object sender, EventArgs e)
        {
            gnameMapGid = calcAllClass(out this.gidMapGname);
            studentS = calcAll(this.gidMapGname);
        }

        private void signButton_Click(object sender, EventArgs e)
        {
            // new Form11_21().Show();
            // var user = new sys_user();
            // user.user_name = "tester";
            // var temp = dbContext.sys_user.Find(user);
            var stu = studentS.Find(ele => ele.getStuId() == stuSignIdTextBox.Text);
            if (stu != null)
            {
                if (stu.getStuId() == signPswTextBox.Text)
                {
                    this.CloseFlag = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码错误！密码就是学号");
                }
            }
            else
            {
                MessageBox.Show("学号错误！");
            }
            
        }
    }
}

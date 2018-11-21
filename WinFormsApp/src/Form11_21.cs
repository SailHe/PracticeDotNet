using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LearnDotNet;

namespace WinFormsApp.src
{
    using StringMapInt = Dictionary<string, int>;
    using intMapString = Dictionary<int, string>;

    public partial class Form11_21 : Form
    {
        public Form11_21()
        {
            InitializeComponent();
            mainTextBox.AppendText("123456");
        }

        static void solve1_11_21()
        {
            List<StudentInfo> studentS = new List<StudentInfo>();
            intMapString gidMapGname = null;
            var gredeIdMapName = LearnDotNet.Program.calcAllClass(out gidMapGname);
            studentS = LearnDotNet.Program.calcAll(gidMapGname);
            // ShellFor_11_15(studentS, gredeIdMapName);
            // LearnDotNet.Program.saveAll(studentS);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            solve1_11_21();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SailHeCSharpClassLib;

namespace WinFormsApp
{
    using WinFormsApp.src;

    public partial class PlaygroundForm : Form
    {
        private bool lhsBigNumHasSet = false, rhsBigNumHasSet = false;

        public PlaygroundForm()
        {
            InitializeComponent();
        }

        private void bigNumPlushbutton_Click(object sender, EventArgs e)
        {
            if(lhsBigNumHasSet && rhsBigNumHasSet)
            {
                int radix = int.Parse(bigNumRadix_textBox.Text);
                if(radix < 2 || radix > 36)
                {
                    Win32API.MessageBox(IntPtr.Zero, "进制输入错误!", "警告", 0);
                }
                else
                {
                    bigNumSum_textBox.Text = UtilityApi.bigPlush(
                        lhsBigNum_textBox.Text
                        , rhsBigNum_textBox.Text
                        , radix
                        );
                }
            }
            else
            {
                bigNumSum_textBox.Text = "请在左侧输入框 输入数字!";
            }
        }

        private void APItestButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(SolveHomeworkProblem.SailHeApiTest());
        }

        private void lhsNum_TextChanged(object sender, EventArgs e)
        {
            lhsBigNumHasSet = true;
        }

        private void button_to_11_21_Click(object sender, EventArgs e)
        {
            new Form11_21().Show();
        }

        private void rhsNum_TextChanged(object sender, EventArgs e)
        {
            rhsBigNumHasSet = true;
        }
    }
}

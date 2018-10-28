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

namespace WinFormsApp
{
    using WinFormsApp.src;

    public partial class PlaygroundForm : Form
    {
        public PlaygroundForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] args = { "2" };
            Debug.WriteLine(SolveHomeworkProblem.SellApiForHomeWork());
        }
    }
}

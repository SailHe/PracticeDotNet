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
    public partial class SinInForm : Form
    {
        public bool CloseFlag { get; internal set; }

        public SinInForm()
        {
            InitializeComponent();
        }

        private void SinInForm_Load(object sender, EventArgs e)
        {

        }

        private void signButton_Click(object sender, EventArgs e)
        {
            // new Form11_21().Show();
            this.CloseFlag = true;
            this.Close();
        }
    }
}

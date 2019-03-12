using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCT.User
{
    public partial class Register : Form
    {
        bool root;
        public Register(bool root)
        {
            InitializeComponent();
            this.root = root;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
            if (root)
            {
                this.textBox1.Enabled = false;
                this.textBox1.Text = "Root";
            }
        }
    }
}

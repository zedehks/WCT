using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCT
{
    public partial class Login : Form
    {
        sqlite_connector con;
        public Login()
        {
            InitializeComponent();
            con = new sqlite_connector();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User.Register r = new User.Register(false);
            r.ShowDialog();
        }
    }
}

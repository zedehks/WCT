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
    public partial class Login : Form
    {
        sqlite_connector con;
        public string username;
        public Login()
        {
            InitializeComponent();
            con = new sqlite_connector();
            this.username = null;
        }
        public Login(string username)
        {
            InitializeComponent();
            con = new sqlite_connector();
            this.username = username;
        }
        public void get_user(string username)
        {
            this.username = username;
            this.textBox1.Text = this.username;
            this.ActiveControl = this.textBox2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User.Register r = new User.Register(this,false);
            r.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if(username != null)
            {
                this.textBox1.Text = username;
            }
            this.ActiveControl = this.textBox1;
        }
    }
}

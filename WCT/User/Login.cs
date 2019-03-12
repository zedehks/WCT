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
        public int login_id;
        public Login()
        {
            InitializeComponent();
            con = new sqlite_connector();
            this.username = null;
            login_id = -1;
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

        bool check_valid_values()
        {
            bool oll_korrect = true;
            if (textBox1.Text == "") oll_korrect = false;
            if (textBox2.Text == "") oll_korrect = false;

            return oll_korrect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!check_valid_values())
            {
                MessageBox.Show("One or more fields are empty!");
            }
            else
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string sql = @"select id_user from users where username='{0}' and password='{1}'";
                string id = "";
                con.open();
                try
                {
                    id = con.select(string.Format(sql, username, password)).
                    Tables[0].Rows[0].ItemArray[0].ToString();
                    con.close();
                }catch(System.IndexOutOfRangeException ex)
                {
                    try { con.close(); } catch { }
                    id = "";
                }

                if (id == "")
                    MessageBox.Show("WROOOOOOOOOOOOOOOOOOOOOOONG");
                else
                {
                    MessageBox.Show("[Hacker Voice]\n IM IN");
                    this.Close();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                button1.PerformClick();
            }
        }
    }
}

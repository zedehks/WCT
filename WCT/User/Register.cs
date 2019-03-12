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
        bool root=false;
        sqlite_connector con;
        Login parent_login = null;
        public Register()
        {
            InitializeComponent();
        }

        public Register(Login l, bool root)
        {
            InitializeComponent();
            parent_login = l;
            this.root = root;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!check_valid_values())
            {
                MessageBox.Show("One or more fields are empty!");
            }
            else if(!check_password())
            {
                MessageBox.Show("Passwords do not match!");
            }
            else
                register_user();

            Application.DoEvents();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            con = new sqlite_connector();
            this.DialogResult = DialogResult.None;
            this.BringToFront();
            this.Activate();
            this.ActiveControl = this.textBox1;
            if (root)
            {
                this.textBox1.Enabled = false;
                this.textBox1.Text = "root";
                this.ActiveControl = this.textBox2;
            }
        }

        void register_user()
        {
            bool can_exit = false;
            string sql_command = @"insert into users(username,password) values ('{0}','{1}');";
            if(root)
            {
                sql_command = @"insert into users(id_user,username,password) values (0,'{0}','{1}');";
            }

            try
            {
                con.open();
                con.command(string.Format(sql_command,this.textBox1.Text,this.textBox2.Text));
                con.close();
                MessageBox.Show("User has been registered succesfully!");
                parent_login.get_user(this.textBox1.Text);
                can_exit = true;
            }
            catch (Exception ex)
            {
                try { con.close(); } catch { }
                if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("user"))
                    MessageBox.Show("Username already registered!");
                else
                    MessageBox.Show(ex.Message);
                can_exit = false;
                Application.DoEvents();
            }
            finally
            {
                try { con.close(); } catch { }
                if(can_exit)
                {
                    this.Close();
                }
            }
        }

        bool check_password()
        {
            return textBox2.Text == textBox3.Text ? true : false;
        }
        bool check_valid_values()
        {
            bool oll_korrect = true;
            if (textBox1.Text == "") oll_korrect = false;
            if (textBox2.Text == "") oll_korrect = false;
            if (textBox3.Text == "") oll_korrect = false;


            return oll_korrect;
        }
    }
}

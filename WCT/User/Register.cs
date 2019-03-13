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
        public bool can_exit = false;
        MainMenu.Modify_Users Modu = null;
        bool modifying;
        string user_id;
        public Register()
        {
            InitializeComponent();
        }
        public Register(MainMenu.Modify_Users mu,bool ismodding)
        {
            InitializeComponent();
            Modu = mu;
            modifying = ismodding;
        }

        public Register(Login l, bool root)
        {
            InitializeComponent();
            parent_login = l;
            this.root = root;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!check_valid_values())
            {
                MessageBox.Show("One or more fields are empty!");
            }
            else if (!check_password())
            {
                MessageBox.Show("Passwords do not match!");
            }
            else if (modifying)
                update_user();
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
            else if (modifying)
            {
                this.Text = "Modifying User";
                this.button1.Text = "Update";
                this.label2.Text = "New Password";
                this.label3.Text = "Confirm New Password";
            }
        }
        public void get_user_data(string user,string id)
        {
            this.textBox1.Text = user;
            this.user_id = id;
        }
        void update_user()
        {
            string sql_command = @"update users 
            set username = '{0}', password='{1}' where id_user = {2};";
            try
            {
                con.open();
                con.command(string.Format(sql_command,this.textBox1.Text,this.textBox2.Text,this.user_id));
                con.close();
                MessageBox.Show("User has been updated succesfully!");
                if (Modu != null) Modu.update_dfv(true);
                this.Close();
            }catch (Exception ex)
            {
                try { con.close(); } catch { }
                if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("user"))
                    MessageBox.Show("Username already registered!");
                else
                    MessageBox.Show(ex.Message);
                Application.DoEvents();
            }
        }
        void register_user()
        {
            can_exit = false;
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
                if (parent_login != null) parent_login.get_user(this.textBox1.Text);
                if (Modu != null) Modu.update_dfv(true);
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

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                button1.PerformClick();
            }
        }
    }
}

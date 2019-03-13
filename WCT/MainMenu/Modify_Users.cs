using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCT.MainMenu
{
    public partial class Modify_Users : Form
    {
        string sql_main = @"select id_user,username,password from users;";
        string sql_search = @"select id_user,username,password from users where username like '%{0}%'";
        sqlite_connector con;
        bool show_pass;
        public Modify_Users()
        {
            InitializeComponent();
            con = new sqlite_connector();
            show_pass = false;
        }

        private void Modify_Users_Load(object sender, EventArgs e)
        {
            con.open();
            this.dataGridView1.DataSource = con.select(sql_main).Tables[0];
            con.close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.open();
            dataGridView1.DataSource = con.select(string.Format(sql_search,textBox1.Text)).Tables[0];
            con.close();
        }
    }
}

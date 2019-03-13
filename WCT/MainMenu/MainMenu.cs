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
    public partial class MainMenu : Form
    {
        readonly int ROOT_ID = 0;
        int id_user;
        sqlite_connector con;
        ToolStripItem mod_users;
        public MainMenu(int user)
        {
            InitializeComponent();
            id_user = user;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            string sql = @"
            select username from users where id_user={0}
            ";
            con = new sqlite_connector();
            con.open();

            string title = con.select(string.Format(sql,id_user.ToString())).Tables[0].Rows[0].ItemArray[0].ToString();
            con.close();
            this.Text += ": Logged in as "+title;
            if (id_user == ROOT_ID)
            {
                mod_users = new ToolStripMenuItem("Users",null, userToolStripMenuItem);
                menuStrip1.Items.Add(mod_users);
            }
        }

        private void userToolStripMenuItem(object sender, EventArgs e)
        {
            Modify_Users mu = new Modify_Users();
            mu.MdiParent = this;
            mu.Show();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
         //   program
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer_Menu tm = new Timer_Menu();
            tm.MdiParent = this;
            tm.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void listSessionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class MainMenu : Form
    {
        int id_user;
        sqlite_connector con;
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
            Application.Restart();
        }
    }
}

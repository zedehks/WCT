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
        string sql_main = @"select id_user as id,username from users order by id asc;";
        string sql_main_full = @"select id_user as id,username,password from users order by id asc;";
        string sql_search = @"select id_user as id,username from users where username like '%{0}%'";
        string sql_search_full = @"select id_user as id,username,password from users where username like '%{0}%'";
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
        public void update_dfv(bool reset)
        {
            string search;
            con.open();
            if (reset)
            {
                search = (show_pass) ? sql_main_full : sql_main;
                dataGridView1.DataSource = con.select(search).Tables[0];
            }
            else
            {
                search = (show_pass) ? sql_search_full : sql_search;
                dataGridView1.DataSource = con.select(string.Format(search,textBox1.Text)).Tables[0];
            }
            con.close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            update_dfv(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (show_pass) show_pass = false;
            else if (!show_pass) show_pass = true;
            update_dfv(true);
        }

        private void registerNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User.Register r = new User.Register(this,false);
            r.MdiParent = this.MdiParent;
            r.Show();
            if (r.can_exit)
                update_dfv(true);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql_del = @"delete from users where id_user = {0};
            delete from session where id_user={0};
            delete from solve where solve.id_session in (select id_session from session where id_user = {0} and solve.id_session=session.id_Session);;
            ";
            string userid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if(userid == "0")
            {
                MessageBox.Show("Cannot delete Root!");
                return;
            }
            try
            {
                con.open();
                con.command(string.Format(sql_del, userid));
                con.close();
                MessageBox.Show("User has been deleted.");
                update_dfv(true);
            }catch (Exception ex) { /*MessageBox.Show(ex.Message)*/; try { con.close(); } catch { } }
            finally {
                update_dfv(true);
            }

        }

        private void dataGridView1_MouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            dataGridView1.Rows[e.RowIndex].Selected = true;
            dataGridView1.Focus();
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string username = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            User.Register r = new User.Register(this,true);
            r.MdiParent = this.MdiParent;
            r.Show();
            r.get_user_data(username,id);
            if (r.can_exit)
                update_dfv(true);
        }
    }
}

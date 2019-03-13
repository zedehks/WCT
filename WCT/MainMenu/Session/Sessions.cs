using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCT.MainMenu.Session
{
    public partial class Sessions : Form
    {
        string sql = @"select id_session as Session,time_date as Date, comment as Comment from Session where id_user={0} order by Date asc";
        sqlite_connector con = new sqlite_connector();
        public string user_id;
        public string session_id;
        public Sessions(string user)
        {
            InitializeComponent();
            this.user_id = user;
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
            update_sessions();
            dataGridView1.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoResizeColumn(2,DataGridViewAutoSizeColumnMode.Fill);
            dataGridView1.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
        }
        public void update_sessions()
        {
            string tmp = string.Format(sql, user_id);
            dataGridView1.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            con.open();
            this.dataGridView1.DataSource = con.select(tmp).Tables[0];
            con.close();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
        }

        private void addNewSessionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            New_Session nw = new New_Session(false,this);
            nw.MdiParent = this.MdiParent;
            nw.Show();
        }

        private void modifySessionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string datetime = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                MessageBox.Show(datetime);
                string comment = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                session_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                New_Session nw = new New_Session(true, this);
                nw.MdiParent = this.MdiParent;
                nw.get_values(datetime, comment, user_id,session_id);
                nw.Show();

            }
            catch (System.ArgumentOutOfRangeException ex) { }
        }

        public void open_timer(string id_session)
        {
            Timer_Menu tm = new Timer_Menu(id_session);
            tm.MdiParent = this.MdiParent;
            tm.Show();
        }

        private void deleteSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"delete from session where id_session = {0}";
                string datetime = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string comment = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string session = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                con.open();
                con.command(string.Format(sql,session));
                con.close();
                MessageBox.Show("Session Deleted.");
                update_sessions();

            }
            catch (System.ArgumentOutOfRangeException ex) { }
        }

        private void selectSessionAndOpenTimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string session = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                Timer_Menu tm = new Timer_Menu(session);
                tm.MdiParent = this.MdiParent;
                tm.Show();
            }
            catch (System.ArgumentOutOfRangeException ex) { }
        }
        private void dataGridView1_MouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.Focus();
            }
            catch { }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectSessionAndOpenTimerToolStripMenuItem_Click(sender, e);
        }

        private void selectSessionAndOpenTimerToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            selectSessionAndOpenTimerToolStripMenuItem_Click(sender, e);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string session = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Timer_Menu tm = new Timer_Menu(session);
                    tm.MdiParent = this.MdiParent;
                    tm.Show();
                }
                catch (System.ArgumentOutOfRangeException ex) { }
            }
        }
    }
}

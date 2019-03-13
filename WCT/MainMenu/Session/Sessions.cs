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
        string sql = @"select time_date as Date, comment as Comment from Session where id_user={0} order by Date asc";
        sqlite_connector con = new sqlite_connector();
        public string user_id;
        public Sessions(string user)
        {
            InitializeComponent();
            this.user_id = user;
        }

        private void Sessions_Load(object sender, EventArgs e)
        {
            update_sessions();
            dataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoResizeColumn(1,DataGridViewAutoSizeColumnMode.Fill);
        }
        public void update_sessions()
        {
            string tmp = string.Format(sql, user_id);
            MessageBox.Show(tmp);
            con.open();
            this.dataGridView1.DataSource = con.select(tmp).Tables[0];
            con.close();
        }

        private void addNewSessionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            New_Session nw = new New_Session(false,this);
            nw.MdiParent = this.MdiParent;
            nw.Show();
        }

        private void modifySessionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string datetime = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string comment = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            New_Session nw = new New_Session(true,this);
            nw.MdiParent = this.MdiParent;
            nw.get_values(datetime,comment,user_id);
            nw.Show();
        }

        public void open_timer(string id_user)
        {
            Timer_Menu tm = new Timer_Menu(id_user);
            tm.MdiParent = this.MdiParent;
            tm.Show();
        }

        private void deleteSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectSessionAndOpenTimerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_MouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            dataGridView1.Rows[e.RowIndex].Selected = true;
            dataGridView1.Focus();
        }
    }
}

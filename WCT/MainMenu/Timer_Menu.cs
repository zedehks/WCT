using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WCT
{
    public partial class Timer_Menu : Form
    {
        Scramble s;
        bool is_solving;
        DateTime StartTime;
        double total_ms = 0;
        string id_session;
        bool show_solves;
        sqlite_connector con = new sqlite_connector();
        string current_scramble;
        bool first_scramble = true;
        public Timer_Menu(string id_session)
        {
            InitializeComponent();
            s = new Scramble();
            label1.Text = "Press Space to generate a scramble."+
                "\nPress Delete to delete last solve.";
            is_solving = false;
            this.id_session = id_session;
            this.show_solves = false;
            update_solves();
        }
        void convert_grid_times()
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                double tmp = Convert.ToDouble(r.Cells[1].Value);
                string tmp_str = TimeSpan.FromMilliseconds(tmp).ToString(@"mm\:ss\.ff");
                r.Cells[1].Value = tmp_str;
            }
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!is_solving)
                {
                    timer1.Stop();
                    if(first_scramble)
                    {
                        first_scramble = false;

                    }
                    else
                    {
                        save_solve();
                    }
                    current_scramble = s.scramble_gen().ToString();
                    label1.Text = "Scramble:\n" + current_scramble;
                    is_solving = true;
                    
                }
                else
                {
                    total_ms = 0;
                    timer1.Start();
                    StartTime = DateTime.Now;
                    is_solving = false;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                deleteToolStripMenuItem_Click(sender, e);
            }
        }
        void save_solve()
        {
            string sql = @"insert into solve(id_session,time,scramble) values({0},{1},""{2}"")";
            con.open();
            string tmp = string.Format(sql,id_session,total_ms,current_scramble);
            con.command(tmp);
            con.close();
            update_solves();
        }

        void update_solves()
        {
            string sql = @"select id_solve as 'Number',time as Time,scramble as Scramble
            from solve where id_session={0}";
            con.open();
            this.dataGridView1.DataSource = con.select(string.Format(sql,id_session)).Tables[0];
            con.close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
           total_ms = elapsed.TotalMilliseconds;
            string time_text = "";
            time_text = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds).ToString(@"mm\:ss\.ff");
            this.label2.Text = time_text;
        }

        private void Timer_Menu_Load(object sender, EventArgs e)
        {
            this.show_solves = true;
            //this.splitContainer2.Panel2Collapsed = false;
            //splitContainer2.Panel2.Hide();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = TimeSpan.FromMilliseconds((double)e.Value).ToString(@"mm\:ss\.ff");
                e.FormattingApplied = true;
                dataGridView1.Focus();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"delete from solve where id_solve =  (Select max(id_solve) from solve where id_session={0});";
                con.open();
                con.command(string.Format(sql,id_session));
                con.close();
                MessageBox.Show("Last Solve Deleted");
                update_solves();

            }
            catch (System.ArgumentOutOfRangeException ex) { }
        }

        private void dataGridView1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("Delete"));

                int curr_row = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if(curr_row >= 0)
                {
                    deleteToolStripMenuItem_Click(sender, new EventArgs());
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Focus();
        }
    }
}

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
    public partial class Form1 : Form
    {
        Scramble s;
        bool is_solving;
        DateTime StartTime;
        public Form1()
        {
            InitializeComponent();
            s = new Scramble();
            label1.Text = "Press Space to generate a scramble";
            is_solving = false;
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if(!is_solving)
                {
                    timer1.Stop();
                    label1.Text = "Scramble:\n" + s.scramble_gen().ToString();
                    is_solving = true;
                }
                else
                {
                    timer1.Start();
                    StartTime = DateTime.Now;
                    is_solving = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
            string time_text = "";
            int tenths = elapsed.Milliseconds / 10;
            time_text +=
                elapsed.Minutes.ToString("00") + ":" +
                elapsed.Seconds.ToString("00") + "." +
                tenths.ToString("00");

            this.label2.Text = time_text;
        }
    }
}

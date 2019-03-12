﻿using System;
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
        public Timer_Menu()
        {
            InitializeComponent();
            s = new Scramble();
            label1.Text = "Press Space to generate a scramble";
            is_solving = false;
          //  this.DoubleBuffered = true;
           // SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if(!is_solving)
                {
                    timer1.Stop();
                    //MessageBox.Show("time elapsed:\n"+total_ms.ToString() + " ms");
                    //MessageBox.Show(TimeSpan.FromMilliseconds(total_ms).ToString("c"));
                    label1.Text = "Scramble:\n" + s.scramble_gen().ToString();
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - StartTime;
           total_ms = elapsed.TotalMilliseconds;
            string time_text = "";
          //  int tenths = elapsed.Milliseconds / 10;
            /*time_text +=
                elapsed.Minutes.ToString("00") + ":" +
                elapsed.Seconds.ToString("00") + "." +
                tenths.ToString("00");*/
            time_text = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds).ToString("c");
            this.label2.Text = time_text;
        }

    }
}
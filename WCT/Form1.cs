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
    public partial class Form1 : Form
    {
        Scramble s;
        public Form1()
        {
            InitializeComponent();
            s = new Scramble();
            label1.Text = "Press Space to generate a scramble";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                label1.Text = "Scramble:\n" + s.scramble_gen().ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}

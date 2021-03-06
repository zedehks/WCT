﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCT.MainMenu.Session
{
    public partial class New_Session : Form
    {
        bool isModding;
        string session_user;
        string session_id;
        Sessions ss;
        sqlite_connector con = new sqlite_connector();
        public New_Session(bool is_modding, Sessions s)
        {
            InitializeComponent();
            this.isModding = is_modding;
            this.ss = s;
            this.session_user = s.user_id;
        }
        public void get_values(string datetime,string comment,string id_user,string session)
        {
            this.textBox1.Text = comment;
            // this.dateTimePicker1.Value = DateTime.ParseExact(datetime,"dd/MM/yyyy h:mm:ss tt",CultureInfo.InvariantCulture);
            this.dateTimePicker1.Value = DateTime.Parse(datetime);
            this.session_user = id_user;
            session_id = session;
        }
        private void New_Session_Load(object sender, EventArgs e)
        {
            if(isModding)
            {
            }
            this.textBox1.Focus();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if(isModding)
            {
                sql = @"update session set time_date='{0}',comment='{1}' where id_session={2}";
                con.open();
                con.command(string.Format(sql, dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm"),this.textBox1.Text,this.session_id));
                con.close();
                ss.update_sessions();
                this.Close();
            }
            else
            {
                sql = @"insert into session(id_user,time_date,comment) values({0},'{2}','{1}')";
                con.open();
                con.command(string.Format(sql,this.session_user,this.textBox1.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm")));
                con.close();
                ss.update_sessions();
                this.Close();
            }
        }
    }
}

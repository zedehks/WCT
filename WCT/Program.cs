using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            sqlite_connector con = new sqlite_connector();
            con.open();
            int amount_users = Convert.ToInt32(con.select(@"select count(id_user) from users;").Tables[0].Rows[0].ItemArray[0].ToString());
            if (amount_users < 1)
            {
                if(first_time_run())
                   Application.Run(new User.Register(true));
            }
            else
            {
                MessageBox.Show(amount_users.ToString());
            }
        }
        static bool first_time_run()
        {
               string ms_text = "Welcome to WCT, the WCT Cube Timer.\n\n" +
                   "Since this is the first time you've run this software, you will now create your Root account, to manage " +
                   "all others. Please notice that you will -NOT- be able to reset this password if you lose it. " +
                   "Additionally, this application stores all passwords as plaintext, so please use your brain and -DO NOT- use your bank credentials (or any other, for that matter) here.";
               string ms_caption = "Yo, A Brief Heads-up";
               MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
               DialogResult result;

            result = MessageBox.Show(ms_text, ms_caption, buttons);

            if (result == System.Windows.Forms.DialogResult.OK)
                return true;
            return false;

        }
    }
}

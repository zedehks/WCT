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

            sqlite_connector con = new sqlite_connector();
            con.open();
            int amount_users = Convert.ToInt32(
                con.select(@"select count(id_user) from users;")
                .Tables[0].Rows[0].ItemArray[0].ToString());
            con.close();

            string registered_user;
            //if we have 0 users, then prompt the user to generate the root account

            User.Login u_login = new User.Login();
            if (amount_users < 1)
            {
                if (first_time_run())
                {
                    registered_user = u_login.username;
                    Application.Run(new User.Register(u_login, true));
                    if (u_login.username != null)
                    {
                        Application.Run(u_login);
                    }
                }
            }
            else
            {
                Application.Run(u_login);
                if (u_login.login_ok)
                {
                    Application.Run(new MainMenu(u_login.login_id));
                }
            }
        }

            static bool first_time_run()
            {
                string ms_text = "Welcome to WCT, the WCT Cube Timer.\n\n" +
                    "Since this is the first time you've run this software, you will now create your Root account, to manage " +
                    "all others. Please note that you will -NOT- be able to reset this password if you lose it. " +
                    "Additionally, this application stores all passwords as plaintext, so " +
                    "please use your brain and -DO NOT- use your bank credentials (or any others, for that matter) here.";
                string ms_caption = "Yo, A Brief Heads-up";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result;

                result = MessageBox.Show(ms_text, ms_caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                    return true;
                return false;

            }
            public static void open_login()
            {
                Application.Run(new User.Login());
            }
            public static void open_main_menu(int user_id)
            {
                Application.Run(new MainMenu(user_id));
            }
        }
    } 

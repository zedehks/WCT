using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Citas
{
    class sqlite_connector
    {
        SQLiteConnection my_connect;
        SQLiteCommand my_cmd;
        SQLiteTransaction my_txn;
        SQLiteDataAdapter da;

        public string chain = "Data Source=WCT.db ; Version=3;datetimeformat=CurrentCulture";
        public void open()
        {
            my_connect= new SQLiteConnection(chain);
            my_connect.Open();
            my_cmd = my_connect.CreateCommand();
            my_txn = my_connect.BeginTransaction(System.Data.IsolationLevel.Serializable);
        }

        public DataSet select(string sql)
        {
            DataSet ds = new DataSet();
            my_cmd.Connection = my_connect;
            my_cmd.Transaction = my_txn;
            da = new SQLiteDataAdapter(sql, my_connect);
            da.SelectCommand.Transaction = my_txn;
            da.Fill(ds);
            return ds;
        }

        public void command(string sql)
        {
            my_cmd.Transaction = my_txn;
            my_cmd.CommandText = sql;
            my_cmd.ExecuteNonQuery();
        }

        public void close()
        {
            my_txn.Commit();
            my_connect.Close();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Configuration;

namespace DatabaseLayer
{
    public class DatabaseAccess
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private string environmentpath;
        public DatabaseAccess()
        {
            environmentpath = ConfigurationManager.ConnectionStrings["path"].ToString();
            checkDB();
        }
        public void checkDB()
        {
            using (SQLiteConnection _connection = new SQLiteConnection())
            {
                if (File.Exists(environmentpath))
                {
                    sql_con.ConnectionString= $"Data Source={environmentpath};Version=3";
                }
                else
                {
                    SQLiteConnection.CreateFile(environmentpath);
                 }
            }
        }
        private void SetConnection()
        {
            sql_con.Open();
        }
        private void ExecuteQuery(string txtQuery)
        {

            SetConnection();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            CloseConnection();
           
        }
        private void CloseConnection()
        {
            sql_con.Close();
        }
        private void LoadData()
        {

        }
        private void AddData()
        {
           
        }
        private void DeleteData()
        {

        }
    }
}

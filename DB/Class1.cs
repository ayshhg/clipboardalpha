using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Configuration;
using System.Data;

namespace DB
{
    public class Class1
    {

            private SQLiteConnection sql_con;
            private SQLiteCommand sql_cmd;
            private SQLiteDataAdapter DB;
            private DataSet DS = new DataSet();
            private DataTable DT = new DataTable();
            private string environmentpath;
        public static void Main()
        {
            Class1 x = new Class1();
        }
            public Class1()
            {
                //  environmentpath = ConfigurationManager.ConnectionStrings["path"].ToString();
                environmentpath = @"D:\Clip.db";
                checkDB();
                checkTable();
            }
            private void checkDB()
            {
                using (sql_con = new SQLiteConnection())
                {
                    if (File.Exists(environmentpath))
                    {
                    }
                    else
                    {
                        SQLiteConnection.CreateFile(environmentpath);
                    }
                }
            }
            private void checkTable()
            {
                SetConnection();
                using (SQLiteCommand _command = new SQLiteCommand())
                {
                    _command.Connection = sql_con;
                    string query = "CREATE TABLE IF NOT EXISTS filedata ( id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, filetype VARCHAR(100) NOT NULL, " +
                     "filepath VARCHAR(100) NOT NULL);";
                    try
                    {
                        _command.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            private void SetConnection()
            {
                sql_con.ConnectionString = $"Data Source={environmentpath};Version=3";
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
            public DataTable LoadData()
            {
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = "select path from filedata";
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                return DT;
                CloseConnection();
            }
            private void AddData()
            {

            }
            private void DeleteData()
            {

            }
        }
    }


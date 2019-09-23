using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Configuration;
using System.Data;
using Models;

namespace DB
{
    public class DatabaseAccess
    {

            private SQLiteConnection sql_con;
            private SQLiteCommand sql_cmd;
            private SQLiteDataAdapter DB;
            private DataSet DS = new DataSet();
            private DataTable DT = new DataTable();
            private string environmentpath;
        private static FileData table;
  
            public DatabaseAccess()
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
                
             
                  
                    string query = "CREATE TABLE IF NOT EXISTS filedata ( id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, filetype VARCHAR(100) NOT NULL, " +
                     "filepath VARCHAR(100) NOT NULL);";
                    try
                    {

                 TableExecuteQuery(query);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                
            }
            private void SetConnection()
            {
            sql_con = new SQLiteConnection($"Data Source={environmentpath};Version=3");
              //  sql_con.ConnectionString = $"Data Source={environmentpath};Version=3";
                sql_con.Open();
            }
            private void TableExecuteQuery(string txtQuery)
            {

                SetConnection();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = txtQuery;
                sql_cmd.ExecuteNonQuery();
                CloseConnection();

            }
        private void UpdateQuery(string txtquery)
        {
            SetConnection();
            sql_cmd = sql_con.CreateCommand();
            DB = new SQLiteDataAdapter(txtquery, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            
            CloseConnection();
            
        }
            private void CloseConnection()
            {
                sql_con.Close();
            }
            public DataTable LoadData()
            {
             
                string CommandText = "select filepath from filedata";
            UpdateQuery(CommandText);
            return DT;
        }
            public void AddData(FileData data)
            {
           // SetConnection();
            string query = "insert into filedata (filetype,filepath) values('" + data.filetype + ","+data.filepath+"')";
            UpdateQuery(query);
            }
            private void DeleteData()
            {

            }
        }
    }


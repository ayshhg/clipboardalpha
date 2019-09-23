using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DB.Models;
using System.Data.Entity;
using System.Data;

namespace DB
{
    public class DatabaseEntity
    {
        DBEntityModel filecontext;
        private DataTable table=null;
        public DatabaseEntity()
        {
            filecontext = new DBEntityModel();
         
        }
        public List<FileData> LoadData()
        {
             filecontext.filedata.Load();
            /* table = ToDataTable(x);
             var testtable = table.DefaultView.Table;
             return x;
             */
          
            filecontext.SaveChangesAsync();
           // testAsync();
            var x =  filecontext.filedata.ToList();
            return x;
        }
        private DataTable ToDataTable<T>(T entity) where T : class
        {
            var properties = typeof(T).GetProperties();
            var table = new DataTable();

            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
            return table;
        }
        public async void testAsync()
        {
            try 
            {
                var x = new FileData("testing", "test");
               // filecontext.SaveChanges();
            //    var z = filecontext.filedata.Count();
                filecontext.filedata.Add(x);
                var z = filecontext.Database;
                var zz = filecontext.Configuration;
                await filecontext.SaveChangesAsync();
        //        var xs = filecontext.filedata;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}

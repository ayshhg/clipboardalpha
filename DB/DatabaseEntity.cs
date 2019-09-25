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
           
            filecontext.SaveChangesAsync();
    
            var x =  filecontext.filedata.ToList();
            return x;
        }
        public void AddData(FileData newdata)
        {
            if (filecontext.filedata.Contains(newdata))
                return;
            filecontext.filedata.Add(newdata);
            filecontext.SaveChangesAsync();
        }
        public void RemoveData(FileData removedata)
        {
  
                filecontext.filedata.Remove(removedata);
                filecontext.SaveChangesAsync();
           
        }
 
        public async void testAsync()
        {
            try 
            {
                var x = new FileData("testing", "test");
                filecontext.filedata.Add(x);
                var z = filecontext.Database;
                var zz = filecontext.Configuration;
                await filecontext.SaveChangesAsync();

            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}

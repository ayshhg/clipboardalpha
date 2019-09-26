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
        public List<ClipBoardModel> LoadData()
        {
           
            filecontext.SaveChangesAsync();
    
            var x =  filecontext.filedata.ToList();
            List<ClipBoardModel> loadata = new List<ClipBoardModel>();
            foreach(FileData temp in x)
            {
                loadata.Add(temp.Convert());
            }
            return loadata;
        }
        public void AddData(FileData newdata)
        {

            filecontext.filedata.Add(newdata);
            filecontext.SaveChangesAsync();
        }
        public void RemoveData(FileData removedata)
        {

            var row = filecontext.filedata.SingleOrDefault(x => x.filepath == removedata.filepath);
            if (row == null)
                return;
            filecontext.filedata.Remove(row);
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

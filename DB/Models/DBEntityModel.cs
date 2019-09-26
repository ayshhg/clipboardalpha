using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DB.Models
{
    public class DBEntityModel : DbContext
    {

        public DBEntityModel() : base("name=FileDataContextConnection")
        {
           
           
                Database.CreateIfNotExists();
                Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS 'FileData' ('filetype' VARCHAR(100) NOT NULL PRIMARY KEY, 'filepath' VARCHAR(100) NOT NULL);");

           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<FileData> filedata { get; set; }
    

      
    }
}

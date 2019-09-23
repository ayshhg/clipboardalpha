using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.Entity;

namespace DB.Models
{
    class DBInitializer : DropCreateDatabaseAlways<DBEntityModel>
    {
    /*    protected override void Seed(DBEntityModel context)
        {
            IList<FileDataModel> sample = new List<FileDataModel>();
            sample.Add(new FileDataModel() { id=1,filetype = "test", filepath = "test" });
            context.filedata.AddRange(sample);
            base.Seed(context);
        }
        */
    }
}

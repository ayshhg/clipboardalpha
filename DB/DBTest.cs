using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DB
{
    class DBTest
    {
        private static FileData table;
        public static void Main()
        {
            DatabaseEntity test = new DatabaseEntity();
          //  test.testAsync();
            test.LoadData();
           FileData x=new FileData("test", "testing");
            test.AddData(x);
            test.RemoveData(x);
;           /* DatabaseAccess x = new DatabaseAccess();
            table = new FileDataModel();
            table.filetype = "testtype";
            table.filepath = "testpath";

            x.AddData(table);
            DataTable test = x.LoadData();
            */
        }
    }
}

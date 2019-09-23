using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FileData
    {
        public FileData()
        {
            this.filepath = null;
            this.filetype = null;
        }
        public FileData(string filetype,string filepath)
        {
            this.filepath = filepath;
            this.filetype = filetype;
        }
        public FileData(int id,string filetype,string filepath)
        {
            this.id = id;
            this.filepath = filepath;
            this.filetype = filetype;
        }
        [Key]
        public int id { get; set; }

        public string filetype { get; set; }

        public string filepath { get; set; }

    }
}

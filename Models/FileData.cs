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
        public FileData(string filepath,string filetype)
        {
            this.filepath = filepath;
            this.filetype = filetype;
        }
        public ClipBoardModel Convert()
        {
            ClipBoardModel x = new ClipBoardModel(this.filepath, this.filetype);
            return x;
        }
      
       
        public string filetype { get; set; }
        [Key]
        public string filepath { get; set; }

    }
}

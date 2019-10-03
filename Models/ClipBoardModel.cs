using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    
    public class ClipBoardModel: INotifyPropertyChanged
    {
        public ClipBoardModel()
        {

        }
        public ClipBoardModel(string filepath,string filetype)
        {
            this.filepath = filepath;
            this.filetype = filetype;
        }
        public FileData Convert()
        {
            FileData data = new FileData(this.filepath, this.filetype);
            return data;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public dynamic filepath{get; set;}

        public string filetype { get; set; }
        

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

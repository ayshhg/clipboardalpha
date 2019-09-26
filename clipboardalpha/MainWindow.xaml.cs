using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClipBoardFrameWork;
using Models;
using DB;

namespace clipboardalpha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClipBoardFrame clipboard;
        ClipBoardViewModel eventresult = new ClipBoardViewModel();
      //  DatabaseAccess db = new DatabaseAccess();
        DatabaseEntity db = new DatabaseEntity();

        public MainWindow()
        {
           
            InitializeComponent();
           
            newclips.ItemsSource = eventresult;
         
        }
      public void FillDataGrid()
        {

            var loadata = db.LoadData();
         
            foreach(ClipBoardModel data in loadata)
            eventresult.Add(data);

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
           
            clipboard = new ClipBoardFrame(this);
            clipboard.ClipboardTextChanged += ClipboardTextChanged;
            FillDataGrid();
        }

        private  void ClipboardTextChanged(object sender, ClipBoardModel result)
        {
            var clippedText = result.filepath.Trim();

            if (string.IsNullOrWhiteSpace(clippedText))
            {
                return;
            }


            eventresult.Add(result);
          
            newclips.Items.MoveCurrentToLast();
            newclips.ScrollIntoView(newclips.Items.CurrentItem);
          
        }
        private void StoreData(object sender, RoutedEventArgs e)
        {
            
            ClipBoardModel newdata=FindClickedItem(sender);
            db.AddData(newdata.Convert());
          
        }
        private void RemoveData(object sender, RoutedEventArgs e)
        {

            ClipBoardModel datatoberemoved = FindClickedItem(sender);
            db.RemoveData(datatoberemoved.Convert());
            eventresult.Remove(datatoberemoved);

        }
        private static ClipBoardModel FindClickedItem(object sender)
        {
            var mi = sender as MenuItem;
            if (mi == null)
            {
                return null;
            }

            var x = mi.DataContext;
            if (x == null)
                return null;
            return x as ClipBoardModel;
          
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            clipboard.ClipboardTextChanged -= ClipboardTextChanged;
            clipboard.Dispose();
        }
    }
}

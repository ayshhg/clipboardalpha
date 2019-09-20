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
using DatabaseLayer;

namespace clipboardalpha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClipBoardFrame clipboard;
        ClipBoardViewModel eventresult = new ClipBoardViewModel();
        DatabaseAccess db = new DatabaseAccess();

        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
            newclips.ItemsSource = eventresult;
        }
      public void FillDataGrid()
        {
            newclips.ItemsSource = db.LoadData().DefaultView;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            clipboard = new ClipBoardFrame(this);
            clipboard.ClipboardTextChanged += ClipboardTextChanged;
        }
        private List<ClipInfo> cliplist()
        {
            bool IsHTMLDataOnClipboard = Clipboard.ContainsData(DataFormats.Text);
            string htmlData = null;
            if (IsHTMLDataOnClipboard)
            {

                htmlData = Clipboard.GetText(TextDataFormat.Text);

            }
            List<ClipInfo> test = new List<ClipInfo>();
            test.Add(new ClipInfo() { Text = htmlData });
            return test;
        }
        private  void ClipboardTextChanged(object sender, ClipBoardModel result)
        {
            var clippedText = result.data.Trim();

            if (string.IsNullOrWhiteSpace(clippedText))
            {
                return;
            }


            eventresult.Add(result);
          
            newclips.Items.MoveCurrentToLast();
            newclips.ScrollIntoView(newclips.Items.CurrentItem);
          
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            clipboard.ClipboardTextChanged -= ClipboardTextChanged;
            clipboard.Dispose();
        }
    }
}

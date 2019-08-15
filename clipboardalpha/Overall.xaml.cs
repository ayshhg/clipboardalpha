using System;
using System.Collections.Generic;
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

namespace clipboardalpha
{
    /// <summary>
    /// Interaction logic for Overall.xaml
    /// </summary>
    public partial class Overall : Page
    {
        public Overall()
        {
            InitializeComponent();
            newclips.ItemsSource = cliplist();
        }
        private List<ClipInfo> cliplist()
        {
            bool IsHTMLDataOnClipboard = Clipboard.ContainsData(DataFormats.Text);
            string htmlData=null;
            if (IsHTMLDataOnClipboard)
            {

                htmlData = Clipboard.GetText(TextDataFormat.Text);

            }
            List<ClipInfo> test = new List<ClipInfo>();
            test.Add(new ClipInfo() { Text = htmlData });
            return test;
        }
    }
}

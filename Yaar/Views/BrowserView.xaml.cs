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
using System.Windows.Shapes;

namespace Yaar.Views
{
    /// <summary>
    /// Interaction logic for BrowserView.xaml
    /// </summary>
    public partial class BrowserView : Window
    {
        public static void Create(string url)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var view = new BrowserView(url) {Left = 10, Top = 10};
                    view.Show();
                });
        }

        public BrowserView(string url)
        {
            InitializeComponent();
            Browser.Loaded += (sender, args) => Browser.Navigate(new Uri(url));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for EmailView.xaml
    /// </summary>
    public partial class EmailView : Slideable
    {
        public EmailView(string from, string subject, string link)
        {
            InitializeComponent();

            this.From.Content = from;
            this.Subject.Text = subject;

            this.MouseLeftButtonUp += (sender, args) => Process.Start(link);
        }

        public static void Create(string from, string subject, string link)
        {
            Application.Current.Dispatcher.Invoke(() =>
                                                      {
                                                          var view = new EmailView(from, subject, link);
                                                          view.SlideIn();
                                                      });
        }
    }
}

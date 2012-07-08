using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for TweetView.xaml
    /// </summary>
    public partial class TweetView : Slideable
    {
        public TweetView(string tweet, string user)
        {
            InitializeComponent();

            this.Tweet.Text = tweet;
            this.User.Text = user;
        }

        public static void Create(string tweet, string user)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var view = new TweetView(tweet, user);
                    view.SlideIn();
                });
        }
    }
}

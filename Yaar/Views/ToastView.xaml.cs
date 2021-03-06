﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ToastView.xaml
    /// </summary>
    public partial class ToastView : Slideable
    {
        public ToastView(string tweet, string user)
        {
            InitializeComponent();

            this.Tweet.Text = tweet;
            this.User.Text = user;
        }
        
        public ToastView(string tweet, string user, string link) : this(tweet, user)
        {
            this.MouseLeftButtonUp += (sender, args) => Process.Start(link);
        }

        public static void Create(string tweet, string user, string link, bool temporary)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var view = new ToastView(tweet, user, link);
                    view.SlideIn();
                    if (temporary)
                    {
                        new Thread(() =>
                        {
                            Thread.Sleep(4.Seconds());
                            if (view.IsVisible)
                                view.SlideOut();
                        }).Start();
                    }
                });
        }

        public static void Create(string tweet, string user, bool temporary)
        {
            Application.Current.Dispatcher.Invoke(() =>
                                                      {
                                                          var view = new ToastView(tweet, user);
                                                          view.SlideIn();
                                                          if(temporary)
                                                          {
                                                              new Thread(() =>
                                                                             {
                                                                                 Thread.Sleep(4.Seconds());
                                                                                 if(view.IsVisible)
                                                                                    view.SlideOut();
                                                                             }).Start();
                                                          }
                                                      });
        }
    }
}

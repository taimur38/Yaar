using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Yaar.Views
{
    public class Slideable : Window
    {
        public Slideable()
        {
            this.Left = SystemParameters.PrimaryScreenWidth;
            this.Top = SlideableManager.AddFadeable(this);
            this.MouseRightButtonUp += (sender, args) => this.SlideOut();
            this.MouseLeftButtonUp += (sender, args) => this.SlideOut();
            this.MouseDown +=
                (sender, args) =>
                    {
                        if (args.ChangedButton == MouseButton.Middle && args.ButtonState == MouseButtonState.Pressed)
                            SlideableManager.DismissAll();
                    };
            this.MouseWheel += (sender, args) =>
                                   {
                                       if(args.Delta > 0)
                                           SlideableManager.ScrollUp();
                                       else
                                           SlideableManager.ScrollDown();
                                   };
        }

        public void SlideIn()
        {
            Application.Current.Dispatcher.Invoke(() =>
                                                      {
                                                          this.Show();
                                                          var slide = new DoubleAnimation(-340, 10, 400.Milliseconds());
                                                          this.BeginAnimation(LeftProperty, slide);
                                                      });
        }

        public void SlideOut()
        {
            Application.Current.Dispatcher.Invoke(() =>
                                                      {
                                                          var slideout = new DoubleAnimation(Top, -Height,
                                                                                             400.Milliseconds());
                                                          slideout.Completed += (sender, args) => this.Close();
                                                          this.BeginAnimation(TopProperty, slideout);
                                                          SlideableManager.RemoveFadeable(this);
                                                      });
        }

        public void SlideTo(double x, double y)
        {
            Application.Current.Dispatcher.Invoke(() =>
                                                      {
                                                          var slidex = new DoubleAnimation(Left, x, 400.Milliseconds());
                                                          var slidey = new DoubleAnimation(Top, y, 400.Milliseconds());
                                                          BeginAnimation(LeftProperty, slidex);
                                                          BeginAnimation(TopProperty, slidey);
                                                      });
        }
    }
}

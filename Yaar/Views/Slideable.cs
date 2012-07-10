using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        }

        public void SlideIn()
        {
            this.Show();
            var slide = new DoubleAnimation(-340, 10, 400.Milliseconds());
            this.BeginAnimation(LeftProperty, slide);
        }

        public void SlideOut()
        {
            var slideout = new DoubleAnimation(Top, -Height, 400.Milliseconds());
            slideout.Completed += (sender, args) => this.Close();
            this.BeginAnimation(TopProperty, slideout);
            SlideableManager.RemoveFadeable(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Yaar.Views
{
    public class Fadeable : Window
    {
        public Fadeable()
        {
            this.Left = SystemParameters.PrimaryScreenWidth - 220;
            this.Top = FadeableManager.AddFadeable(this);
            this.MouseLeftButtonUp += (sender, args) => this.FadeOut();
        }

        public void FadeIn()
        {
            Opacity = 0;
            this.Show();
            var da = new DoubleAnimation(0, 1.0, 300.Milliseconds());
            this.BeginAnimation(OpacityProperty, da);
        }

        public void FadeOut()
        {
            var da = new DoubleAnimation(1.0, 0, 200.Milliseconds());
            da.Completed += (sender, args) => this.Close();
            this.BeginAnimation(OpacityProperty, da);
            FadeableManager.RemoveFadeable(this);
        }
    }
}

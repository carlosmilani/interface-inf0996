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
using CommunityToolkit.Mvvm.Messaging;
using Interface_0996.ViewModel;
using System.Windows.Threading;

namespace Interface_0996
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            DataContext = new ViewModel.PlayListViewModel();
            WeakReferenceMessenger.Default.Register<PlayListViewModel>(this, (r, m) =>
            {
                if (m.MidiaAtual.MediaState == "Stopped")
                {
                    me.Close();
                }
                else if (m.MidiaAtual.MediaState == "Paused")
                {
                    me.Pause();
                }
                else
                {
                // TimeSpan ts = me.NaturalDuration.TimeSpan;
                // slider_seek.Maximum = ts.TotalSeconds;
                timer.Start();
                me.Play();
                }
            });
        }
        void timer_Tick(object sender, EventArgs e)
        {
            slider_seek.Value = me.Position.TotalSeconds;
        }
        private void MediaPlayer(object sender, RoutedEventArgs e)
        {

        }
        private void SeekSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
             me.Position = TimeSpan.FromSeconds(this.slider_seek.Value);

		}
        private void VolSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			me.Volume = this.slider_vol.Value;
            slider_vol.Value.ToString();
		}
    }
}
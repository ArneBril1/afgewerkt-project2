using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;
using System.IO.Ports;
using System.Windows.Threading;
using System.Xml;

namespace project_in_wpf2
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _dispatcherTimer;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        serielepoort _serielepoort = new serielepoort("COM7", 9600);
        int a;
        public MainWindow()
        {
            InitializeComponent();
            timedisplay.Text = Class2.starttime;
            showlcd();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:000}",
                ts.Minutes, ts.Seconds, ts.Milliseconds);
                timedisplay.Text = currentTime;
            }
        }
        public void showlcd()
        {
            _serielepoort.showlcd(timedisplay.Text);
        }
        public void Start_Click(object sender, RoutedEventArgs e)
        {
            DispatchTimer();
            sw.Start();
            dt.Start();
            Reset.IsEnabled = false;
            Stop.IsEnabled = true;
            Start.IsEnabled = false;
        }
        public void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
            }
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            Reset.IsEnabled = true;
            Textblock2.Inlines.Add(new Run { Text = timedisplay.Text });
            Textblock2.Inlines.Add(new LineBreak());
            a = a+1;
            if (a==5)
            {
                Textblock2.Text = "";
                a = 0;
            }
        }
        public void Reset_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            timedisplay.Text = Class2.starttime;
        }
        public void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void DispatchTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(900);
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            showlcd(); 
        }
    }
}

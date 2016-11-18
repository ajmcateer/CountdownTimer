using NodaTime;
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
using System.Windows.Threading;

namespace CountdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime EventDT;
        LocalDateTime LocalizedEventDT;
        Period TimeLeft;

        public MainWindow()
        {
            this.Top = Properties.Settings.Default.WindowTop;
            this.Left = Properties.Settings.Default.WindowLeft;

            InitializeComponent();
            //Start with a date and time
            EventDT = Convert.ToDateTime("11/03/2020 7:00AM");

            // Localize it
            LocalizedEventDT = new LocalDateTime(
                EventDT.Year, EventDT.Month,
                EventDT.Day, EventDT.Hour,
                EventDT.Minute, 0);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += Dt_Tick;
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Start();

        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            TimeLeft = GetPeriodRemaining();

            lblYears.Content = TimeLeft.Years.ToString("D2");
            lblMonths.Content = TimeLeft.Months.ToString("D2");
            lblDays.Content = TimeLeft.Days.ToString("D2");
            lblHours.Content = TimeLeft.Hours.ToString("D2");
            lblMinutes.Content = TimeLeft.Minutes.ToString("D2");
            lblSec.Content = TimeLeft.Seconds.ToString("D2");
        }

        public Period GetPeriodRemaining()
        {
            DateTime dt_Now = DateTime.Now;

            return Period.Between(new LocalDateTime(
                dt_Now.Year, dt_Now.Month, dt_Now.Day, dt_Now.Hour,
                dt_Now.Minute, dt_Now.Second), LocalizedEventDT);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.Save();
        }
    }
}

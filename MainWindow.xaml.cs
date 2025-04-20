using BeautifulWeather.ViewModels;
using System.Windows;
using System.Windows.Media;

namespace BeautifulWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;

            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var hour = DateTime.Now.Hour;
            LinearGradientBrush gradient;

            if (hour >=6 && hour <= 24)
            {
                gradient = new LinearGradientBrush()
                {
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop((Color)ColorConverter.ConvertFromString("#FFC371"), 0),
                        new GradientStop((Color)ColorConverter.ConvertFromString("#FF5F60"), 1)
                    }
                };
            }
            else
            {
                gradient = new LinearGradientBrush()
                {
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop(Colors.Black, 0),
                        new GradientStop(Colors.Blue, 1),
                    }
                };
            }

            Application.Current.Resources["MainWindowBackground"] = gradient;
        }
    }
}
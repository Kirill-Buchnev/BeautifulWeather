using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeautifulWeather.Views.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private ScrollViewer scrollViewer;
        public HomeView()
        {
            InitializeComponent();
            scrollViewer = GetDescendantByType(WeatherDays_ListBox, typeof(ScrollViewer)) as ScrollViewer;
            ScrollToLeft_Button.Click += ScrollToLeft_Button_Click;
            ScrollToRight_Button.Click += ScrollToRight_Button_Click;
        }

        private void ScrollToRight_Button_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer.LineRight();
        }

        private void ScrollToLeft_Button_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer.LineLeft();
        }

        private static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }
            if (element.GetType() == type)
            {
                return element;
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }
    }
}

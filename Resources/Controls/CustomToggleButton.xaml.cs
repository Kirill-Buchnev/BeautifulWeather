using System.Windows;
using System.Windows.Controls.Primitives;

namespace BeautifulWeather.Resources.Controls
{
    /// <summary>
    /// Interaction logic for CustomToggleButton.xaml
    /// </summary>
    public partial class CustomToggleButton : ToggleButton
    {
        public CustomToggleButton()
        {
            InitializeComponent();
        }

        public string LeftCase
        {
            get { return (string)GetValue(LeftCaseProperty); }
            set { SetValue(LeftCaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftCaseProperty =
            DependencyProperty.Register("LeftCase", typeof(string), typeof(CustomToggleButton));

        public string RightCase
        {
            get { return (string)GetValue(RightCaseProperty); }
            set { SetValue(RightCaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightCaseProperty =
            DependencyProperty.Register("RightCase", typeof(string), typeof(CustomToggleButton));
    }
}

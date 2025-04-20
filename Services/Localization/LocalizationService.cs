using BeautifulWeather.Views.Settings;
using System.Globalization;
using System.Windows;

namespace BeautifulWeather.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private Dictionary<Cultures, ResourceDictionary> cultureDictionary = new Dictionary<Cultures, ResourceDictionary>()
        {
            {Cultures.EN, new ResourceDictionary() {Source = new Uri("Resources/Localization/Language.en-US.xaml", UriKind.RelativeOrAbsolute)} },
            {Cultures.RU, new ResourceDictionary() {Source = new Uri("Resources/Localization/Language.ru-RU.xaml", UriKind.RelativeOrAbsolute)} }
        };
        public void SetCulture(Cultures culture)
        {
            App.Current.Resources.MergedDictionaries.Add(cultureDictionary[culture]);
            CultureInfo cultureInfo = new CultureInfo(App.Current.Resources["lang"].ToString());
            CultureInfo.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulWeather.Services
{
    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
}

namespace BeautifulWeather.Services.GeoCoder
{
    public class ApiResponse
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public Geoobjectcollection GeoObjectCollection { get; set; }
    }

    public class Geoobjectcollection
    {
        public Featuremember[] FeatureMember { get; set; }
    }


    public class Featuremember
    {
        public Geoobject GeoObject { get; set; }
    }

    public class Geoobject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Point Point { get; set; }
    }

    public class Point
    {
        public string Pos { get; set; }
    }
}
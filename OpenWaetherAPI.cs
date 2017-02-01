using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class OpenWaetherAPI
    {
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            var httpClient = new HttpClient();
            var url = string.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=imperial&appid=8e32da9afccbd4509187132ee86cebee", lat,lon);
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var memoryStream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(memoryStream);

            return data;
        }
    }
    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public int population { get; set; }
    }

    [DataContract]
    public class City
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int population { get; set; }
        [DataMember]
        public Sys sys { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
        [DataMember]
        public double pressure { get; set; }
        [DataMember]
        public double sea_level { get; set; }
        [DataMember]
        public double grnd_level { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_kf { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }
        [DataMember]
        public double deg { get; set; }
    }

    /*
    public class Rain
    {
        public double __invalid_name__3h { get; set; }
    }
    */

    [DataContract]
    public class Sys2
    {
        [DataMember]
        public string pod { get; set; }
    }

    [DataContract]
    public class List
    {
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        //public Rain rain { get; set; }

        [DataMember]
        public Sys2 sys { get; set; }

        [DataMember]
        public string dt_txt { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public City city { get; set; }
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public int cnt { get; set; }
        [DataMember]
        public List<List> list { get; set; }
    }
}

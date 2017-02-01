using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Weather
{
    public class LocationManager
    {
        public async static Task<Geoposition> GetLocationPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();
            var geoLocator = new Geolocator { DesiredAccuracyInMeters=0};
            var position = await geoLocator.GetGeopositionAsync();
            return position;


        }
    }
}

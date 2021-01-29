using System;
using System.Diagnostics;
using System.Net;
using System.Device.Location;

namespace GeoLocation
{
    class MapImage
    {
        public static void Show(GeoCoordinate location)
        {
            string filename = $"{location.Latitude:##.000},{location.Longitude:##.000},{location.HorizontalAccuracy:####}m.jpg";

            DownloadMapImage(BuildURI(location), filename);

            OpenWthDefaultApp(filename);
        }

        private static void DownloadMapImage(Uri target, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(target, filename);
            }
        }

        /// <summary>
        /// Map Image REST API by HERE Location Services
        /// </summary>
        /// <remarks>
        /// https://developer.here.com/
        /// </remarks>

        private static Uri BuildURI(GeoCoordinate location)
        {
            #region HERE App ID & App Code
            string HereApi_AppID = "BpJ4qijas6smWHBJFCXo";
            string HereApi_AppCode = "6Io4_d8-h9D2WSuinBfhjA";
            #endregion

            var HereApi_DNS = "image.maps.cit.api.here.com";
            var HereApi_URL = $"https://{HereApi_DNS}/mia/1.6/mapview";
            var HereApi_Secrets = $"&app_id={HereApi_AppID}&app_code={HereApi_AppCode}";

            var latlon = $"&lat={location.Latitude}&lon={location.Longitude}";

            return new Uri(HereApi_URL + $"?u={location.HorizontalAccuracy}" + HereApi_Secrets + latlon);
        }

        private static void OpenWthDefaultApp(string filename)
        {
            var si = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C start {filename}",
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(si);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace GeoUsersUI.GoogleMaps
{
    public class GoogleGeoCodeResponse
    {
        public results[] results { get; set; }
        public string status { get; set; }


    }

    public class results
    {
        public address_component[] address_components { get; set; }
        public string formatted_address { get; set; }
        public geometry geometry { get; set; }
        public string[] types { get; set; }
    }

    public class address_component
    {
        String long_name { get; set; }
        String short_name { get; set; }
        String types { get; set; }

    }

    public class geometry
    {
        public bounds bounds { get; set; }
        public location location { get; set; }
        public string location_type { get; set; }
        public viewport viewport { get; set; }
    }

    public class location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class viewport
    {
        public northeast northeast { get; set; }
        public southwest southwest { get; set; }
    }

    public class bounds
    {
        public northeast northeast { get; set; }
    }

    public class northeast
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class southwest
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Cordenada
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public bool status { get; set; }
    }

    public class Geocode
    {

        public Cordenada gerLatLong(string direccion)
        {
            string Address = direccion.Replace(" ", "+");
            string AddressURL = "http://maps.google.com/maps/api/geocode/json?address=" + Address;
            var cordenada = new Cordenada();
            cordenada.status = false;
            using (var webClient = new System.Net.WebClient())
            {
                Newtonsoft.Json.Linq.JObject json;
                GoogleGeoCodeResponse dirObjet;
                try
                {
                    json = Newtonsoft.Json.Linq.JObject.Parse(webClient.DownloadString(AddressURL));

                    dirObjet = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json.ToString());
                }
                catch (WebException)
                {
                    //alert here ("No se puede acceder a la url solicitada, revise su conexión a internet.");
                    return cordenada;
                }
                cordenada.lat = dirObjet.results[0].geometry.location.lat.ToString();
                cordenada.lon = dirObjet.results[0].geometry.location.lng.ToString();
                if (cordenada.lat != "" && cordenada.lon != "")
                {
                    cordenada.status = true;
                }
                return cordenada;
            }

        }
    }
}

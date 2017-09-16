using GeoUsers.Services.Model.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace GeoUsers.Services.Utils
{
    public static class WebUtils
    {
        public static Location GetCoordinates(string direccion)
        {
            var address = direccion.Replace(" ", "+");

            var downloadUrl = "http://maps.google.com/maps/api/geocode/json?address=" + address;

            using (var webClient = new WebClient())
            {
                JObject json;
                GoogleGeoCodeResponse dirObjet;

                try
                {
                    json = JObject.Parse(webClient.DownloadString(downloadUrl));

                    dirObjet = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json.ToString());
                }
                catch (WebException)
                {
                    throw new Exception("No se pueden obtener las coordenadas de la organizacion. Revise su conexion a internet");
                }

                return new Location()
                {
                    lat = dirObjet.results[0].geometry.location.lat.ToString(),
                    lng = dirObjet.results[0].geometry.location.lng.ToString()
                };
            }
        }

        public static string GetFileDataUri(Archivo file)
        {
            if (!File.Exists(file.Ruta)) return null;

            var dataUri = FileUtils.GetBase64EncodedFileBytes(file.Ruta);

            return $"data:image/{file.Extension};base64,{dataUri}";
        }
    }
}

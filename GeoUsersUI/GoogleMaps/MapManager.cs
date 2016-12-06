﻿//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Net;

namespace GeoUsersUI.GoogleMaps
{
    public class MapManager
    {
        private string centerLatitude = "0";
        private string centerLongitude = "0";
        private string markers = "";

        public string getHtmlString()
        {
            string line = "<!DOCTYPE html><html>" +
                               "<head>" +
                               "<style>" +
                               "#map { width: 100 %; height: 400px;}" +
                               "</style>" +
                               "</head>" +
                               "<body>" +
                               "<div id = \"map\" ></ div >" +
                               "<script>" +

                               "function noError(){ return true; }" +
                              "window.onerror = noError;" +


                               "function initMap(){" +

                                 "var locations = [ " +
                                 markers +
                                 "];" +

                                 "var mapDiv = document.getElementById('map');" +
                                 "var map = new google.maps.Map(mapDiv, {" +
                                    "center: { lat: 0, lng: 0}," +
                                    "zoom: 13 });" +

                                  "var infowindow = new google.maps.InfoWindow();" +
                                  "var marker, i;" +
                                  "var bounds = new google.maps.LatLngBounds();" +

                                  "for (i = 0; i < locations.length; i++){" +
                                      "marker = new google.maps.Marker({" +
                                       "position: new google.maps.LatLng(locations[i][1], locations[i][2])," +
                                       "map: map });" +

                                       "bounds.extend(marker.getPosition());" +

                                       "google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
                                       "return function() {" +
                                       "infowindow.setContent(locations[i][0]);" +
                                       "infowindow.open(map, marker);}" +
                                      "})(marker, i));" +
                                  "}" +
                                  "map.fitBounds(bounds);" +
                              "}" +



                              "</script>" +
                              "<script async defer src = \"https://maps.googleapis.com/maps/api/js?v=3.17key=AIzaSyBGw_Saj9-jiCl382ENV3fkrA2aeYqIBuI&callback=initMap \" >" +
                              "</script>" +
                              "</body>" +
                              "</html>";

            return line;
        }



        public void addDireccion(string direccion, string name)
        {
            string Address = direccion.Replace(" ", "+");
            string AddressURL = "http://maps.google.com/maps/api/geocode/json?address=" + Address;

            using (var webClient = new System.Net.WebClient())
            {
                //JObject json;
                //GoogleGeoCodeResponse dirObjet;

                //try
                //{
                //    json = JObject.Parse(webClient.DownloadString(AddressURL));

                //    dirObjet = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(json.ToString());
                //}
                //catch (WebException)
                //{
                //    //alert here ("No se puede acceder a la url solicitada, revise su conexión a internet.");
                //    return;
                //}

                //centerLatitude = dirObjet.results[0].geometry.location.lat.ToString();
                //centerLongitude = dirObjet.results[0].geometry.location.lng.ToString();
                var geoPos = new Geocode().gerLatLong(direccion);
                //centerLatitude = geoPos.lat;
                //centerLongitude = geoPos.lon;

                if (geoPos.status)
                {
                    if (markers.Length == 0)
                    {
                        markers = markers + "['" + name + "' , " + geoPos.lat + "," + geoPos.lon + "]";
                    }
                    else
                    {
                        markers = markers + ",['" + name + "' , " + geoPos.lat + "," + geoPos.lon + "]";
                    }
                }
            }
        }

        public void reset()
        {
            markers = "";
            centerLatitude = "";
            centerLongitude = "";
        }
    }
}

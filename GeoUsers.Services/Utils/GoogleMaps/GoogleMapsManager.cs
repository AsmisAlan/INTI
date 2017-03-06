using GeoUsers.Services.Model.DataTransfer;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsersUI.GoogleMaps
{
    public class GoogleMapsManager
    {
        public string GetHtmlString(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
            string markers = string.Empty;

            foreach (var organizacion in organizaciones)
            {
                var marker = $"['{organizacion.Nombre}' , {organizacion.Latitud},{organizacion.Longitud}]";

                if (markers.Length == 0)
                {
                    markers = markers + marker;
                }
                else
                {
                    markers = $"{markers},{marker}";
                }
            }

            var centerLatitude = organizaciones.Any() ? organizaciones.FirstOrDefault().Latitud : "0";
            var centerLongitude = organizaciones.Any() ? organizaciones.FirstOrDefault().Longitud : "0";

            string line = "<!DOCTYPE html><html>" +
                               "<head>" +
                               "<style>" +
                               "#map { width: 100 %; height: 90vh}" +
                               "</style>" +
                               "</head>" +
                               "<body>" +
                               "<div id = \"map\" ></ div >" +
                               "<script>" +
                               "function initMap(){" +
                                 "var locations = [ " +
                                 markers +
                                 "];" +
                                 "var mapDiv = document.getElementById('map');" +
                                 "var map = new google.maps.Map(mapDiv, {" +
                                    "center: { lat: " + centerLatitude + ", lng: " + centerLongitude + "}," +
                                    "zoom: 13 });" +

                                  "var infowindow = new google.maps.InfoWindow();" +

                                  "for (i = 0; i < locations.length; i++){" +
                                      "marker = new google.maps.Marker({" +
                                       "position: new google.maps.LatLng(locations[i][1], locations[i][2])," +
                                       "map: map });" +

                                       "google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
                                       "return function() {" +
                                       "infowindow.setContent(locations[i][0]);" +
                                       "infowindow.open(map, marker);}" +
                                      "})(marker, i));" +
                                  "}" +
                              "}" +
                              "</script>" +
                              "<script async defer src = \"https://maps.googleapis.com/maps/api/js?key=AIzaSyBGw_Saj9-jiCl382ENV3fkrA2aeYqIBuI&callback=initMap \" >" +
                              "</script>" +
                              "</body>" +
                              "</html>";

            return line;
        }
    }
}

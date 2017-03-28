using GeoUsers.Services.Model.DataTransfer;
using System.Collections.Generic;

namespace GeoUsersUI.GoogleMaps
{
    public class GoogleMapsManager
    {
        public string GetHtmlString(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
            string markers = string.Empty;

            foreach (var organizacion in organizaciones)
            {
                var defaultIcon = "https://cdn2.iconfinder.com/data/icons/connectivity/32/navigation-128.png";

                var marker = $"{{\"name\": \"{organizacion.Nombre}\"," +
                             $" \"contacto\": \"{organizacion.ContactoCargo}\"," +
                             $" \"email\": \"{organizacion.Email}\"," +
                             $" \"tel\": \"{organizacion.Telefono}\"," +
                             $" \"lat\": \"{organizacion.Latitud}\"," +
                             $" \"lng\": \"{organizacion.Longitud}\"," +
                             $" \"direc\": \"{organizacion.Direccion}\"," +
                             $" \"icon\": \"{(string.IsNullOrEmpty(organizacion.Icono) ? defaultIcon : organizacion.Icono)}\"," +
                             $" \"isInti\": {organizacion.UsuarioInti.ToString().ToLower()} " +
                             $"}}";

                if (markers.Length == 0)
                {
                    markers = markers + marker;
                }
                else
                {
                    markers = $"{markers}, {marker}";
                }
            }

            string line = "<!DOCTYPE html><html>" +
                           "<link rel=\"stylesheet\" href=\"https://www.w3schools.com/lib/w3.css\">" +
                           "<head>" +
                           "<style>" +
                           "#map { width: 100 %; height: 100vh;}" +
                           "</style>" +
                           "</head>" +
                           "<body>" +
                           "<div id = \"map\" ></ div >" +
                           "<script>" +
                           "function getInfoWindows(object){" +
                                "var color = object.isInti?  'w3-green' : 'w3-red'; " +
                                "return '<div class=\"w3-card-4\" style=\"width:100%; height:100%;\">' +" +
                                    "'<header class=\"w3-container '+color+' \">' +" +
                                        "'<h6>'+object.name+'</h6>' +" +
                                    "'</header>' +" +

                                    "'<div class=\"w3-container\">' +" +
                                    "    ' <p>Contacto: '+object.contacto+'</p>' +" +
                                   "     ' <p>Telefono:'+ object.tel+'</p>' +" +
                                    "    ' <p>Mail: '+object.email+'</p>' +" +
                                   " ' </div>' +" +

                                   " ' <footer class=\"w3-container '+color+'\">' +" +
                                   "     ' <p>Direccion: '+object.direc +" +
                                    "' </p> </footer>' +" +
                                    "'</div>'}" +
                            "function initMap(){" +
                             "var locations = [ " +
                             markers +
                             "];" +
                             "var mapDiv = document.getElementById('map');" +
                             "var map = new google.maps.Map(mapDiv, {" +
                                "zoom: 13 });" +
                              "var infowindow = new google.maps.InfoWindow();" +
                              "var bounds = new google.maps.LatLngBounds();" +
                              "for (i = 0; i < locations.length; i++){" +
                                   //creador de circulos   
                                   "if(locations[i].isInti){" +
                                   " var cityCircle = new google.maps.Circle({" +
                                        "strokeColor: '#98FB98'," +
                                        "strokeOpacity: 0.8," +
                                        "strokeWeight: 2," +
                                        "fillColor: '#98FB98'," +
                                        "fillOpacity: 0.35," +
                                        "map: map," +
                                        "center: new google.maps.LatLng(locations[i].lat, locations[i].lng)," +
                                        "radius: 2000" +
                                      "})}" +
                                   "var pinIcon = new google.maps.MarkerImage(" +
                                        "locations[i].icon ," +
                                        "null, /* size is determined at runtime */" +
                                        "null, /* origin is 0,0 */" +
                                        "null, /* anchor is bottom center of the scaled image */" +
                                        "new google.maps.Size(45, 45)" +
                                   " );" +
                                            "bounds.extend(new google.maps.LatLng(locations[i].lat, locations[i].lng));" +
                                            "marker = new google.maps.Marker({" +
                                            "position: new google.maps.LatLng(locations[i].lat, locations[i].lng)," +
                                            "map: map," +
                                            "icon: pinIcon," +
                                            "animation: google.maps.Animation.DROP" +
                                            " });" +
                                   "google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
                                   "return function() {" +
                                   "if (marker.getAnimation() !== null){" +
                                   "   marker.setAnimation(null);" +
                                   "}else{" +
                                   "    marker.setAnimation( google.maps.Animation.DROP);" +
                                   "}" +

                                   "infowindow.setContent(getInfoWindows(locations[i]));" +
                                   "infowindow.open(map, marker);}" +
                                  "})(marker, i));" +
                              "}" +
                              "map.fitBounds(bounds);" +
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

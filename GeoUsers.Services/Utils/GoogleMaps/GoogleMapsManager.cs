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

                               //"function getInfoWindows(isInti,name,tel,email,direc,contact){"+
                               "function getInfoWindows(objet){" +
                                    "var color = objet.isInti?  'w3-green' : 'w3-red'; " +
                                    "return '<div class=\"w3-card-4\" style=\"width:100%; height:100%;\">' +" +
                                        "'<header class=\"w3-container '+color+' \">' +" +
                                            "'<h6>'+objet.name+'</h6>' +" +
                                        "'</header>' +" +

                                        "'<div class=\"w3-container\">' +" +
                                        "    ' <p>Contacto: '+objet.contacto+'</p>' +" +
                                       "     ' <p>Telefono:'+ objet.tel+'</p>' +" +
                                        "    ' <p>mail: '+objet.email+'</p>' +" +
                                       " ' </div>' +" +

                                       " ' <footer class=\"w3-container '+color+'\">' +" +
                                       "     ' <p>direccion: '+objet.direc +" +
                                        "' </p> </footer>' +" +
                                        "'</div>'}" +






                                "function initMap(){" +

                                 "var locations = [ " +
                                 markers +

                                 "];" +

                                 "var mapDiv = document.getElementById('map');" +
                                 "var map = new google.maps.Map(mapDiv, {" +
                                    //"center: { lat: " + centerLatitude + ", lng: " + centerLongitude + "}," +
                                    "zoom: 13 });" +

                                  "var infowindow = new google.maps.InfoWindow();" +
                                  "var bounds = new google.maps.LatLngBounds();" +

                                  "for (i = 0; i < locations.length; i++){" +

                                       //creador de circulos   
                                       "if(locations[i].isInti){" +

                                       "var circleIcon = new google.maps.MarkerImage(" +
                                            "'http://orig09.deviantart.net/4a3c/f/2011/338/e/f/the_green_circle___emblem___logo_by_exxp0-d4i51r3.png' ," +

                                            "null, /* size is determined at runtime */" +
                                            "new google.maps.Point(0, 0), /* origin is 0,0 */" +
                                            "new google.maps.Point(13, 15), /* anchor is bottom center of the scaled image */" +
                                            "new google.maps.Size(25, 25)" +
                                       " );}else{" +
                                       "var circleIcon = new google.maps.MarkerImage(" +
                                            "'http://tecfa.unige.ch/guides/svg/ex/html5/svg-import/huge-red-circle.svg' ," +

                                            "null, /* size is determined at runtime */" +
                                            "new google.maps.Point(0, 0), /* origin is 0,0 */" +
                                            "new google.maps.Point(13, 15), /* anchor is bottom center of the scaled image */" +
                                            "new google.maps.Size(25, 25)" +
                                       " );}" +

                                        //nuevo
                                        "marker = new google.maps.Marker({" +
                                                "position: new google.maps.LatLng(locations[i].lat, locations[i].lng)," +
                                                "map: map," +
                                                "icon: circleIcon" +
                                        " });" +

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

                                       //"window.setTimeout(function(){marker} , i * 200);"+

                                       "google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
                                       "return function() {" +
                                       "infowindow.setContent(getInfoWindows(locations[i]));" +
                                       "infowindow.open(map, marker);}" +
                                      "})(marker, i));" +
                                  "}" +

                                  "map.fitBounds(bounds);" +
                              "}" +
                              "</script>" +
                              "<script async defer src = \"https://maps.googleapis.com/maps/api/js?key=AIzaSyC0R2S8BI5otvzqP8E7xY4iHubEee_lz8Y&callback=initMap \" >" +
                              "</script>" +
                              "</body>" +
                              "</html>";

            return line;
        }
    }
}

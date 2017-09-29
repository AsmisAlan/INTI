using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Utils;
using System.Collections.Generic;

namespace GeoUsersUI.GoogleMaps
{
    public class GoogleMapsManager
    {
        //constants
        private const string G_MAP_API = "https://maps.googleapis.com/maps/api/js?key=AIzaSyC0R2S8BI5otvzqP8E7xY4iHubEee_lz8Y&callback=initMap";
        private const string G_MAP_API_CLUSTER = "https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/markerclusterer.js";

        private const string INTI_BUBBLE = "http://orig09.deviantart.net/4a3c/f/2011/338/e/f/the_green_circle___emblem___logo_by_exxp0-d4i51r3.png";
        private const string NO_INTI_BUBBLE = "http://tecfa.unige.ch/guides/svg/ex/html5/svg-import/huge-red-circle.svg";

        //vars for the js script
        private string markerClusterOptionsVar = "markerClusterOptions";

        //functions for the map
        private string newMarkerBubbleImage(string url)
        {
            return $@"new google.maps.MarkerImage( 
                        {url},
                        null, 
                        new google.maps.Point(0, 0), 
                        new google.maps.Point(13, 15),
                        new google.maps.Size(25, 25)
                    )";
        }

        private string newMarkerImage(string url)
        {
            return $@"new google.maps.MarkerImage( 
                        {url},
                        null,
                        null, 
                        null,
                        new google.maps.Size(45, 45)
                    )";
        }

        private string newMarker(string latVar, string lonVar, string markerIcon, string animation = null)
        {
            var animationActive = (animation != null) ? ",animation:" + animation : "";
            return
                @"new google.maps.Marker({" +
                    $@"position: new google.maps.LatLng({latVar}, {lonVar}), 
                    map: map,
                    icon: {markerIcon}" +
                    animationActive +
                "})";
        }

        private string createStringUrl(string url)
        {
            return $@"'{url}'";
        }

        private string ifElse(string condition, string True, string False)
        {
            return $@"({condition})? {True} : {False}";
        }


        private string asingVar(string var, string instance)
        {
            return $@"{var} = {instance};";
        }

        private string createVar(string var, string instance)
        {
            return $@"var " + this.asingVar(var, instance);
        }


        private string generateClusterStyles(string cluster)
        {
            return @"
            var clusterStyles = [
                {
                    textColor: 'white',
                    url: 'https://upload.wikimedia.org/wikipedia/commons/e/e6/Lol_circle.png',
                    height: 50,
                    width: 50
                },
                {
                    textColor: 'white',
                    url: 'http://www.clipartx.com/uploads/pink/pink-circle-155099',
                    height: 50,
                    width: 50
                },
                {
                    textColor: 'white',
                    url: 'http://icons.iconarchive.com/icons/danieledesantis/playstation-flat/512/playstation-circle-dark-icon.png',
                    height: 50,
                    width: 50
                }
            ];
            var " + cluster + @" = {
                gridSize: 50,
                styles: clusterStyles,
                maxZoom: 15
            };
            ";
        }

        private string generateMarkers(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
            string markers = string.Empty;
            foreach (var organizacion in organizaciones)
            {
                var defaultIcon = "https://cdn2.iconfinder.com/data/icons/connectivity/32/navigation-128.png";

                var marker = "{" +
                                $@"
                                name: '{organizacion.Nombre.EscapeQuoatationMarks()}',
                                contacto: '{organizacion.ContactoCargo.EscapeQuoatationMarks()}',
                                email: '{organizacion.Email.EscapeQuoatationMarks()}',
                                tel: '{organizacion.Telefono.EscapeQuoatationMarks()}',
                                lat : '{organizacion.Latitud}',
                                lng : '{organizacion.Longitud}',
                                direc :'{organizacion.Direccion.EscapeQuoatationMarks()}',
                                icon : '{(string.IsNullOrEmpty(organizacion.Icono) ? defaultIcon : organizacion.Icono)}',
                                isInti : '{organizacion.UsuarioInti.ToString().ToLower()}'"
                             + "}";

                if (markers.Length == 0)
                {
                    markers = markers + marker;
                }
                else
                {
                    markers = $"{markers}, {marker}";
                }
            }
            return markers;
        }

        private string includeScriptUrl(string url)
        {
            return "<script src=\" " + url + " \"></script>";
        }


        public string GetHtmlString(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
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
                                     this.createVar("locations", "[" + this.generateMarkers(organizaciones) + "]") +
                                     this.createVar("mapDiv", "document.getElementById('map')") +
                                     this.createVar("map", "new google.maps.Map(mapDiv, {zoom: 13 })") +
                                     this.createVar("infowindow", "new google.maps.InfoWindow()") +
                                     this.createVar("bounds", "new google.maps.LatLngBounds()") +
                                     this.createVar("markers", "[]") +
                                     "for (i = 0; i < locations.length; i++){" +

                                            //creador de circulos   
                                            this.createVar("circleIcon",
                                                this.newMarkerBubbleImage(
                                                    this.ifElse("locations[i].isInti",
                                                        this.createStringUrl(INTI_BUBBLE),
                                                        this.createStringUrl(NO_INTI_BUBBLE)
                                                    )
                                                )
                                            ) +
                                            //asigna el circulo a un marcador para indentificar si es inti o no
                                            this.asingVar("marker",
                                                this.newMarker(
                                                    "locations[i].lat",
                                                    "locations[i].lng",
                                                    "circleIcon"
                                                )
                                            ) +
                                            //crea el icono de la organizacion
                                            this.createVar("pinIcon",
                                                this.newMarkerImage("locations[i].icon")
                                            ) +
                                            //crea el marcador de la organizacion y lo agrega al mapa
                                            this.asingVar("marker",
                                                this.newMarker(
                                                    "locations[i].lat",
                                                    "locations[i].lng",
                                                    "pinIcon",
                                                    "google.maps.Animation.DROP")
                                            ) +
                                            "bounds.extend(new google.maps.LatLng(locations[i].lat, locations[i].lng));" +
                                           //"window.setTimeout(function(){marker} , i * 200);"+
                                           "markers.push(marker);" +

                                           "google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
                                           "return function() {" +
                                           "infowindow.setContent(getInfoWindows(locations[i]));" +
                                           "infowindow.open(map, marker);}" +
                                          "})(marker, i));" +
                                      "}" +
                                      this.generateClusterStyles(this.markerClusterOptionsVar) +
                                      this.createVar("markerCluster",
                                                    "new MarkerClusterer(map, markers, " + this.markerClusterOptionsVar + ")") +
                                      "map.fitBounds(bounds);" +
                                  "}" +
                              "</script>" +
                              this.includeScriptUrl(G_MAP_API_CLUSTER) +
                              this.includeScriptUrl(G_MAP_API) +
                              "</body>" +
                              "</html>";
            return line;
        }
    }
}

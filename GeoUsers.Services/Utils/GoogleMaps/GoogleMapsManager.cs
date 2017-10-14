using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace GeoUsersUI.GoogleMaps
{
    public class GoogleMapsManager
    {

        private string GenerateMarkers(IEnumerable<OrganizacionHeaderData> organizaciones)
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
                                isInti : {organizacion.UsuarioInti.ToString().ToLower()}"
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

        public string CreateMarkersFunction(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
            return $@"setIntiUsersToMap([{GenerateMarkers(organizaciones)}])";
        }

        public string CenterMapInFunction(string lat, string lon)
        {
            return $"setCenter({lat},{lon})";
        }

        public string GetWebManagerPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "WebManager/index.html");
        }
    }
}

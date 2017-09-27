using System.ComponentModel;

namespace GeoUsers.Services.Model.Enums
{
    public enum UsuarioIntiStatus
    {
        [Description("Usuario Inti")]
        UsuarioInti = 0,

        [Description("No Usuario Inti")]
        NoUsuarioInti = 1,

        [Description("Todos")]
        Todos = 2
    }
}

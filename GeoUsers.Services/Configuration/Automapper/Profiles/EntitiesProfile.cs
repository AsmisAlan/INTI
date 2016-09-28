using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;

namespace GeoUsers.Services.Automapper.Profiles
{
    public class EntitiesProfile : Profile
    {
        public EntitiesProfile()
        {
            CreateMap<Sector, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Nombre));
            CreateMap<Organizacion, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Tipo));
            CreateMap<Rubro, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Nombre));
            CreateMap<Localidad, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => $"{x.Nombre} ({x.CodigoPostal})"));

            CreateMap<Usuario, UsuarioHeader>()
                .ForMember(x => x.Direccion, opt => opt.MapFrom(x => $"{x.Direccion} {x.Localidad.Nombre}"));
        }
    }
}

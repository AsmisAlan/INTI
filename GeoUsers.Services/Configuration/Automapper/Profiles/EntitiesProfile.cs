using AutoMapper;
using GeoUsers.Services.Model;
using GeoUsers.Services.Model.Entities;

namespace GeoUsers.Services.Automapper.Profiles
{
    public class EntitiesProfile : Profile
    {
        public EntitiesProfile()
        {
            CreateMap<Sector, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Nombre));
            CreateMap<Localidad, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => $"{x.Nombre} ({x.CodigoPostal})"));
        }
    }
}

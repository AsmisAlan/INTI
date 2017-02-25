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

            CreateMap<Sector, SectorHeaderData>();

            CreateMap<Sector, SectorEditionData>();

            CreateMap<TipoOrganizacion, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Tipo));

            CreateMap<TipoOrganizacion, TipoOrganizacionEditionData>();

            CreateMap<TipoOrganizacion, TipoOrganizacionHeaderData>();

            CreateMap<Rubro, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Nombre));

            CreateMap<Rubro, RubroHeaderData>()
                .ForMember(x => x.Sector, opt => opt.MapFrom(x => x.Sector.Nombre));

            CreateMap<Rubro, RubroEditionData>()
                .ForMember(x => x.SectorId, opt => opt.MapFrom(x => x.Sector.Id));

            CreateMap<Localidad, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => $"{x.Nombre} ({x.CodigoPostal})"));

            CreateMap<Localidad, LocalidadEditionData>();

            CreateMap<Localidad, LocalidadHeaderData>();

            CreateMap<Organizacion, OrganizacionHeaderData>()
                .ForMember(x => x.Direccion, opt => opt.MapFrom(x => $"{x.Direccion} {x.Localidad.Nombre}"));

            CreateMap<Organizacion, OrganizacionData>();

            CreateMap<Organizacion, OrganizacionEditionData>()
                .ForMember(x => x.Direccion, opt => opt.MapFrom(x => x.Direccion))
                .ForMember(x => x.LocalidadId, opt => opt.MapFrom(x => x.Localidad.Id))
                .ForMember(x => x.RubroId, opt => opt.MapFrom(x => x.Rubro.Id))
                .ForMember(x => x.TipoOrganizacionId, opt => opt.MapFrom(x => x.TipoOrganizacion.Id));
        }
    }
}

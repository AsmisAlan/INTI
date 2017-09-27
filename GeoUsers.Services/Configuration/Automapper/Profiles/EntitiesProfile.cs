using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using GeoUsers.Services.Properties;
using GeoUsers.Services.Utils;
using System.IO;

namespace GeoUsers.Services.Automapper.Profiles
{
    public class EntitiesProfile : Profile
    {
        public EntitiesProfile()
        {
            CreateMap<Sector, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Nombre));

            CreateMap<Sector, SectorHeaderData>()
                .ForMember(x => x.IsActive, opt => opt.UseValue(true));

            CreateMap<Sector, SectorEditionData>();

            CreateMap<TipoOrganizacion, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Tipo));

            CreateMap<TipoOrganizacion, TipoOrganizacionEditionData>();

            CreateMap<TipoOrganizacion, TipoOrganizacionHeaderData>()
                .ForMember(x => x.IsActive, opt => opt.UseValue(true));

            CreateMap<Rubro, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Nombre));

            CreateMap<Rubro, RubroHeaderData>()
                .ForMember(x => x.IsActive, opt => opt.UseValue(true))
                .ForMember(x => x.Sector, opt => opt.MapFrom(x => x.Sector.Nombre));

            CreateMap<Rubro, RubroEditionData>()
                .ForMember(x => x.SectorId, opt => opt.MapFrom(x => x.Sector.Id));

            CreateMap<Localidad, IdAndValue>()
                .ForMember(x => x.Value, opt => opt.MapFrom(x => $"{x.Nombre} ({x.CodigoPostal})"));

            CreateMap<Localidad, LocalidadEditionData>();

            CreateMap<Localidad, LocalidadHeaderData>()
                .ForMember(x => x.IsActive, opt => opt.UseValue(true));

            CreateMap<Organizacion, OrganizacionHeaderData>()
                .ForMember(x => x.IsActive, opt => opt.UseValue(true))
                .ForMember(x => x.Icono, opt => opt.MapFrom(x => GetIcono(x)))
                .ForMember(x => x.Direccion, opt => opt.MapFrom(x => $"{x.Direccion} {x.Localidad.Nombre}"));

            CreateMap<Organizacion, OrganizacionData>()
                .IncludeBase<Organizacion, OrganizacionBaseData>()
                .ForMember(x => x.IsActive, opt => opt.UseValue(true));

            CreateMap<Organizacion, OrganizacionEditionData>()
                .ForMember(x => x.Direccion, opt => opt.MapFrom(x => x.Direccion))
                .ForMember(x => x.LocalidadId, opt => opt.MapFrom(x => x.Localidad.Id))
                .ForMember(x => x.RubroId, opt => opt.MapFrom(x => x.Rubro.Id))
                .ForMember(x => x.TipoOrganizacionId, opt => opt.MapFrom(x => x.TipoOrganizacion.Id));

            CreateMap<Archivo, ArchivoEditionData>()
                .ForMember(x => x.Ruta, opt => opt.MapFrom(x => $"{Path.Combine(Settings.Default.AttachmentFolder, x.Ruta)}"));
        }

        private static string GetIcono(Organizacion organizacion)
        {
            if (organizacion.Rubro.Sector.Icono == null) return null;

            return WebUtils.GetFileDataUri(organizacion.Rubro.Sector.Icono);
        }
    }
}

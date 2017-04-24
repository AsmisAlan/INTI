using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using GeoUsers.Services.Model.Enums;
using GeoUsers.Services.SQLExceptions;
using GeoUsers.Services.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsers.Services.Logics
{
    public class OrganizacionLogic : BaseLogic
    {
        readonly RubroLogic rubroLogic;
        readonly TipoOrganizacionLogic tipoOrganizacionLogic;
        readonly LocalidadLogic localidadLogic;

        public OrganizacionLogic(RubroLogic rubroLogic,
                                 LocalidadLogic localidadLogic,
                                 TipoOrganizacionLogic organizacionLogic,
                                 IMapper mapper,
                                 ISessionFactory sessionFactory) : base(mapper, sessionFactory)
        {
            this.rubroLogic = rubroLogic;
            this.localidadLogic = localidadLogic;
            this.tipoOrganizacionLogic = organizacionLogic;
        }

        public OrganizacionEditionData GetForEdition(int organizacionId)
        {
            var organizacion = Session.Get<Organizacion>(organizacionId);

            var data = Mapper.Map<OrganizacionEditionData>(organizacion);

            return data;
        }

        public IEnumerable<OrganizacionData> GetAll()
        {
            var organizaciones = Session.QueryOver<Organizacion>()
                                        .OrderBy(x => x.Nombre)
                                        .Asc
                                        .List();

            return Mapper.Map<IEnumerable<OrganizacionData>>(organizaciones);
        }

        public IEnumerable<IdAndValue> GetByIds(ICollection<int> organizacionIds)
        {
            var organizaciones = Session.QueryOver<Organizacion>()
                                        .WhereRestrictionOn(x => x.Id)
                                        .IsIn(organizacionIds.ToArray())
                                        .List()
                                        .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(organizaciones);
        }

        public IEnumerable<IdAndValue> GetForSelection(ICollection<int> organizacionIds = null)
        {
            var organizacionesQuery = Session.QueryOver<Organizacion>();

            if (organizacionIds != null && organizacionIds.Count > 0)
            {
                organizacionesQuery.WhereRestrictionOn(x => x.Id)
                                   .Not.IsIn(organizacionIds.ToArray());
            }

            var organizaciones = organizacionesQuery.List()
                                                    .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(organizaciones);
        }

        public IEnumerable<OrganizacionHeaderData> GetHeadersByFilter(FilterData filter)
        {
            var organizaciones = GetByFilter(filter);

            return Mapper.Map<IEnumerable<OrganizacionHeaderData>>(organizaciones);
        }

        public IEnumerable<OrganizacionData> GetOrganizacionesByFilter(FilterData filter)
        {
            var organizaciones = GetByFilter(filter);

            return Mapper.Map<IEnumerable<OrganizacionData>>(organizaciones);
        }

        public int Create(OrganizacionEditionData creationData)
        {
            return Edit(creationData);
        }

        public int Edit(OrganizacionEditionData organizacionData)
        {
            Organizacion organizacion;

            if (organizacionData.Id.HasValue)
            {
                organizacion = Session.Get<Organizacion>(organizacionData.Id);

                if (organizacion == null) throw new Exception("Organizacion Invalido");
            }
            else
            {
                organizacion = new Organizacion();
            }

            var rubro = rubroLogic.GetRubro(organizacionData.RubroId.Value);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var tipoOrganizacion = tipoOrganizacionLogic.Get(organizacionData.TipoOrganizacionId.Value);

            if (tipoOrganizacion == null) throw new Exception("Tipo de Organizacion Invalida");

            var localidad = localidadLogic.Get(organizacionData.LocalidadId.Value);

            if (localidad == null) throw new Exception("Localidad Invalida");

            if (!organizacionData.AutoDetectCoordinates &&
                string.IsNullOrEmpty(organizacionData.Latitud) &&
                string.IsNullOrEmpty(organizacionData.Longitud))
            {
                throw new Exception("Debe especificar latitud y longitud.");
            }

            organizacion.ContactoCargo = organizacionData.ContactoCargo;
            organizacion.Cuit = organizacionData.Cuit;
            organizacion.Direccion = organizacionData.Direccion;
            organizacion.Email = organizacionData.Email;
            organizacion.Localidad = localidad;
            organizacion.Nombre = organizacionData.Nombre;
            organizacion.TipoOrganizacion = tipoOrganizacion;
            organizacion.Personal = organizacionData.Personal;
            organizacion.Rubro = rubro;
            organizacion.Telefono = organizacionData.Telefono;
            organizacion.UsuarioInti = organizacionData.UsuarioInti;
            organizacion.Web = organizacionData.Web;

            if (organizacionData.AutoDetectCoordinates)
            {
                var address = $"{organizacion.Direccion} {organizacion.Localidad.Nombre}";

                var coordinates = WebUtils.GetCoordinates(address);

                organizacion.Latitud = coordinates.lat;
                organizacion.Longitud = coordinates.lng;
            }
            else
            {

                if (string.IsNullOrEmpty(organizacionData.Latitud))
                {
                    throw new Exception("El campo latitud no puede estar vacío.");
                }

                if (string.IsNullOrEmpty(organizacionData.Longitud))
                {
                    throw new Exception("El campo longitud no puede estar vacío.");
                }

                organizacion.Latitud = organizacionData.Latitud;
                organizacion.Longitud = organizacionData.Longitud;
            }

            try
            {
                Session.Save(organizacion);

                Session.Transaction.Commit();
            }
            catch (UniqueConstraintViolationException)
            {
                throw new Exception($"El cuit {organizacion.Cuit} ya fue registrado en otra organización");
            }

            return organizacion.Id;
        }

        public bool Delete(int organizacionId)
        {
            var organizacion = Session.Get<Organizacion>(organizacionId);

            if (organizacion == null) throw new Exception("Organizacion Invalido");

            Session.Delete(organizacion);

            return true;
        }

        private IEnumerable<Organizacion> GetByFilter(FilterData filter)
        {
            var organizacionesQuery = Session.QueryOver<Organizacion>();

            if (((UsuarioIntiStatus)filter.UsuarioInti) == UsuarioIntiStatus.NoUsuarioInti)
            {
                organizacionesQuery.WhereNot(x => x.UsuarioInti);
            }
            else if (((UsuarioIntiStatus)filter.UsuarioInti) == UsuarioIntiStatus.UsuarioInti)
            {
                organizacionesQuery.Where(x => x.UsuarioInti);
            }

            if (filter.LocalidadIds != null && filter.LocalidadIds.Count > 0)
            {
                organizacionesQuery.WhereRestrictionOn(x => x.Localidad.Id)
                                   .IsIn(filter.LocalidadIds.ToArray());
            }

            if (filter.SectorIds != null && filter.SectorIds.Count > 0)
            {
                Rubro rubroAlias = null;

                organizacionesQuery.JoinAlias(x => x.Rubro, () => rubroAlias)
                                   .WhereRestrictionOn(() => rubroAlias.Sector.Id)
                                   .IsIn(filter.SectorIds.ToArray());
            }

            if (filter.RubroIds != null && filter.RubroIds.Count > 0)
            {
                organizacionesQuery.WhereRestrictionOn(x => x.Rubro.Id)
                                   .IsIn(filter.RubroIds.ToArray());
            }

            if (filter.TipoOrganizacionIds != null && filter.TipoOrganizacionIds.Count > 0)
            {
                organizacionesQuery.WhereRestrictionOn(x => x.TipoOrganizacion.Id)
                                   .IsIn(filter.TipoOrganizacionIds.ToArray());
            }

            var organizaciones = organizacionesQuery.List()
                                                    .OrderBy(x => x.Nombre);

            return organizaciones;
        }
    }
}

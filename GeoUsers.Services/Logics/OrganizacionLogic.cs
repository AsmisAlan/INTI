using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
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

        public Organizacion GetOrganizacion(int organizacionId)
        {
            return Session.Get<Organizacion>(organizacionId);
        }

        public IEnumerable<Organizacion> GetAll()
        {
            return Session.QueryOver<Organizacion>()
                          .List()
                          .OrderBy(x => x.Nombre);
        }

        public IEnumerable<IdAndValue> GetForSelection(/*ICollection<int> currentIds*/)
        {
            var organizaciones = Session.QueryOver<Organizacion>()
                                        //.WhereRestrictionOn(x => x.Id)
                                        //.Not.IsIn(currentIds.ToArray())
                                        .List()
                                        .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(organizaciones);
        }

        public IEnumerable<OrganizacionHeader> GetOrganizacionHeaders(FilterData filter)
        {
            var organizacionesQuery = Session.QueryOver<Organizacion>();

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

            return Mapper.Map<IEnumerable<OrganizacionHeader>>(organizaciones);
        }

        public bool Create(OrganizacionCreationData creationData)
        {
            var rubro = rubroLogic.GetRubro(creationData.RubroId.Value);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var tipoOrganizacion = tipoOrganizacionLogic.GetOrganizacion(creationData.TipoOrganizacionId.Value);

            if (tipoOrganizacion == null) throw new Exception("Organizacion Invalida");

            var localidad = localidadLogic.GetLocalidad(creationData.LocalidadId.Value);

            if (localidad == null) throw new Exception("Localidad Invalida");

            var organizacion = new Organizacion()
            {
                Nombre = creationData.Nombre,
                ContactoCargo = creationData.ContactoCargo,
                Cuit = creationData.Cuit,
                Direccion = creationData.Direccion,
                Email = creationData.Email,
                Personal = creationData.Personal.Value,
                Telefono = creationData.Telefono,
                Web = creationData.Web,
                UsuarioInti = creationData.UsuarioInti,
                Localidad = localidad,
                TipoOrganizacion = tipoOrganizacion,
                Rubro = rubro
            };

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(int organizacionId,
                         string nombre,
                         string direccion,
                         string telefono,
                         string email,
                         string web,
                         string contactoCargo,
                         bool organizacionGeoUsersServices,
                         int personal,
                         long? cuit,
                         int rubroId,
                         int tipoOrganizacionId,
                         int localidadId)
        {
            var organizacion = Session.Get<Organizacion>(organizacionId);

            if (organizacion == null) throw new Exception("Organizacion Invalido");

            var rubro = rubroLogic.GetRubro(rubroId);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var tipoOrganizacion = tipoOrganizacionLogic.GetOrganizacion(tipoOrganizacionId);

            if (tipoOrganizacion == null) throw new Exception("Tipo de Organizacion Invalida");

            var localidad = localidadLogic.GetLocalidad(localidadId);

            if (localidad == null) throw new Exception("Localidad Invalida");

            organizacion.ContactoCargo = contactoCargo;
            organizacion.Cuit = cuit;
            organizacion.Direccion = direccion;
            organizacion.Email = email;
            organizacion.Localidad = localidad;
            organizacion.Nombre = nombre;
            organizacion.TipoOrganizacion = tipoOrganizacion;
            organizacion.Personal = personal;
            organizacion.Rubro = rubro;
            organizacion.Telefono = telefono;
            organizacion.UsuarioInti = organizacionGeoUsersServices;
            organizacion.Web = web;

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int organizacionId)
        {
            var organizacion = Session.Get<TipoOrganizacion>(organizacionId);

            if (organizacion == null) throw new Exception("Organizacion Invalido");

            Session.Delete(organizacion);

            return true;
        }
    }
}

using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsers.Services.Logics
{
    public class TipoOrganizacionLogic : BaseLogic
    {
        public TipoOrganizacionLogic(IMapper autoMapper,
                                     ISessionFactory sessionFactory) : base(autoMapper, sessionFactory)
        { }

        public TipoOrganizacion Get(int tipoOrganizacionId)
        {
            return Session.Get<TipoOrganizacion>(tipoOrganizacionId);
        }

        public IEnumerable<TipoOrganizacionHeaderData> GetAll()
        {
            var tipoOrganizaciones = Session.QueryOver<TipoOrganizacion>()
                                            .OrderBy(x => x.Tipo)
                                            .Asc
                                            .List();

            return Mapper.Map<IEnumerable<TipoOrganizacionHeaderData>>(tipoOrganizaciones);
        }

        public TipoOrganizacionEditionData GetForEdition(int tipoOrganizacionId)
        {
            var tipoOrganizacion = Get(tipoOrganizacionId);

            return Mapper.Map<TipoOrganizacionEditionData>(tipoOrganizacion);
        }

        public IEnumerable<IdAndValue> GetByIds(ICollection<int> tipoOrganizacionIds)
        {
            var tipoOrganizacion = Session.QueryOver<TipoOrganizacion>()
                                          .WhereRestrictionOn(x => x.Id)
                                          .IsIn(tipoOrganizacionIds.ToArray())
                                          .OrderBy(x => x.Tipo).Asc
                                          .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(tipoOrganizacion);
        }

        public IEnumerable<IdAndValue> GetForSelection(ICollection<int> currentIds = null)
        {
            var tipoOrganizacionQuery = Session.QueryOver<TipoOrganizacion>();

            if (currentIds != null && currentIds.Count > 0)
            {
                tipoOrganizacionQuery.WhereRestrictionOn(x => x.Id)
                                     .Not
                                     .IsIn(currentIds.ToArray());
            }

            var tipoOrganizaciones = tipoOrganizacionQuery.OrderBy(x => x.Tipo)
                                                          .Asc
                                                          .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(tipoOrganizaciones);
        }

        public bool Save(TipoOrganizacionEditionData organizacionData)
        {
            return organizacionData.Id.HasValue ? Edit(organizacionData) : Create(organizacionData);
        }

        public bool Create(TipoOrganizacionEditionData organizacionData)
        {
            var organizacion = new TipoOrganizacion()
            {
                Tipo = organizacionData.Tipo
            };

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(TipoOrganizacionEditionData tipoOrganizacionData)
        {
            var organizacion = Session.Get<TipoOrganizacion>(tipoOrganizacionData.Id);

            if (organizacion == null) throw new Exception("Tipo de organizacion Invalida");

            organizacion.Tipo = tipoOrganizacionData.Tipo;

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int tipoOrganizacionId)
        {
            var organizacion = Session.Get<TipoOrganizacion>(tipoOrganizacionId);

            if (organizacion == null) throw new Exception("Tipo de organizacion invalida");

            Session.Delete(organizacion);

            return true;
        }
    }
}

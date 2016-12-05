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

        public TipoOrganizacion GetOrganizacion(int organizacionId)
        {
            return Session.Get<TipoOrganizacion>(organizacionId);
        }

        public IEnumerable<TipoOrganizacion> GetAll()
        {
            return Session.QueryOver<TipoOrganizacion>()
                          .List()
                          .OrderBy(x => x.Tipo);
        }

        public IEnumerable<IdAndValue> GetForSelection(/*ICollection<int> currentIds*/)
        {
            var organizaciones = Session.QueryOver<TipoOrganizacion>()
                          //.WhereRestrictionOn(x => x.Id)
                          //.Not.IsIn(currentIds.ToArray())
                          .List()
                          .OrderBy(x => x.Tipo);

            return Mapper.Map<IEnumerable<IdAndValue>>(organizaciones);
        }

        public bool Create(TipoOrganizacionCreationData organizacionData)
        {
            var organizacion = new TipoOrganizacion()
            {
                Tipo = organizacionData.Tipo
            };

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(int organizacionId, string tipo)
        {
            var organizacion = Session.Get<TipoOrganizacion>(organizacionId);

            organizacion.Tipo = tipo;

            if (organizacion == null) throw new Exception("Organizacion Invalida");

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int organizacionId)
        {
            var organizacion = Session.Get<Sector>(organizacionId);

            if (organizacion == null) throw new Exception("Organizacion Invalida");

            Session.Delete(organizacion);

            return true;
        }
    }
}

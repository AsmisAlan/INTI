using AutoMapper;
using GeoUsers.Services.Model;
using GeoUsers.Services.Model.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsers.Services.Logics
{
    public class OrganizacionLogic : BaseLogic
    {
        public OrganizacionLogic(IMapper autoMapper,
                                 ISessionFactory sessionFactory) : base(autoMapper, sessionFactory)
        { }

        public Organizacion GetOrganizacion(int organizacionId)
        {
            return Session.Get<Organizacion>(organizacionId);
        }

        public IEnumerable<Organizacion> GetAll()
        {
            return Session.QueryOver<Organizacion>()
                          .List()
                          .OrderBy(x => x.Tipo);
        }

        public IEnumerable<IdAndValue> GetForSelection(ICollection<int> currentIds)
        {
            var organizaciones = Session.QueryOver<Organizacion>()
                          .WhereRestrictionOn(x => x.Id)
                          .Not.IsIn(currentIds.ToArray())
                          .List()
                          .OrderBy(x => x.Tipo);

            return Mapper.Map<IEnumerable<IdAndValue>>(organizaciones);
        }

        public bool Create(string nombre)
        {
            var organizacion = new Organizacion()
            {
                Tipo = nombre
            };

            Session.Save(organizacion);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(int organizacionId, string tipo)
        {
            var organizacion = Session.Get<Organizacion>(organizacionId);

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

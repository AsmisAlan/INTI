using AutoMapper;
using GeoUsers.Services.Model;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsers.Services.Logics
{
    public class LocalidadLogic : BaseLogic
    {
        public LocalidadLogic(IMapper autoMapper,
                              ISessionFactory sessionFactory) : base(autoMapper, sessionFactory)
        { }

        public Localidad GetLocalidad(int localidadId)
        {
            return Session.Get<Localidad>(localidadId);
        }

        public IEnumerable<Localidad> GetAll()
        {
            return Session.QueryOver<Localidad>()
                          .List()
                          .OrderBy(x => x.Nombre);
        }

        public IEnumerable<IdAndValue> GetForSelection()
        {
            var localidades = Session.QueryOver<Localidad>()
                                     .List()
                                     .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(localidades);
        }

        public bool Create(LocalidadCreationData localidadData)
        {
            var localidad = new Localidad()
            {
                Nombre = localidadData.Nombre,
                CodigoPostal = localidadData.CodigoPostal.Value
            };

            Session.Save(localidad);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(int localidadId, string nombre, int codigoPostal)
        {
            var localidad = Session.Get<Localidad>(localidadId);

            if (localidad == null) throw new Exception("Localidad Invalida");

            localidad.Nombre = nombre;
            localidad.CodigoPostal = codigoPostal;

            Session.Save(localidad);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int localidadId)
        {
            var localidad = Session.Get<Localidad>(localidadId);

            if (localidad == null) throw new Exception("Localidad Invalida");

            Session.Delete(localidad);

            return true;
        }
    }
}

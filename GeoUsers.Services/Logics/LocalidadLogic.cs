using AutoMapper;
using GeoUsers.Services.Model;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using GeoUsers.Services.SQLExceptions;
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

        public Localidad Get(int localidadId)
        {
            return Session.Get<Localidad>(localidadId);
        }

        public LocalidadEditionData GetForEdition(int localidadId)
        {
            var localidad = Get(localidadId);

            return Mapper.Map<LocalidadEditionData>(localidad);
        }

        public IEnumerable<LocalidadHeaderData> GetAll()
        {
            var localidades = Session.QueryOver<Localidad>()
                                     .List()
                                     .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<LocalidadHeaderData>>(localidades);
        }

        public IEnumerable<IdAndValue> GetByIds(ICollection<int> localidadIds)
        {
            var localidades = Session.QueryOver<Localidad>()
                                .WhereRestrictionOn(x => x.Id)
                                .IsIn(localidadIds.ToArray())
                                .List()
                                .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(localidades);
        }

        public IEnumerable<IdAndValue> GetForSelection()
        {
            var localidades = Session.QueryOver<Localidad>()
                                     .OrderBy(x => x.Nombre)
                                     .Asc
                                     .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(localidades);
        }

        public bool Save(LocalidadEditionData localidadData)
        {
            return localidadData.Id.HasValue ? Edit(localidadData) : Create(localidadData);
        }

        public bool Create(LocalidadEditionData localidadData)
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

        public bool Edit(LocalidadEditionData localidadData)
        {
            var localidad = Session.Get<Localidad>(localidadData.Id);

            if (localidad == null) throw new Exception("Localidad Invalida");

            localidad.Nombre = localidadData.Nombre;
            localidad.CodigoPostal = localidadData.CodigoPostal.Value;

            Session.Save(localidad);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int localidadId)
        {
            var localidad = Session.Get<Localidad>(localidadId);

            if (localidad == null) throw new Exception("Localidad Invalida");

            try
            {
                Session.Delete(localidad);
            }
            catch (ConstraintViolationException)
            {
                throw new Exception("La localidad que se desesa eliminar está siendo utilizada.");
            }

            return true;
        }
    }
}

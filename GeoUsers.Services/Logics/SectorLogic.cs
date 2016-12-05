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
    public class SectorLogic : BaseLogic
    {
        public SectorLogic(IMapper autoMapper,
                           ISessionFactory sessionFactory) : base(autoMapper, sessionFactory)
        { }

        public Sector GetSector(int sectorId)
        {
            return Session.Get<Sector>(sectorId);
        }

        public IEnumerable<Sector> GetAll()
        {
            return Session.QueryOver<Sector>()
                          .List()
                          .OrderBy(x => x.Nombre);
        }

        public IEnumerable<IdAndValue> GetByIds(IEnumerable<int> sectorIds)
        {
            var sectores = Session.QueryOver<Sector>()
                                  .WhereRestrictionOn(x => x.Id)
                                  .IsIn(sectorIds.ToArray())
                                  .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(sectores);
        }

        public IEnumerable<IdAndValue> GetForSelection(ICollection<int> currentIds = null)
        {
            var sectoresQuery = Session.QueryOver<Sector>();

            if (currentIds != null && currentIds.Count > 0)
            {
                sectoresQuery.WhereRestrictionOn(x => x.Id)
                             .Not.IsIn(currentIds.ToArray());
            }

            var sectores = sectoresQuery.List()
                                        .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(sectores);
        }

        public bool Create(SectorCreationData sectorData)
        {
            var sector = new Sector()
            {
                Nombre = sectorData.Nombre
            };

            Session.Save(sector);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(int sectorId, string nombre)
        {
            var sector = Session.Get<Sector>(sectorId);

            if (sector == null) throw new Exception("Sector Invalido");

            sector.Nombre = nombre;

            Session.Save(sector);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int sectorId)
        {
            var sector = Session.Get<Sector>(sectorId);

            if (sector == null) throw new Exception("Sector Invalido");

            Session.Delete(sector);

            return true;
        }
    }
}

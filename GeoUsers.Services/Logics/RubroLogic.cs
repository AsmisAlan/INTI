using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsers.Services.Logics
{
    public class RubroLogic : BaseLogic
    {
        public RubroLogic(IMapper mapper,
                          ISessionFactory sessionFactory) : base(mapper, sessionFactory)
        { }

        public Rubro GetRubro(int rubroId)
        {
            return Session.Get<Rubro>(rubroId);
        }

        public RubroEditionData GetForEdition(int rubroId)
        {
            return Mapper.Map<RubroEditionData>(GetRubro(rubroId));
        }

        public IEnumerable<RubroHeaderData> GetAll()
        {
            var rubros = Session.QueryOver<Rubro>()
                                .OrderBy(x => x.Nombre)
                                .Asc
                                .List();

            return Mapper.Map<IEnumerable<RubroHeaderData>>(rubros);
        }

        public IEnumerable<IdAndValue> GetByIds(ICollection<int> rubroIds)
        {
            var rubros = Session.QueryOver<Rubro>()
                                .WhereRestrictionOn(x => x.Id)
                                .IsIn(rubroIds.ToArray())
                                .OrderBy(x => x.Nombre)
                                .Asc
                                .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(rubros);
        }

        public IEnumerable<IdAndValue> GetForSelection(ICollection<int> currentIds = null)
        {
            var rubrosQuery = Session.QueryOver<Rubro>();

            if (currentIds != null)
            {
                rubrosQuery.WhereRestrictionOn(x => x.Id)
                      .Not.IsIn(currentIds.ToArray());
            }

            var rubros = rubrosQuery.OrderBy(x => x.Nombre)
                                    .Asc
                                    .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(rubros);
        }

        public IEnumerable<IdAndValue> GetForSectores(ICollection<int> sectorIds)
        {
            var rubros = Session.QueryOver<Rubro>()
                          .WhereRestrictionOn(x => x.Sector.Id)
                          .IsIn(sectorIds.ToArray())
                          .OrderBy(x => x.Nombre)
                          .Asc
                          .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(rubros);
        }

        public bool Save(RubroEditionData rubroData)
        {
            if (!rubroData.Id.HasValue)
            {
                return Create(rubroData);
            }

            return Edit(rubroData);
        }

        public bool Create(RubroEditionData rubroData)
        {
            var sector = Session.Get<Sector>(rubroData.SectorId);

            if (sector == null) throw new Exception("Sector invalido");

            var rubro = new Rubro()
            {
                Nombre = rubroData.Nombre,
                Sector = sector
            };

            Session.Save(rubro);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(RubroEditionData rubroData)
        {
            var rubro = Session.Get<Rubro>(rubroData.Id);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var sector = Session.Get<Sector>(rubroData.SectorId);

            if (sector == null) throw new Exception("Sector Invalido");

            rubro.Nombre = rubroData.Nombre;
            rubro.Sector = sector;

            Session.Save(rubro);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int rubroId)
        {
            var rubro = Session.Get<Rubro>(rubroId);

            if (rubro == null) throw new Exception("Rubro Invalido");

            Session.Delete(rubro);

            return true;
        }
    }
}

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
    public class RubroLogic : BaseLogic
    {
        public RubroLogic(IMapper mapper,
                          ISessionFactory sessionFactory) : base(mapper, sessionFactory)
        { }

        public Rubro GetRubro(int rubroId)
        {
            return Session.Get<Rubro>(rubroId);
        }

        public IEnumerable<Rubro> GetAll()
        {
            return Session.QueryOver<Rubro>()
                          .List()
                          .OrderBy(x => x.Nombre);
        }

        public IEnumerable<IdAndValue> GetForSelection(/*ICollection<int> currentIds*/)
        {
            var rubros = Session.QueryOver<Rubro>()
                          //.WhereRestrictionOn(x => x.Id)
                          //.Not.IsIn(currentIds.ToArray())
                          .List()
                          .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(rubros);
        }

        public IEnumerable<IdAndValue> GetForSectores(ICollection<int> sectorIds)
        {
            var rubros = Session.QueryOver<Rubro>()
                          .WhereRestrictionOn(x => x.Sector.Id)
                          .IsIn(sectorIds.ToArray())
                          .List()
                          .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(rubros);
        }

        public bool Create(RubroCreationData rubroData)
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

        public bool Edit(int rubroId, string nombre, int sectorId)
        {
            var rubro = Session.Get<Rubro>(rubroId);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var sector = Session.Get<Sector>(sectorId);

            if (sector == null) throw new Exception("Sector Invalido");

            rubro.Nombre = nombre;
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

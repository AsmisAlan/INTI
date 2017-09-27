using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using GeoUsers.Services.SQLExceptions;
using GeoUsers.Services.Utils;
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
            var tipoOrganizaciones = GetTipoOrganizaciones();

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

        public IEnumerable<IdAndValue> GetForSelection()
        {
            var tipoOrganizaciones = Session.QueryOver<TipoOrganizacion>()
                                            .OrderBy(x => x.Tipo)
                                            .Asc
                                            .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(tipoOrganizaciones);
        }

        public void ExportToExcel(string filePath)
        {
            var tipoOrganizaciones = GetTipoOrganizaciones();

            ExcelUtils.ExportTipoOrganizacionesTable(tipoOrganizaciones, filePath);
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
            var tipoOrganizacion = Session.Get<TipoOrganizacion>(tipoOrganizacionId);

            if (tipoOrganizacion == null) throw new Exception("Tipo de organizacion invalida");

            try
            {
                Session.Delete(tipoOrganizacion);
            }
            catch (ConstraintViolationException)
            {
                throw new Exception("El tipo de organización que se desesa eliminar está siendo utilizado.");
            }

            return true;
        }

        private IEnumerable<TipoOrganizacion> GetTipoOrganizaciones()
        {
            var tipoOrganizaciones = Session.QueryOver<TipoOrganizacion>()
                                            .OrderBy(x => x.Tipo)
                                            .Asc
                                            .List();

            return tipoOrganizaciones;
        }
    }
}

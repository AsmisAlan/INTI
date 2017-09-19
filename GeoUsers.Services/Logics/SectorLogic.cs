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
    public class SectorLogic : BaseLogic
    {
        private readonly ArchivoLogic archivoLogic;

        public SectorLogic(IMapper autoMapper,
                           ISessionFactory sessionFactory,
                           ArchivoLogic archivoLogic) : base(autoMapper, sessionFactory)
        {
            this.archivoLogic = archivoLogic;
        }

        public SectorEditionData GetForEdition(int sectorId)
        {
            var sector = Session.QueryOver<Sector>()
                                .Where(x => x.Id == sectorId)
                                .Fetch(x => x.Icono)
                                .Eager
                                .List()
                                .First();

            return Mapper.Map<SectorEditionData>(sector);
        }

        public IEnumerable<SectorHeaderData> GetAll()
        {
            var sectores = GetSectores();

            return Mapper.Map<IEnumerable<SectorHeaderData>>(sectores);
        }

        public IEnumerable<IdAndValue> GetByIds(IEnumerable<int> sectorIds)
        {
            var sectores = Session.QueryOver<Sector>()
                                  .WhereRestrictionOn(x => x.Id)
                                  .IsIn(sectorIds.ToArray())
                                  .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(sectores);
        }

        public IEnumerable<IdAndValue> GetForSelection()
        {
            var sectores = Session.QueryOver<Sector>()
                                       .OrderBy(x => x.Nombre)
                                       .Asc
                                       .List();

            return Mapper.Map<IEnumerable<IdAndValue>>(sectores);
        }

        public void ExportToExcel(string filePath)
        {
            var sectores = GetSectores();

            ExcelUtils.ExportSectoresTable(sectores, filePath);
        }

        public bool Save(SectorEditionData sectorData)
        {
            if (sectorData.Id.HasValue)
            {
                return Edit(sectorData);
            }
            else
            {
                return Create(sectorData);
            }
        }

        public bool Create(SectorEditionData sectorData)
        {
            var sector = new Sector()
            {
                Nombre = sectorData.Nombre
            };

            if (sectorData.Icono != null)
            {
                sector.Icono = archivoLogic.AddArchivo(sectorData.Icono);
            }

            Session.Save(sector);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(SectorEditionData sectorData)
        {
            var sector = Session.Get<Sector>(sectorData.Id);

            if (sector == null) throw new Exception("Sector Invalido");

            sector.Nombre = sectorData.Nombre;

            if (sectorData.Icono == null)
            {
                archivoLogic.DeleteArchivo(sector.Icono.Id);

                sector.Icono = null;
            }
            else if (sectorData.Icono.Data != null)
            {
                if (sector.Icono == null)
                {
                    sector.Icono = archivoLogic.AddArchivo(sectorData.Icono);
                }
                else
                {
                    archivoLogic.EditArchivo(sectorData.Icono);
                }
            }

            Session.Save(sector);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int sectorId)
        {
            var sector = Session.Get<Sector>(sectorId);

            if (sector == null) throw new Exception("Sector Invalido");

            if (sector.Icono != null)
            {
                var archivoId = sector.Icono.Id;

                sector.Icono = null;

                archivoLogic.DeleteArchivo(archivoId);
            }

            try
            {
                Session.Delete(sector);

                Session.Transaction.Commit();
            }
            catch (ConstraintViolationException)
            {
                throw new Exception("El sector que se desesa eliminar esta siendo utilizado.");
            }

            return true;
        }

        private IEnumerable<Sector> GetSectores()
        {
            var sectores = Session.QueryOver<Sector>()
                                  .OrderBy(x => x.Nombre)
                                  .Asc
                                  .List();

            return sectores;
        }
    }
}

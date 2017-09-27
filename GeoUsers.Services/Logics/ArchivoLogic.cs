using AutoMapper;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Entities;
using GeoUsers.Services.Utils;
using NHibernate;
using System;

namespace GeoUsers.Services.Logics
{
    public class ArchivoLogic : BaseLogic
    {
        public ArchivoLogic(ISessionFactory sessionFactory,
                            IMapper mapper) : base(mapper, sessionFactory)
        {
        }

        public Archivo Get(int archivoId)
        {
            return Session.Get<Archivo>(archivoId);
        }

        public Archivo AddArchivo(ArchivoEditionData archivoData)
        {
            var fileName = FileUtils.SaveNewFile(archivoData.Extension, archivoData.Data);

            var archivo = new Archivo()
            {
                Nombre = archivoData.Nombre,
                Extension = archivoData.Extension,
                Ruta = fileName
            };

            return archivo;
        }

        public void EditArchivo(ArchivoEditionData archivoData)
        {
            var archivo = Get(archivoData.Id.Value);

            if (archivo == null) throw new Exception("Archivo invalido");

            archivo.Nombre = archivoData.Nombre;

            if (archivoData.Data != null)
            {
                FileUtils.DeleteFile(archivo.Ruta);

                archivo.Extension = archivoData.Extension;
                archivo.Ruta = FileUtils.SaveNewFile(archivoData.Extension, archivoData.Data);
            }
        }

        public void DeleteArchivo(int archivoId)
        {
            var archivo = Get(archivoId);

            FileUtils.DeleteFile(archivo.Ruta);
        }
    }
}

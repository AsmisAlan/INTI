﻿using AutoMapper;
using GeoUsers.Services.Model;
using GeoUsers.Services.Model.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoUsers.Services.Logics
{
    public class UsuarioLogic : BaseLogic
    {
        readonly RubroLogic rubroLogic;
        readonly OrganizacionLogic organizacionLogic;
        readonly LocalidadLogic localidadLogic;

        public UsuarioLogic(RubroLogic rubroLogic,
                            LocalidadLogic localidadLogic,
                            OrganizacionLogic organizacionLogic,
                            IMapper mapper,
                            ISessionFactory sessionFactory) : base(mapper, sessionFactory)
        {
            this.rubroLogic = rubroLogic;
            this.localidadLogic = localidadLogic;
            this.organizacionLogic = organizacionLogic;
        }

        public Usuario GetUsuario(int usuarioId)
        {
            return Session.Get<Usuario>(usuarioId);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return Session.QueryOver<Usuario>()
                          .List()
                          .OrderBy(x => x.Nombre);
        }

        public IEnumerable<IdAndValue> GetForSelection(ICollection<int> currentIds)
        {
            var usuarios = Session.QueryOver<Usuario>()
                          .WhereRestrictionOn(x => x.Id)
                          .Not.IsIn(currentIds.ToArray())
                          .List()
                          .OrderBy(x => x.Nombre);

            return Mapper.Map<IEnumerable<IdAndValue>>(usuarios);
        }

        public bool Create(string nombre,
                           string direccion,
                           string telefono,
                           string email,
                           string web,
                           string contactoCargo,
                           bool usuarioGeoUsersServices,
                           int personal,
                           long? cuit,
                           int rubroId,
                           int organizacionId,
                           int localidadId)
        {
            var rubro = rubroLogic.GetRubro(rubroId);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var organizacion = organizacionLogic.GetOrganizacion(organizacionId);

            if (organizacion == null) throw new Exception("Organizacion Invalida");

            var localidad = localidadLogic.GetLocalidad(localidadId);

            if (localidad == null) throw new Exception("Localidad Invalida");

            var usuario = new Usuario()
            {
                Nombre = nombre,
                ContactoCargo = contactoCargo,
                Cuit = cuit,
                Direccion = direccion,
                Email = email,
                Personal = personal,
                Telefono = telefono,
                Web = web,
                UsuarioGeoUsersServices = usuarioGeoUsersServices,
                Localidad = localidad,
                Organizacion = organizacion,
                Rubro = rubro
            };

            Session.Save(usuario);

            Session.Transaction.Commit();

            return true;
        }

        public bool Edit(int usuarioId,
                         string nombre,
                         string direccion,
                         string telefono,
                         string email,
                         string web,
                         string contactoCargo,
                         bool usuarioGeoUsersServices,
                         int personal,
                         long? cuit,
                         int rubroId,
                         int organizacionId,
                         int localidadId)
        {
            var usuario = Session.Get<Usuario>(usuarioId);

            if (usuario == null) throw new Exception("Usuario Invalido");

            var rubro = rubroLogic.GetRubro(rubroId);

            if (rubro == null) throw new Exception("Rubro Invalido");

            var organizacion = organizacionLogic.GetOrganizacion(organizacionId);

            if (organizacion == null) throw new Exception("Organizacion Invalida");

            var localidad = localidadLogic.GetLocalidad(localidadId);

            if (localidad == null) throw new Exception("Localidad Invalida");

            usuario.ContactoCargo = contactoCargo;
            usuario.Cuit = cuit;
            usuario.Direccion = direccion;
            usuario.Email = email;
            usuario.Localidad = localidad;
            usuario.Nombre = nombre;
            usuario.Organizacion = organizacion;
            usuario.Personal = personal;
            usuario.Rubro = rubro;
            usuario.Telefono = telefono;
            usuario.UsuarioGeoUsersServices = usuarioGeoUsersServices;
            usuario.Web = web;

            Session.Save(usuario);

            Session.Transaction.Commit();

            return true;
        }

        public bool Delete(int usuarioId)
        {
            var usuario = Session.Get<Usuario>(usuarioId);

            if (usuario == null) throw new Exception("Usuario Invalido");

            Session.Delete(usuario);

            return true;
        }
    }
}

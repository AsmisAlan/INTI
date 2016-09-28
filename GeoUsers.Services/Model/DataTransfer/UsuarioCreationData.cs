﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsers.Services.Model.DataTransfer
{
    public class UsuarioCreationData : UsuarioHeader
    {
        protected string telefono { get; set; }

        protected string contactoCargo { get; set; }

        protected virtual int personal { get; set; }

        protected virtual string web { get; set; }

        protected virtual long? cuit { get; set; }

        protected virtual bool usuarioInti { get; set; }

        protected int localidadId { get; set; }

        protected int organizacionId { get; set; }

        protected int rubroId { get; set; }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }

        public string ContactoCargo
        {
            get { return contactoCargo; }
            set
            {
                contactoCargo = value;
                OnPropertyChanged(nameof(ContactoCargo));
            }
        }

        public virtual int Personal
        {
            get { return personal; }
            set
            {
                personal = value;
                OnPropertyChanged(nameof(Personal));
            }
        }

        public virtual string Web
        {
            get { return web; }
            set
            {
                web = value;
                OnPropertyChanged(nameof(Web));
            }
        }

        public virtual long? Cuit
        {
            get { return cuit; }
            set
            {
                cuit = value;
                OnPropertyChanged(nameof(Cuit));
            }
        }

        public virtual bool UsuarioInti
        {
            get { return usuarioInti; }
            set
            {
                usuarioInti = value;
                OnPropertyChanged(nameof(UsuarioInti));
            }
        }

        public int LocalidadId
        {
            get { return localidadId; }
            set
            {
                localidadId = value;
                OnPropertyChanged(nameof(LocalidadId));
            }
        }

        public int OrganizacionId
        {
            get { return organizacionId; }
            set
            {
                organizacionId = value;
                OnPropertyChanged(nameof(OrganizacionId));
            }
        }

        public int RubroId
        {
            get { return rubroId; }
            set
            {
                rubroId = value;
                OnPropertyChanged(nameof(RubroId));
            }
        }
    }
}
﻿using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class UsuarioCreationEditionViewModel
    {
        readonly UsuarioLogic usuarioLogic;
        readonly LocalidadLogic localidadLogic;
        readonly RubroLogic rubroLogic;
        readonly OrganizacionLogic organizacionLogic;

        public UsuarioCreationData Usuario { get; set; }
        public IEnumerable<IdAndValue> Localidades { get; set; }
        public IEnumerable<IdAndValue> Organizaciones { get; set; }
        public IEnumerable<IdAndValue> Rubros { get; set; }

        public UsuarioCreationEditionViewModel()
        {
        }

        public UsuarioCreationEditionViewModel(UsuarioLogic usuarioLogic,
                                               LocalidadLogic localidadLogic,
                                               RubroLogic rubroLogic,
                                               OrganizacionLogic organizacionLogic)
        {
            this.usuarioLogic = usuarioLogic;
            this.localidadLogic = localidadLogic;
            this.rubroLogic = rubroLogic;
            this.organizacionLogic = organizacionLogic;

            Usuario = new UsuarioCreationData();
        }

        public async Task<bool> LoadData()
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    Localidades = localidadLogic.GetForSelection();
                    Rubros = rubroLogic.GetForSelection();
                    Organizaciones = organizacionLogic.GetForSelection();
                }
            });

            return true;
        }

        public async void Create()
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    usuarioLogic.Create(Usuario);
                }
            });
        }
    }
}
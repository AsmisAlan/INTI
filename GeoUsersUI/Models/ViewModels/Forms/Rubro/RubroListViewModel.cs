﻿using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class RubroListViewModel : BaseListViewModel
    {
        private readonly RubroLogic rubroLogic;

        public ObservableCollection<RubroHeaderData> Rubros { get; set; }

        public RubroListViewModel() { }

        public RubroListViewModel(RubroLogic rubroLogic)
        {
            this.rubroLogic = rubroLogic;

            Rubros = new ObservableCollection<RubroHeaderData>();
        }

        public async Task<bool> LoadRubros()
        {
            IEnumerable<RubroHeaderData> rubros = null;

            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    rubros = rubroLogic.GetAll();
                }
            });

            Rubros.Update(rubros);

            return true;
        }

        public async Task<bool> Delete(int rubroId)
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    rubroLogic.Delete(rubroId);
                }
            });

            return true;
        }
    }
}

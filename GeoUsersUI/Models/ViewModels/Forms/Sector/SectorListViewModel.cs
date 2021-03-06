﻿using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    class SectorListViewModel : BaseListViewModel
    {
        private readonly SectorLogic sectorLogic;

        public ObservableCollection<SectorHeaderData> Sectores { get; set; }

        public SectorListViewModel() { }

        public SectorListViewModel(SectorLogic sectorLogic)
        {
            this.sectorLogic = sectorLogic;

            Sectores = new ObservableCollection<SectorHeaderData>();
            WindowTitle = "Sectores";
        }

        public async Task<bool> LoadSectores()
        {
            IEnumerable<SectorHeaderData> sectores = null;

            await RequestService.Execute(() =>
            {
                sectores = sectorLogic.GetAll();
            });

            Sectores.Update(sectores);

            return true;
        }

        public async Task<bool> Delete(int sectorId)
        {
            await RequestService.Execute(() =>
            {
                sectorLogic.Delete(sectorId);
            });

            return true;
        }

        protected override void Export(string filePath)
        {
            sectorLogic.ExportToExcel(filePath);
        }
    }
}

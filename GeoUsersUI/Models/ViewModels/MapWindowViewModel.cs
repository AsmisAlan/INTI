using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class MapWindowViewModel : BaseWindowViewModel
    {
        private readonly OrganizacionLogic organizacionLogic;

        private bool loadingMap { get; set; }

        public bool LoadingMap
        {
            get
            {
                return loadingMap;
            }
            set
            {
                loadingMap = value;

                OnPropertyChanged(nameof(LoadingMap));
            }
        }

        public OrganizacionHeaderData Organizacion { get; set; }

        public MapWindowViewModel(OrganizacionLogic organizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;
        }

        public async Task LoadOrganizacion(int organizacionId)
        {
            await RequestService.Execute(() =>
            {
                Organizacion = organizacionLogic.GetOrganiacionHeader(organizacionId);
            });
        }
    }
}

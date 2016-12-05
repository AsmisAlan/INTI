using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.Helpers;
using GeoUsersUI.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class MainViewModel
    {
        readonly OrganizacionLogic organizacionLogic;
        readonly SectorLogic sectorLogic;

        public ObservableCollection<OrganizacionHeader> Organizaciones { get; set; }

        public Task<IEnumerable<OrganizacionHeader>> InitializationTask { get; private set; }

        public MainMenuContainer MainMenu { get; set; }

        public FilterStatus OrganizacionesFilter { get; set; }

        public bool Loading { get; set; }

        public MainViewModel()
        {
        }

        public MainViewModel(OrganizacionLogic organizacionLogic,
                             SectorLogic sectorLogic)
        {
            this.organizacionLogic = organizacionLogic;
            this.sectorLogic = sectorLogic;

            OrganizacionesFilter = new FilterStatus();
            InitializationTask = ExecuteDataFunction();
        }

        private async Task<IEnumerable<OrganizacionHeader>> ExecuteDataFunction()
        {
            Loading = true;

            var results = await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    return organizacionLogic.GetOrganizacionHeaders(OrganizacionesFilter.Filter);
                }
            });

            Organizaciones = new ObservableCollection<OrganizacionHeader>(results);

            Loading = false;

            return results;
        }

        public bool? ApplySectorFilter()
        {
            var form = new SmartSelectWindow(() => { return sectorLogic.GetForSelection(OrganizacionesFilter.Filter.SectorIds); },
                                             () => { return sectorLogic.GetByIds(OrganizacionesFilter.Filter.SectorIds); },
                                             OrganizacionesFilter.Filter.SectorIds);
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                OrganizacionesFilter.Filter.SectorIds = form.GetSelection().ToList();
            }

            return form.DialogResult;
        }
    }
}

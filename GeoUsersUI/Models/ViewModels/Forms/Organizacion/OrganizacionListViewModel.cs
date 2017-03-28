using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Enums;
using GeoUsersUI.Models.ViewModels.Helpers;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels
{
    public class OrganizacionListViewModel : BaseListViewModel
    {
        private readonly OrganizacionLogic organizacionLogic;
        private readonly SectorLogic sectorLogic;
        private readonly RubroLogic rubroLogic;
        private readonly LocalidadLogic localidadLogic;
        private readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        private Visibility filtersVisibility;

        public ObservableCollection<OrganizacionData> Organizaciones { get; set; }

        public FilterStatus OrganizacionesFilter { get; set; }

        public Visibility FiltersVisibility
        {
            get
            {
                return filtersVisibility;
            }
            set
            {
                filtersVisibility = value;

                OnPropertyChanged(nameof(FiltersVisibility));
            }
        }

        public OrganizacionListViewModel() { }

        public OrganizacionListViewModel(OrganizacionLogic organizacionLogic,
                                         SectorLogic sectorLogic,
                                         RubroLogic rubroLogic,
                                         LocalidadLogic localidadLogic,
                                         TipoOrganizacionLogic tipoOrganizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;
            this.sectorLogic = sectorLogic;
            this.rubroLogic = rubroLogic;
            this.localidadLogic = localidadLogic;
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            OrganizacionesFilter = new FilterStatus();

            OrganizacionesFilter.Filter.UsuarioInti = (int)UsuarioIntiStatus.Todos;

            Organizaciones = new ObservableCollection<OrganizacionData>();

            FiltersVisibility = Visibility.Visible;
        }

        public async Task<bool> LoadOrganizaciones()
        {
            IEnumerable<OrganizacionData> organizaciones = null;

            await RequestService.Execute(() =>
            {
                organizaciones = organizacionLogic.GetOrganizacionesByFilter(OrganizacionesFilter.Filter);
            });

            Organizaciones.Update(organizaciones);

            return true;
        }

        public async Task<bool> Delete(int organizacionId)
        {
            await RequestService.Execute(() =>
            {
                organizacionLogic.Delete(organizacionId);
            });

            return true;
        }

        public IEnumerable<IdAndValue> GetSectores()
        {
            return sectorLogic.GetForSelection();
        }

        public IEnumerable<IdAndValue> GetSectoresByIds()
        {
            return sectorLogic.GetByIds(OrganizacionesFilter.Filter.SectorIds);
        }

        public IEnumerable<IdAndValue> GetRubros()
        {
            return rubroLogic.GetForSelection();
        }

        public IEnumerable<IdAndValue> GetRubrosByIds()
        {
            return rubroLogic.GetByIds(OrganizacionesFilter.Filter.RubroIds);
        }

        public IEnumerable<IdAndValue> GetLocalidades()
        {
            return localidadLogic.GetForSelection();
        }

        public IEnumerable<IdAndValue> GetLocalidadesByIds()
        {
            return localidadLogic.GetByIds(OrganizacionesFilter.Filter.LocalidadIds);
        }

        public IEnumerable<IdAndValue> GetTipoOrganizaciones()
        {
            return tipoOrganizacionLogic.GetForSelection();
        }

        public IEnumerable<IdAndValue> GetTipoOrganizacionesByIds()
        {
            return tipoOrganizacionLogic.GetByIds(OrganizacionesFilter.Filter.TipoOrganizacionIds);
        }
    }
}

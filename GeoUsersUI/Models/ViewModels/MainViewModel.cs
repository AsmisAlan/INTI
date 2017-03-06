using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.Model.Enums;
using GeoUsers.Services.Utils;
using GeoUsersUI.Models.ViewModels.Helpers;
using GeoUsersUI.Utils;
using GeoUsersUI.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels
{
    public class MainViewModel : BaseWindowViewModel
    {
        private readonly OrganizacionLogic organizacionLogic;
        private readonly SectorLogic sectorLogic;
        private readonly RubroLogic rubroLogic;
        private readonly LocalidadLogic localidadLogic;
        private readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        private Visibility loadingMap { get; set; }

        public ObservableCollection<OrganizacionHeaderData> Organizaciones { get; set; }

        public ObservableCollection<IdAndValue> UsuarioIntiStatuses { get; set; }

        public Task<IEnumerable<OrganizacionHeaderData>> InitializationTask { get; private set; }

        public MenuContainer OrganizacionMenuContainer { get; set; }

        public MenuContainer TipoOrganizacionMenuContainer { get; set; }

        public MenuContainer SectorMenuContainer { get; set; }

        public MenuContainer RubroMenuContainer { get; set; }

        public MenuContainer LocalidadMenuContainer { get; set; }

        public FilterStatus OrganizacionesFilter { get; set; }

        public Visibility LoadingMap
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
        public MainViewModel()
        {
        }

        public MainViewModel(OrganizacionLogic organizacionLogic,
                             SectorLogic sectorLogic,
                             RubroLogic rubroLogic,
                             LocalidadLogic localidadLogic,
                             TipoOrganizacionLogic tipoOrganizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;
            this.sectorLogic = sectorLogic;
            this.localidadLogic = localidadLogic;
            this.rubroLogic = rubroLogic;
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            UsuarioIntiStatuses = new ObservableCollection<IdAndValue>();

            var usuarioIntiStatus = new IdAndValue()
            {
                Id = ((int)UsuarioIntiStatus.UsuarioInti),
                Value = EnumUtils.GetDescription(UsuarioIntiStatus.UsuarioInti)
            };

            var noUsuarioIntiStatus = new IdAndValue()
            {
                Id = ((int)UsuarioIntiStatus.NoUsuarioInti),
                Value = EnumUtils.GetDescription(UsuarioIntiStatus.NoUsuarioInti)
            };

            var todosStatus = new IdAndValue()
            {
                Id = ((int)UsuarioIntiStatus.Todos),
                Value = EnumUtils.GetDescription(UsuarioIntiStatus.Todos)
            };

            UsuarioIntiStatuses.Add(usuarioIntiStatus);
            UsuarioIntiStatuses.Add(noUsuarioIntiStatus);
            UsuarioIntiStatuses.Add(todosStatus);

            OrganizacionesFilter = new FilterStatus();

            Organizaciones = new ObservableCollection<OrganizacionHeaderData>();

            InitializationTask = UpdateOrganizacionHeaders();
        }

        public async Task<IEnumerable<OrganizacionHeaderData>> UpdateOrganizacionHeaders()
        {
            var results = await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    return organizacionLogic.GetByFilter(OrganizacionesFilter.Filter);
                }
            });

            Organizaciones.Update(results);

            return results;
        }

        public bool? ApplySectorFilter()
        {
            var form = new SmartSelectWindow(() => { return sectorLogic.GetForSelection(); },
                                             () => { return sectorLogic.GetByIds(OrganizacionesFilter.Filter.SectorIds); },
                                             OrganizacionesFilter.Filter.SectorIds,
                                             "Sectores");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                OrganizacionesFilter.Filter.SectorIds = form.GetSelection().ToList();

                OrganizacionesFilter.UpdateStatuses();
            }

            return form.DialogResult;
        }

        public bool? ApplyRubroFilter()
        {
            var form = new SmartSelectWindow(() => { return rubroLogic.GetForSelection(); },
                                             () => { return rubroLogic.GetByIds(OrganizacionesFilter.Filter.RubroIds); },
                                             OrganizacionesFilter.Filter.RubroIds,
                                             "Rubros");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                OrganizacionesFilter.Filter.RubroIds = form.GetSelection().ToList();

                OrganizacionesFilter.UpdateStatuses();
            }

            return form.DialogResult;
        }

        public bool? ApplyLocalidadFilter()
        {
            var form = new SmartSelectWindow(() => { return localidadLogic.GetForSelection(); },
                                             () => { return localidadLogic.GetByIds(OrganizacionesFilter.Filter.LocalidadIds); },
                                             OrganizacionesFilter.Filter.LocalidadIds,
                                             "Localidades");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                OrganizacionesFilter.Filter.LocalidadIds = form.GetSelection().ToList();

                OrganizacionesFilter.UpdateStatuses();
            }

            return form.DialogResult;
        }

        public bool? ApplyTipoOrganizacionFilter()
        {
            var currentIds = OrganizacionesFilter.Filter.TipoOrganizacionIds;

            var form = new SmartSelectWindow(() => { return tipoOrganizacionLogic.GetForSelection(); },
                                             () => { return tipoOrganizacionLogic.GetByIds(currentIds); },
                                             OrganizacionesFilter.Filter.TipoOrganizacionIds,
                                             "Tipos de Organizaciones");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                OrganizacionesFilter.Filter.TipoOrganizacionIds = form.GetSelection().ToList();

                OrganizacionesFilter.UpdateStatuses();
            }

            return form.DialogResult;
        }

        public void FilterOrganizaciones(string serchTerm)
        {
            if (serchTerm == "")
            {
                return;
            }

            foreach (var organizacion in Organizaciones)
            {
                organizacion.IsActive = organizacion.Nombre.Contains(serchTerm) ||
                                        organizacion.Direccion.Contains(serchTerm) ||
                                        organizacion.Email.Contains(serchTerm) ||
                                        organizacion.Telefono.Contains(serchTerm);
            }
        }
    }
}

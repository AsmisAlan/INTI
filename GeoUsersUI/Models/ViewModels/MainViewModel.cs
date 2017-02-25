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

namespace GeoUsersUI.Models.ViewModels
{
    public class MainViewModel : BaseNotifierEntity
    {
        readonly OrganizacionLogic organizacionLogic;
        readonly SectorLogic sectorLogic;
        readonly RubroLogic rubroLogic;
        readonly LocalidadLogic localidadLogic;
        readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        public ObservableCollection<OrganizacionHeaderData> Organizaciones { get; set; }

        public ObservableCollection<IdAndValue> UsuarioIntiStatuses { get; set; }

        public Task<IEnumerable<OrganizacionHeaderData>> InitializationTask { get; private set; }

        public MenuContainer OrganizacionMenuContainer { get; set; }

        public MenuContainer TipoOrganizacionMenuContainer { get; set; }

        public MenuContainer SectorMenuContainer { get; set; }

        public MenuContainer RubroMenuContainer { get; set; }

        public MenuContainer LocalidadMenuContainer { get; set; }

        public FilterStatus OrganizacionesFilter { get; set; }

        public bool Loading { get; set; }

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
            Loading = true;

            var results = await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    return organizacionLogic.GetByFilter(OrganizacionesFilter.Filter);
                }
            });

            Organizaciones.Update(results);

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

                OrganizacionesFilter.UpdateStatuses();
            }

            return form.DialogResult;
        }

        public bool? ApplyRubroFilter()
        {
            var form = new SmartSelectWindow(() => { return rubroLogic.GetForSelection(OrganizacionesFilter.Filter.RubroIds); },
                                             () => { return rubroLogic.GetByIds(OrganizacionesFilter.Filter.RubroIds); },
                                             OrganizacionesFilter.Filter.RubroIds);
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
            var form = new SmartSelectWindow(() => { return localidadLogic.GetForSelection(OrganizacionesFilter.Filter.LocalidadIds); },
                                             () => { return localidadLogic.GetByIds(OrganizacionesFilter.Filter.LocalidadIds); },
                                             OrganizacionesFilter.Filter.LocalidadIds);
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
            var form = new SmartSelectWindow(() =>
                                             {
                                                 return tipoOrganizacionLogic.GetForSelection(OrganizacionesFilter.Filter.TipoOrganizacionIds);
                                             },
                                             () =>
                                             {
                                                 return tipoOrganizacionLogic.GetByIds(OrganizacionesFilter.Filter.TipoOrganizacionIds);
                                             },
                                             OrganizacionesFilter.Filter.TipoOrganizacionIds);
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                OrganizacionesFilter.Filter.TipoOrganizacionIds = form.GetSelection().ToList();

                OrganizacionesFilter.UpdateStatuses();
            }

            return form.DialogResult;
        }
    }
}

﻿using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.SQLExceptions;
using GeoUsersUI.Models.ViewModels.Forms;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for TipoOrganizacionList.xaml
    /// </summary>
    public partial class TipoOrganizacionList : Window
    {
        private TipoOrganizacionListViewModel ViewModel { get; set; }

        public TipoOrganizacionList()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<TipoOrganizacionListViewModel>();

            Initialize();
        }

        private async void Initialize()
        {
            await UpdateTipoOrganizaciones();
        }

        private async Task<bool> UpdateTipoOrganizaciones()
        {
            ViewModel.SetLoading(true);

            await ViewModel.LoadTipoOrganizaciones();

            ViewModel.SetLoading(false);

            return true;
        }

        private async void OpenEditionForm()
        {
            if (TipoOrganizacionGrid.SelectedItem == null)
            {
                return;
            }

            var tipoOrganizacion = (TipoOrganizacionHeaderData)TipoOrganizacionGrid.SelectedItem;

            var form = new TipoOrganizacionCreationEditionForm(tipoOrganizacion.Id);

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateTipoOrganizaciones();
            }
        }

        private void TipoOrganizacionGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenEditionForm();
        }

        private void EntityListButtonBar_OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void EntityListButtonBar_OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            var form = new TipoOrganizacionCreationEditionForm();

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateTipoOrganizaciones();
            }
        }

        private async void EntityListButtonBar_OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBoxUtils.Confirmation("Seguro desea eliminar el tipo de organización ?");

            if (result == MessageBoxResult.Yes)
            {
                var tipoOrganizacion = (TipoOrganizacionHeaderData)TipoOrganizacionGrid.SelectedItem;

                try
                {
                    await ViewModel.Delete(tipoOrganizacion.Id);
                }
                catch (Exception)
                {
                }


                await UpdateTipoOrganizaciones();
            }
        }

        private void EntityListButtonBar_OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            OpenEditionForm();
        }

        private async void DataGridExportButtonBar_OnExportButtonClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.ExportToExcel();
        }

        private void DataGridExportButtonBar_OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintUtils.PrintDataGrid(TipoOrganizacionGrid, "Tipos de Organizacion");
        }
    }
}

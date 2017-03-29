using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.Models.ViewModels
{
    public class BaseListViewModel : BaseNotifierEntity
    {
        private Visibility loadingTable;

        protected bool isExporting;

        public bool IsExporting
        {
            get
            {
                return isExporting;
            }
            set
            {
                isExporting = value;

                OnPropertyChanged(nameof(IsExporting));
            }
        }

        public Visibility LoadingTable
        {
            get
            {
                return loadingTable;
            }
            set
            {
                loadingTable = value;

                OnPropertyChanged(nameof(LoadingTable));
            }
        }

        public BaseListViewModel() { }

        public void Export(DataGrid grid)
        {
            IsExporting = true;

            try
            {
                var saveFileDialog = new SaveFileDialog();

                saveFileDialog.InitialDirectory = "C:";
                saveFileDialog.Title = "Guardar";
                saveFileDialog.FileName = "reporte";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (saveFileDialog.ShowDialog().Value)
                {
                    ExcelExportUtils.ExportToExcel(grid,
                                                   saveFileDialog.FileName);
                }
            }
            catch (Exception e)
            {
                MessageBoxUtils.Error(e.Message);
            }
            finally
            {
                IsExporting = false;
            }
        }

        public void StartLoadingTable()
        {
            LoadingTable = Visibility.Visible;
        }

        public void StopLoadingTable()
        {
            LoadingTable = Visibility.Hidden;
        }
    }
}

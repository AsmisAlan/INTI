using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.Models.ViewModels
{
    public class BaseListViewModel : BaseNotifierEntity
    {
        private Visibility loadingTable;

        protected bool IsExporting { get; set; }

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
            if (!IsExporting)
            {
                IsExporting = true;

                try
                {
                    ExcelExportUtils.ExportToExcel(grid);
                }
                catch
                {
                }
                finally
                {
                    IsExporting = false;
                }
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

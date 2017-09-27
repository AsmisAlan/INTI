using GeoUsersUI.Utils;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseListViewModel : BaseWindowViewModel
    {
        private bool loadingTable;

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

        public bool LoadingTable
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

        public async Task ExportToExcel()
        {
            IsExporting = true;

            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "C:";
            saveFileDialog.Title = "Guardar";
            saveFileDialog.FileName = "reporte";
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel Files|*.xls";

            if (saveFileDialog.ShowDialog().Value)
            {
                try
                {
                    await RequestService.Execute(() =>
                    {
                        Export(saveFileDialog.FileName);
                    });
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
        }

        public void SetLoading(bool loading)
        {
            LoadingTable = loading;
        }

        protected abstract void Export(string filePath);
    }
}

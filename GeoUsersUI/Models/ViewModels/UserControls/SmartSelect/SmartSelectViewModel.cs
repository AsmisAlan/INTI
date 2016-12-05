using GeoUsers.Services;
using GeoUsers.Services.Model.DataTransfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.SmartSelect
{
    public class SmartSelectViewModel
    {
        public ObservableCollection<IdAndValue> Selection { get; private set; }
        public Func<IEnumerable<IdAndValue>> DataFunction { get; set; }
        public Func<IEnumerable<IdAndValue>> GetSelectionFunction { get; set; }
        public bool Loading { get; set; }
        public string EntityListTitle { get; set; }

        public ObservableCollection<IdAndValue> Entities { get; set; }
        public string RemoveEntityButtonContent { get; set; }
        public string AddEntityButtonContent { get; set; }

        public SmartSelectViewModel()
        {
            AddEntityButtonContent = "<";
            RemoveEntityButtonContent = ">";
        }

        public async Task<bool> Initialize(Func<IEnumerable<IdAndValue>> dataFunction,
                                           Func<IEnumerable<IdAndValue>> getSelectionDataFunction,
                                           IEnumerable<int> selection)
        {
            DataFunction = dataFunction;
            GetSelectionFunction = getSelectionDataFunction;

            return await LoadInitialData(selection);
        }

        private async Task<bool> LoadInitialData(IEnumerable<int> selectedIds)
        {
            return await DataFunctionWrapper(selectedIds);
        }

        private async Task<bool> DataFunctionWrapper(IEnumerable<int> selectedIds)
        {
            return await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    Entities = new ObservableCollection<IdAndValue>(DataFunction());

                    SetSelection(GetSelectionFunction());

                    return true;
                }
            });
        }

        public void SetSelection(IEnumerable<IdAndValue> selection)
        {
            Selection = new ObservableCollection<IdAndValue>(selection);
        }

        public void AddSelection(IdAndValue item)
        {
            Selection.Add(item);
            Entities.Remove(item);
        }

        public void RemoveSelection(IdAndValue item)
        {
            Selection.Remove(item);
            Entities.Add(item);
        }
    }
}

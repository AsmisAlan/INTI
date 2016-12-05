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

        public void SetSelection(IEnumerable<IdAndValue> selection)
        {
            Selection = new ObservableCollection<IdAndValue>(selection);
        }

        public void Initialize(Func<IEnumerable<IdAndValue>> dataFunction,
                               IEnumerable<IdAndValue> selection)
        {
            Loading = true;
            DataFunction = dataFunction;
            SetSelection(selection);
            executeDataFunction();
        }

        private async void executeDataFunction()
        {
            var results = await DataFunctionWrapper();

            var selectedIds = Selection.Select(x => x.Id);
            var entities = results.Where(x => !selectedIds.Contains(x.Id));

            Entities = new ObservableCollection<IdAndValue>(entities);

            Loading = false;
        }

        private async Task<IEnumerable<IdAndValue>> DataFunctionWrapper()
        {
            return await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    return DataFunction();
                }
            });
        }

        private void AddSelection(IdAndValue item)
        {
            Selection.Add(item);
            Entities.Remove(item);
        }

        private void RemoveSelection(IdAndValue item)
        {
            Selection.Remove(item);
            Entities.Add(item);
        }
    }
}

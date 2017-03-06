using GeoUsers.Services;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.UserControls;
using GeoUsersUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class SmartSelectViewModel
    {
        public ObservableCollection<SmartSelectItem> Selection { get; private set; }
        public Func<IEnumerable<IdAndValue>> DataFunction { get; set; }
        public Func<IEnumerable<IdAndValue>> GetSelectionFunction { get; set; }
        public bool Loading { get; set; }
        public string EntityListTitle { get; set; }

        public ObservableCollection<SmartSelectItem> Entities { get; set; }
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

            Entities = new ObservableCollection<SmartSelectItem>();
            Selection = new ObservableCollection<SmartSelectItem>();

            return await LoadInitialData(selection);
        }

        private async Task<bool> LoadInitialData(IEnumerable<int> selectedIds)
        {
            IEnumerable<IdAndValue> entities = null;
            IEnumerable<IdAndValue> selection = null;

            await Task.Run(() =>
           {
               using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
               {
                   entities = DataFunction();
                   selection = GetSelectionFunction();
               }
           });

            CreateSelectionItemsCollection(selection);
            CreateEntityItemsCollection(entities);

            return true;
        }

        public void AddSelection(SmartSelectItem item)
        {
            var copy = item.Copy();

            Selection.Add(copy);

            item.IsActive = false;
        }

        public void RemoveSelection(SmartSelectItem item)
        {
            Selection.Remove(item);

            var entityItem = Entities.First(x => x.Id == item.Id);

            entityItem.IsActive = true;
        }

        public void OrderSelection()
        {
            Selection.Update(Selection.OrderBy(x => x.Value));
        }

        private void CreateSelectionItemsCollection(IEnumerable<IdAndValue> items)
        {
            var smartSelectItems = new Collection<SmartSelectItem>();

            foreach (var item in items)
            {
                var smartSelectItem = new SmartSelectItem()
                {
                    Id = item.Id,
                    IsActive = true,
                    Value = item.Value
                };

                smartSelectItems.Add(smartSelectItem);
            }

            Selection.Update(smartSelectItems);
        }

        private void CreateEntityItemsCollection(IEnumerable<IdAndValue> items)
        {
            var smartSelectItems = new Collection<SmartSelectItem>();

            foreach (var item in items)
            {
                var smartSelectItem = new SmartSelectItem()
                {
                    Id = item.Id,
                    IsActive = Selection.FirstOrDefault(x => x.Id == item.Id) == null,
                    Value = item.Value
                };

                smartSelectItems.Add(smartSelectItem);
            }

            Entities.Update(smartSelectItems);
        }
    }

}

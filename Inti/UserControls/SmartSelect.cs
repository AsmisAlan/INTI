using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using GeoUsers.Services.Model;
using System.Threading.Tasks;
using GeoUsers.Services;

namespace Inti.UserControls
{
    public partial class SmartSelect : UserControl
    {
        public ICollection<IdAndValue> Selection { get; private set; }
        public Func<IEnumerable<IdAndValue>> DataFunction { get; set; }
        public Func<IdAndValue, bool> ItemChangedCallback { get; set; }

        private IEnumerable<IdAndValue> Items { get; set; }
        private IEnumerable<IdAndValue> FilteredItems { get; set; }

        public SmartSelect()
        {
            InitializeComponent();
            selectionList.SelectionMode = SelectionMode.MultiExtended;
            entityList.SelectionMode = SelectionMode.MultiExtended;
        }

        public void SetSelection(IEnumerable<IdAndValue> selection)
        {
            Selection = selection.ToList();
            selectionList.Items.AddRange(selection.ToArray());
        }

        public void Initialize(Func<IEnumerable<IdAndValue>> dataFunction,
                               Func<IdAndValue, bool> itemChangedCallback,
                               IEnumerable<IdAndValue> selection)
        {
            DataFunction = dataFunction;
            ItemChangedCallback = ItemChangedCallback;
            SetSelection(selection);
            executeDataFunction();
        }

        private async void executeDataFunction()
        {
            loadingLabel.Visible = true;
            selectionList.Enabled = false;
            entityList.Enabled = false;
            addEntityButton.Enabled = false;
            removeEntityButton.Enabled = false;

            var results = await DataFunctionWrapper();

            var selectedIds = Selection.Select(x => x.Id);
            Items = results.Where(x => !selectedIds.Contains(x.Id));

            entityList.Items.Clear();
            entityList.Items.AddRange(Items.ToArray());

            loadingLabel.Visible = false;
            selectionList.Enabled = true;
            entityList.Enabled = true;
            addEntityButton.Enabled = true;
            removeEntityButton.Enabled = true;
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
            selectionList.Items.Add(item);
            entityList.Items.Remove(item);
        }

        private void RemoveSelection(IdAndValue item)
        {
            Selection.Remove(item);
            selectionList.Items.Remove(item);
            entityList.Items.Add(item);
        }

        private void addEntityButton_Click(object sender, EventArgs e)
        {
            object[] selection = new object[entityList.SelectedItems.Count];

            entityList.SelectedItems.CopyTo(selection, 0);

            if (selection == null || selection.Count() == 0) return;

            foreach (var item in selection)
            {
                AddSelection((IdAndValue)item);
            }
        }

        private void removeEntityButton_Click(object sender, EventArgs e)
        {
            object[] selection = new object[selectionList.SelectedItems.Count];

            selectionList.SelectedItems.CopyTo(selection, 0);

            if (selection == null || selection.Count() == 0) return;

            foreach (var item in selection)
            {
                RemoveSelection((IdAndValue)item);
            }
        }

        private void selectedItemDoubleClicked(object sender, MouseEventArgs e)
        {
            var selectedItem = selectionList.SelectedItem;

            Selection.Remove((IdAndValue)selectedItem);
            selectionList.Items.Remove(selectedItem);
            entityList.Items.Add(selectedItem);
        }

        private void entityDoubleClicked(object sender, MouseEventArgs e)
        {
            var selectedItem = entityList.SelectedItem;

            Selection.Add((IdAndValue)selectedItem);
            selectionList.Items.Add(selectedItem);
            entityList.Items.Remove(selectedItem);
        }
    }
}

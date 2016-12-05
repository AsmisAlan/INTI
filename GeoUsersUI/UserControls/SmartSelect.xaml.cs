using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.SmartSelect;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for SmartSelect.xaml
    /// </summary>
    public partial class SmartSelect : UserControl
    {
        public SmartSelectViewModel ViewModel { get; set; }

        public SmartSelect()
        {
            InitializeComponent();

            DataContext = ViewModel = new SmartSelectViewModel();
        }

        public async void Initialize(Func<IEnumerable<IdAndValue>> dataFunction,
                               Func<IEnumerable<IdAndValue>> getSelectionDataFunction,
                               IEnumerable<int> selection)
        {
            await ViewModel.Initialize(dataFunction, getSelectionDataFunction, selection);

            EntitiesListBox.ItemsSource = ViewModel.Entities;
            SelectionListBox.ItemsSource = ViewModel.Selection;
        }

        public void RemoveEntityButton_Click(object sender, EventArgs e)
        {
            var value = ((IdAndValue)SelectionListBox.SelectedItem);

            if (value != null)
            {
                ViewModel.RemoveSelection(value);
            }
        }

        public void AddEntityButton_Click(object sender, EventArgs e)
        {
            var value = ((IdAndValue)EntitiesListBox.SelectedItem);

            if (value != null)
            {
                ViewModel.AddSelection(value);
            }
        }

        public IEnumerable<int> GetSelection()
        {
            return ViewModel.Selection.Select(x => x.Id);
        }
    }
}

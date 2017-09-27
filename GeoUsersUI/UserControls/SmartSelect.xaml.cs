using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using GeoUsersUI.Models.ViewModels.UserControls;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for SmartSelect.xaml
    /// </summary>
    public partial class SmartSelect : UserControl, INotifyPropertyChanged
    {
        public string EntityListHeader
        {
            get
            {
                return (string)GetValue(EntityListHeaderProperty);
            }
            set
            {
                SetValue(EntityListHeaderProperty, value);

                OnPropertyChanged(nameof(EntityListHeader));
            }
        }

        public static readonly DependencyProperty EntityListHeaderProperty =
             DependencyProperty.Register("EntityListHeader",
                                         typeof(string),
                                         typeof(SmartSelect),
                                         new PropertyMetadata(""));

        public event PropertyChangedEventHandler PropertyChanged;

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
            if (SelectionListBox.SelectedItems != null && SelectionListBox.SelectedItems.Count > 0)
            {
                var itemsToDelete = new SmartSelectItem[SelectionListBox.SelectedItems.Count];

                SelectionListBox.SelectedItems.CopyTo(itemsToDelete, 0);

                foreach (var item in itemsToDelete)
                {
                    ViewModel.RemoveSelection(item as SmartSelectItem);
                }
            }
        }

        public void AddEntityButton_Click(object sender, EventArgs e)
        {
            if (EntitiesListBox.SelectedItems != null && EntitiesListBox.SelectedItems.Count > 0)
            {
                foreach (var item in EntitiesListBox.SelectedItems)
                {
                    ViewModel.AddSelection(item as SmartSelectItem);
                }
            }

            ViewModel.OrderSelection();

            EntitiesListBox.SelectedItems.Clear();
        }

        public IEnumerable<int> GetSelection()
        {
            return ViewModel.Selection.Select(x => x.Id);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SelectionListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = SelectionListBox.SelectedItem as SmartSelectItem;

            if (item == null) return;

            ViewModel.RemoveSelection(item);
        }

        private void EntitiesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = EntitiesListBox.SelectedItem as SmartSelectItem;

            if (item == null) return;

            ViewModel.AddSelection(item);
        }
    }
}

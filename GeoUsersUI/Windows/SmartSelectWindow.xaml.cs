using GeoUsers.Services.Model.DataTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for SmartSelectWindow.xaml
    /// </summary>
    public partial class SmartSelectWindow : Window, INotifyPropertyChanged
    {
        private string entityListHeader { get; set; }

        public string EntityListHeader
        {
            get
            {
                return entityListHeader;
            }
            set
            {
                entityListHeader = value;

                OnPropertyChanged(nameof(EntityListHeader));
            }
        }

        public SmartSelectWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public SmartSelectWindow(Func<IEnumerable<IdAndValue>> dataFunction,
                                 Func<IEnumerable<IdAndValue>> getSelectioDataFunction,
                                 IEnumerable<int> selection,
                                 string entityListHeader)
        {
            InitializeComponent();

            EntityListHeader = entityListHeader;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = this;

            SmartSelectControl.Initialize(dataFunction, getSelectioDataFunction, selection);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<int> GetSelection()
        {
            return SmartSelectControl.GetSelection();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Close();
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

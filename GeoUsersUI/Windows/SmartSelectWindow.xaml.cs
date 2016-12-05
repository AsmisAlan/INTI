using GeoUsers.Services.Model.DataTransfer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for SmartSelectWindow.xaml
    /// </summary>
    public partial class SmartSelectWindow : Window
    {
        public SmartSelectWindow()
        {
            InitializeComponent();
        }

        public SmartSelectWindow(Func<IEnumerable<IdAndValue>> dataFunction,
                                 Func<IEnumerable<IdAndValue>> getSelectioDataFunction,
                                 IEnumerable<int> selection)
        {
            InitializeComponent();

            SmartSelectControl.Initialize(dataFunction, getSelectioDataFunction, selection);
        }

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
    }
}

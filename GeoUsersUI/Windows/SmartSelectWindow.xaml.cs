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
                                 IEnumerable<IdAndValue> selection)
        {
            InitializeComponent();

            SmartSelectControl.Initialize(dataFunction, selection);
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

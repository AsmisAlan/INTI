using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Extensions
{
    public static class MessageBoxExtensions
    {
        public static void ShowFormIncompleteError()
        {
            MessageBox.Show("Hay campos obligatorios incompletos.", "Error", MessageBoxButton.OK);
        }
    }
}

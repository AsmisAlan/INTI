using System.Windows;

namespace GeoUsersUI.Utils
{
    public static class MessageBoxUtils
    {
        public static void ShowFormIncompleteError()
        {
            MessageBox.Show("Hay campos obligatorios incompletos.", "Error", MessageBoxButton.OK);
        }

        public static MessageBoxResult ShowConfirmationBox(string confirmationMessage)
        {
            return MessageBox.Show(confirmationMessage,
                                   "Confirmar",
                                   MessageBoxButton.YesNo);
        }
    }
}

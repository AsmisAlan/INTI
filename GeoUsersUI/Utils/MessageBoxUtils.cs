using System.Windows;

namespace GeoUsersUI.Utils
{
    public static class MessageBoxUtils
    {
        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
        }

        public static void FormIncompleteError()
        {
            Error("Hay campos obligatorios incompletos.");
        }

        public static MessageBoxResult Confirmation(string confirmationMessage)
        {
            return MessageBox.Show(confirmationMessage, "Confirmar", MessageBoxButton.YesNo);
        }
    }
}

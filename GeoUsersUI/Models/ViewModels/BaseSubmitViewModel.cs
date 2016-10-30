using GeoUsers.Services;
using GeoUsersUI.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseSubmitViewModel<T>
    {
        public T Result { get; set; }

        /// <summary>
        /// Validates wether the function should execute or not.
        /// </summary>
        /// <param name="submitFunction"></param>
        /// <returns></returns>
        protected Func<bool> SubmitValidation { get; set; }

        /// <summary>
        /// Executes when the form is submitted.
        /// </summary>
        /// <param name="submitFunction"></param>
        /// <returns></returns>
        protected Func<T> SubmitFunction { get; set; }

        /// <summary>
        /// Runs the submit function in background catching common exceptions.
        /// </summary>
        /// <param name="submitFunction"></param>
        /// <returns></returns>
        public Task<bool> Submit()
        {
            // TODO. implement notifypropertychanged together with a loading property to notify when the loading starts and finishes.
            if (!SubmitValidation())
            {
                MessageBoxExtensions.ShowFormIncompleteError();
                return Task.FromResult(false);
            }

            return Task.Run(() =>
            {
                try
                {
                    using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                    {
                        SubmitFunction();
                    }

                    return true;
                }
                catch (InvalidOperationException e)
                {
                    Console.Write(e);
                    // TODO. Logger implementation. Log exceptions.
                    MessageBox.Show("Operacion invalida");
                    return false;
                }
            });
        }
    }
}

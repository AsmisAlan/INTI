using GeoUsers.Services;
using GeoUsersUI.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseSubmitViewModel<T> : BaseWindowViewModel
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
                MessageBoxUtils.FormIncompleteError();
                return Task.FromResult(false);
            }

            try
            {
                return RequestService.Execute(() => SubmitFunction());
            }
            catch (InvalidOperationException e)
            {
                Console.Write(e);
                // TODO. Logger implementation. Log exceptions.
                MessageBox.Show("Operacion invalida");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return Task.FromResult(false);
        }
    }
}

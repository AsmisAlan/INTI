using GeoUsersUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseSubmitViewModel<T> : BaseWindowViewModel
    {
        protected bool loading;

        protected bool isReadonly;

        protected readonly IList<ValidationError> Errors;

        public BaseSubmitViewModel()
        {
            IsReadonly = !App.IsUserAuthenticated;

            Errors = new List<ValidationError>();
        }

        public bool Loading
        {
            get
            {
                return loading;
            }
            set
            {
                loading = value;

                OnPropertyChanged(nameof(Loading));
            }
        }

        public bool IsReadonly
        {
            get
            {
                return isReadonly;
            }
            set
            {
                isReadonly = value;

                OnPropertyChanged(nameof(IsReadonly));
            }
        }

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
        public async Task<bool> Submit()
        {
            var error = Errors.FirstOrDefault();

            if (error != null)
            {
                MessageBoxUtils.Error((string)error.ErrorContent);

                return false;
            }

            if (!SubmitValidation())
            {
                MessageBoxUtils.FormIncompleteError();

                return false;
            }

            try
            {
                Loading = true;

                var result = await RequestService.Execute(() => SubmitFunction());

                Loading = false;

                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                Loading = false;
            }

            return true;
        }
    }
}

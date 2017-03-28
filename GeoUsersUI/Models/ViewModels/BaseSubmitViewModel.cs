﻿using GeoUsersUI.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseSubmitViewModel<T> : BaseWindowViewModel
    {
        protected bool loading;

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

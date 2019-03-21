﻿namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductsItemViewModel:Products
    {
        #region Commands
        public ICommand SelectProductCommand
        {
            get
            {
                return new RelayCommand(SelectProduct);
            }
        }

        private  void SelectProduct()
        {
            
        }
        #endregion
    }
}
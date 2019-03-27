using GalaSoft.MvvmLight.Command;
using RQTools.Models;
using RQTools.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RQTools.ViewModels
{
   public class InventarioModelViewModel:InventarioModel
    {
        #region Commands
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }
        #endregion
        #region Methods
        private async void Edit()
        {
            MainViewModel.GetInstance().EditOrEliminate = new EditOrEliminateViewModel(this);
            await App.Navigator.PushAsync(new EditOrEliminatePage());
        }
        #endregion
    }
}

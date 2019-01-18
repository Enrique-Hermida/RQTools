namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    class PrincipalViewModel:BaseViewModel
    {
        #region Comandos
        public ICommand Shospiltalcommand
        {
            get
            {
                return new RelayCommand(Shospital);
            }
        }

        private async void Shospital()
        {
            await Application.Current.MainPage.DisplayAlert(
                    "Bien",
                    "Si funciona este desvergue xd",
                    "Aceptar");
            return;
        }

        #endregion
    }
}

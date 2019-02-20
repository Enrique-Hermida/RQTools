namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Views;
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
            MainViewModel.GetInstance().Hospital = new HospitalViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HospitalPage());
        }

        #endregion
    }
}

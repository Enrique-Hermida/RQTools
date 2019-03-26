namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HospitalItemViewModel : HospitalModel
    {
        #region Commands
        public ICommand SelectHospitalCommand
        {
            get
            {
                return new RelayCommand(SelectHospital);
            }
        }

        private async void SelectHospital()
        {
            MainViewModel.GetInstance().HospitalActual = this;
            MainViewModel.GetInstance().Hospital = new HospitalViewModel();
            await App.Navigator.PushAsync(new HospitalPage());
        }
        #endregion
    }
}

namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    class PrincipalViewModel:BaseViewModel
    {
        #region Atributos
        private string user;
        #endregion

        #region Properties   
        public string User {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        #endregion
        #region Constructores
        public PrincipalViewModel(DeviceUser deviceUser)
        {
            this.User = deviceUser.Name_User;
        }
        #endregion
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

namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PrincipalViewModel:BaseViewModel
    {
        #region Atributos
        private string user;

        #endregion

        #region Properties   
        public string User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public HospitalModel Hospital { get; set; }
        public DeviceUser LocalUser { get; set; }
        #endregion
        #region Constructores
        public PrincipalViewModel()
        {
            this.LocalUser = MainViewModel.GetInstance().deviceUser;
            this.User = LocalUser.Name_User;
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
            Hospital = new HospitalModel();
            Hospital.Nombre_Hospital = "Hospital no seleccionado";
            Hospital.Activo = 0;
            Hospital.Codigo_Hospital = "n/a";
            Hospital.ID_Hospital = 0;

            MainViewModel.GetInstance().Hospital = new HospitalViewModel(Hospital);
            await Application.Current.MainPage.Navigation.PushAsync(new HospitalPage());
        }

        #endregion
    }
}

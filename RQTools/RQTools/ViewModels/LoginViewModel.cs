namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string password;
        private string email;
        private bool isRunning;
        private bool isEnabled;
        #endregion
        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion
        #region Constructores
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.isEnabled = true;
        }
        #endregion
        #region Comandos
        public ICommand LoginCommand {
            get
            {
                return new RelayCommand(Login);
            }
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar tu Email",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar tu Contraseña",
                    "Aceptar");
                return;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new PrincipalPage());
        }
        #endregion
    }
}

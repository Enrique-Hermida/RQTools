namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using RQTools.Models;
    using RQTools.Views;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Windows.Input;
   
    using Xamarin.Forms;

    class LoginViewModel : BaseViewModel
    {

        #region Atributos
        private string IPLocal = "http://192.168.1.38:80";
        private string url;
        private string password;
        private string email;
        private string result;
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

            this.IsRunning = true;
            this.IsEnabled = false;

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(IPLocal);
                url = string.Format("/api/get/login/ID_User-Name_User-Correo-Password-User_Type/?w=Correo:%27{0}%27 AND Password:%27{1}%27", this.email, this.password);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(result))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No hay respuesta en el servido, intenta mas tarde",
                        "Aceptar");

                    this.IsRunning = false;
                    this.IsEnabled = true;
                    return;
                }
            } catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        string.Format("Ocurrio el Error :{0},",ex.Message),
                        "Aceptar");

                this.IsRunning = false;
                this.IsEnabled = true;

                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            if (result.Contains("errors"))
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Usuario o Contraseña incorrecta",
                        "Aceptar");
                return;
            }
            var deviceUser = JsonConvert.DeserializeObject<DeviceUser>(result);



            MainViewModel.GetInstance().Principal = new PrincipalViewModel(deviceUser);
            await Application.Current.MainPage.Navigation.PushAsync(new PrincipalPage());
        }
        #endregion
    }
}

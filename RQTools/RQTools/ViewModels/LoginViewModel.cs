namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using RQTools.Helpers;
    using RQTools.Models;
    using RQTools.Views;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Windows.Input;
    using Services;
    using Xamarin.Forms;
    using System.Threading.Tasks;
    using Android;
    
    using Android.Content.PM;

    class LoginViewModel : BaseViewModel
    {
        #region Services
        private DataServices dataService;
        #endregion
        #region Atributos
        private string ryqdns = "http://ryqmty.dyndns.org:8181";
        private string url;
        private string password;
        private string email;
        private string result;
        private bool isRunning;
        private bool isEnabled;
        private DeviceUser deviceUser;
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
            this.dataService = new DataServices();
            if (Settings.IsRemembered == "true")
            {
                CargarUsuario();             
            }
            this.isEnabled = true;

        }
        #endregion
        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        #endregion

        #region Metodos
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
                client.BaseAddress = new Uri(ryqdns);
                url = string.Format("/apiRest/public/api/deviceuser/{0}/{1}", this.email, this.password);
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
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        string.Format("Ocurrio el Error :{0},", ex.Message),
                        "Aceptar");

                this.IsRunning = false;
                this.IsEnabled = true;

                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            if (result.Contains("error"))
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Usuario o Contraseña incorrecta",
                        "Aceptar");
                return;
            }
            if (result.Contains("Name_User"))
            {
                var listdeviceUser = JsonConvert.DeserializeObject<List<DeviceUser>>(result);
                deviceUser = listdeviceUser[0];
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.deviceUser = deviceUser;

                if (this.IsRemembered)
                {
                    Settings.IsRemembered = "true";
                    DeleteandInsertUser();
                }
                else
                {
                    Settings.IsRemembered = "false";
                }
                mainViewModel.Principal = new PrincipalViewModel();
                Application.Current.MainPage = new MasterPage();
                this.Password = string.Empty;

            }

        }

        private async Task DeleteandInsertUser()
        {
            await this.dataService.DelleteAllUsers();
            this.dataService.Insert(deviceUser);
        }

        private async Task CargarUsuario()
        {
            var mainViewModel = MainViewModel.GetInstance();
            var listusers = await this.dataService.GetAllUsers();
            deviceUser = listusers[0];
            mainViewModel.deviceUser = deviceUser;
            mainViewModel.Principal = new PrincipalViewModel();
            // await Application.Current.MainPage.Navigation.PushAsync(new PrincipalPage());
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
        #region Permisos



        #endregion
    }
}

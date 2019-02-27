namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using RQTools.Helpers;
    using RQTools.Models;
    using RQTools.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Windows.Input;
   
    using Xamarin.Forms;

    class LoginViewModel : BaseViewModel
    {

        #region Atributos
        private string IPLocal = "http://192.168.15.14:80";
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
            this.Email = "enriqueh.cehd@gmail.com";
            this.Password = "1234";
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
                var deviceUser = listdeviceUser[0];
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.deviceUser = deviceUser;

                if (this.IsRemembered)
                {
                    Settings.IsRemembered = "true";
                    using (var datos = new DataAccess())
                    {
                        datos.InsertDeviceUser(deviceUser);
                    }
                }
                else
                {
                    Settings.IsRemembered = "false";
                }


                mainViewModel.Principal = new PrincipalViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new PrincipalPage());
            }
           
        }
        #endregion
    }
}

﻿namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Services;
    using RQTools.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PrincipalViewModel:BaseViewModel
    {
        #region Services
        private DataServices dataService;
        #endregion
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
            this.dataService = new DataServices();
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
        
        #endregion
        #region Methods
        private async void Shospital()
        {
            #region Modelo Hospital Vacio
            Hospital = new HospitalModel();
            Hospital.Nombre_Hospital = "Hospital no seleccionado";
            Hospital.Activo = 0;
            Hospital.Codigo_Hospital = "n/a";
            Hospital.ID_Hospital = 0;
            #endregion
            MainViewModel.GetInstance().HospitalActual = Hospital;
            MainViewModel.GetInstance().Hospital = new HospitalViewModel();
            await App.Navigator.PushAsync(new HospitalPage());
        }
        #endregion
    }
}

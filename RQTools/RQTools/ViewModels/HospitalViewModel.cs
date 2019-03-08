namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HospitalViewModel : BaseViewModel
    {
        #region Atributos
        private string hospitalseleccionado;
        private string motivonocamara;
        private bool validacionhospital;
        #endregion

        #region Propiedades
        public string HospitalSeleccionado
        {
            get{ return this.hospitalseleccionado; }
            set { SetValue(ref this.hospitalseleccionado, value); }
        }
        public string MotivoNoCamara
        {
            get { return this.motivonocamara; }
            set { SetValue(ref this.motivonocamara, value); }
        }
        public bool ValidacionHospital
        {
            get { return this.validacionhospital; }
            set { SetValue(ref this.validacionhospital, value); }
        }
        public HospitalModel Hospital { get; set; }

        #endregion

        #region Constructors

        public HospitalViewModel(HospitalModel hospital)
        {
            this.Hospital = hospital;
            this.ValidacionHospital = false;
            this.HospitalSeleccionado = Hospital.Nombre_Hospital;
        }
        #endregion
        #region Comandos
        public ICommand InciarInventariocommand
        {
            get
            {
                return new RelayCommand(IniciarInventario);
            }
        }

        private async void IniciarInventario()
        {
            if (ValidacionHospital==true) {

                await Application.Current.MainPage.DisplayAlert(
                           "SALU2",
                           "AVER AL CINE PVTO",
                           "OKTL");
                return;
            }
            await Application.Current.MainPage.DisplayAlert(
                          "Error",
                          "Selecciona un Hospital",
                          "Aceptar");
            return;
        }
        public ICommand BuscarHospitalCommand
        {
            get
            {
                return new RelayCommand(BuscarHospital);
            }
        }

        private async void BuscarHospital()
        {
            if (string.IsNullOrEmpty(this.MotivoNoCamara))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar el Motivo de porque no usas la camara",
                    "OK :(");
                return;
            }
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.HospitalList = new HospitalListViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HospitalListPage());
        }

        #endregion
    }
}

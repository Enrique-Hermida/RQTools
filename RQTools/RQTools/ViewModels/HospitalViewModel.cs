namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using RQTools.Interface;
    using RQTools.Models;
    using RQTools.Services;
    using RQTools.Views;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HospitalViewModel : BaseViewModel
    {
        #region Services
        private DataServices dataService;
        #endregion
        #region Atributos
        private string ryqdns = "http://ryqmty.dyndns.org:8181";
        private string url;
        private string result;
        private string hospitalseleccionado;
        private string motivonocamara;
        private string barCode;
        private string scanResult;
        private bool validacionhospital;
        private bool isRunning;
        private bool isEnabled;
        private HospitalModel scanHospital;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        #endregion

        #region Propiedades
        public string Barcode
        {
            get { return this.barCode; }
            set { SetValue(ref this.barCode, value); }
        }
        public string ScanResult
        {
            get { return this.scanResult; }
            set { SetValue(ref this.scanResult, value); }
        }
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
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public HospitalModel Hospital { get; set; }
        #endregion

        #region Constructors
        public HospitalViewModel()
        {
            this.dataService = new DataServices();
            this.Hospital = mainViewModel.HospitalActual;
            this.HospitalSeleccionado = Hospital.Nombre_Hospital;
            if (Hospital.ID_Hospital==0)
            {
                //obvi que es porque no hay hospital seleccionado
                this.ValidacionHospital = false;
            }
            if (Hospital.ID_Hospital != 0)
            {
                //obvi que es porque si hay hospital seleccionado
                this.ValidacionHospital = true;
            }
            
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
        
        public ICommand BuscarHospitalCommand
        {
            get
            {
                return new RelayCommand(BuscarHospital);
            }
        }

        public ICommand ScanCommand
        {
            get
            {
                return new RelayCommand(Scan);
            }
        }

        #endregion
        #region Methods

        private async void BuscarHospital()
        {
            if (string.IsNullOrEmpty(this.MotivoNoCamara))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar el Motivo de porque no usas la camara",
                    "Aceptar");
                return;
            }
            mainViewModel.HospitalList = new HospitalListViewModel();
            await App.Navigator.PushAsync(new HospitalListPage());
        }

        private async void IniciarInventario()
        {
            if (ValidacionHospital == true)
            {
                mainViewModel.Inventario = new InventarioViewModel();
                await App.Navigator.PushAsync(new InventarioTabbedPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                          "Error",
                          "Selecciona un Hospital",
                          "Aceptar");
                return;
            }

        }

        private async void Scan()
        {
            Barcode = await ScannerSKU(); 
        }

        private async void FindScanHospital()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ryqdns);
                url = string.Format("/apiRest/public/api/hospital/{0}",this.ScanResult);
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
                        "Codigo inexitente",
                        "Aceptar");
                return;
            }
            if (result.Contains("ID_Hospital"))
            {
                var listhospitals = JsonConvert.DeserializeObject<List<HospitalModel>>(result);
                scanHospital = listhospitals[0];
                this.HospitalSeleccionado = scanHospital.Nombre_Hospital.ToString();
                mainViewModel.HospitalActual = scanHospital;
                this.ValidacionHospital = true;

            }
        }
        #endregion

        #region Scanner
        public async Task<string> ScannerSKU()
        {
            try
            {
                var scanner = DependencyService.Get<IQrCodeScanningService>();
                this.ScanResult = await scanner.ScanAsync();
                this.ScanResult = ScanResult.ToString();
                this.FindScanHospital();
                return ScanResult;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return string.Empty;
            }
        }

        #endregion
    }
}

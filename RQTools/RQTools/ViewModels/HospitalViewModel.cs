namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Interface;
    using RQTools.Models;
    using RQTools.Views;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class HospitalViewModel : BaseViewModel
    {
        #region Atributos
        private string hospitalseleccionado;
        private string motivonocamara;
        private bool validacionhospital;
        private string barCode;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        #endregion

        #region Propiedades
        public string Barcode
        {
            get { return this.barCode; }
            set { SetValue(ref this.barCode, value); }
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
        public HospitalModel Hospital { get; set; }

        #endregion

        #region Constructors

        public HospitalViewModel(HospitalModel hospital)
        {
            this.Hospital = hospital;
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
            await Application.Current.MainPage.Navigation.PushAsync(new HospitalListPage());
        }

        private async void IniciarInventario()
        {
            if (ValidacionHospital == true)
            {
                mainViewModel.Inventario = new InventarioViewModel(Hospital);
                await Application.Current.MainPage.Navigation.PushAsync(new InventarioTabbedPage());
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
        #endregion
        #region Scanner
        public async Task<string> ScannerSKU()
        {
            try
            {
                var scanner = DependencyService.Get<IQrCodeScanningService>();
                var result = await scanner.ScanAsync();
                return result.ToString();
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

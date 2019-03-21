namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using RQTools.Interface;
    using RQTools.Models;
    using RQTools.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class InventarioViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Atributtes
        private string ryqdns = "http://ryqmty.dyndns.org:8181";
        private string url;
        private string result;
        private string filter;
        private string barCode;
        private string scanResult;
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;
        private Products scanProduct;
        private ObservableCollection<ProductsItemViewModel> productsnoCode;
        #endregion
        #region Properties
        public HospitalModel Hospital { get; set; }
        public ObservableCollection<ProductsItemViewModel> ProductsNocode
        {
            get { return this.productsnoCode; }
            set { SetValue(ref this.productsnoCode, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
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
        #endregion
        #region Constructors
        public InventarioViewModel(HospitalModel hospital)
        {
            this.apiService = new ApiService();
            this.Hospital = hospital;
            this.LoadProductsNoCode();
        }
        #endregion
        #region Methods
        private async void LoadProductsNoCode()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                     "No hay Internet",
                    "aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Products>(
               "http://ryqmty.dyndns.org:8181",
                "/apiRest",
                "/public/api/productos/NoCode");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                     "Error en el servidor",
                    "aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No hay respuesta en el servido, intenta mas tarde",
                    "Aceptar");
                return;
            }

            MainViewModel.GetInstance().ProductsNoCode = (List<Products>)response.Result;
            this.ProductsNocode = new ObservableCollection<ProductsItemViewModel>(
                this.ToProductsNoCodeItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<ProductsItemViewModel> ToProductsNoCodeItemViewModel()
        {
            return MainViewModel.GetInstance().ProductsNoCode.Select(p => new ProductsItemViewModel
            {
                ID_Producto = p.ID_Producto,
                Nombre_Producto = p.Nombre_Producto,
                Clave = p.Clave,
                Tipo_Producto = p.Tipo_Producto,
                Scanbar = p.Scanbar,
                Fecha_Modificacion = p.Fecha_Modificacion,
            });
        }
        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.ProductsNocode = new ObservableCollection<ProductsItemViewModel>(
                    this.ToProductsNoCodeItemViewModel());
            }
            else
            {
                this.ProductsNocode = new ObservableCollection<ProductsItemViewModel>(
                    this.ToProductsNoCodeItemViewModel().Where(
                        p => p.Nombre_Producto.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        private async void Scan()
        {
            Barcode = await ScannerSKU();
        }
        private async void FindScanProdcuct()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ryqdns);
                url = string.Format("/apiRest/public/api/products/{0}", this.ScanResult);
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
                var listProducts = JsonConvert.DeserializeObject<List<Products>>(result);
                scanProduct = listProducts[0];
                

            }
        }
        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProductsNoCode);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
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
        #region Scanner
        public async Task<string> ScannerSKU()
        {
            try
            {
                var scanner = DependencyService.Get<IQrCodeScanningService>();
                this.ScanResult = await scanner.ScanAsync();
                this.ScanResult = ScanResult.ToString();
                this.FindScanProdcuct();
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

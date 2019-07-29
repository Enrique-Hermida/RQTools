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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class InventarioViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataServices dataService;
        #endregion
        #region Atributtes
        private string ryqdns = "http://ryqmty.dyndns.org:8181";
        private string tempcode128 = "";
        private string url;
        private string result;
        private string filter;
        private string barCode;
        private string scanResult;
        private string hospitalseleccionado;
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        private Products scanProduct;
        private ObservableCollection<ProductsItemViewModel> productsnoCode;
        private ObservableCollection<InventarioModelViewModel> inventarioActual;
        #endregion
        #region Properties
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        public string HospitalSeleccionado
        {
            get { return this.hospitalseleccionado; }
            set { SetValue(ref this.hospitalseleccionado, value); }
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
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
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
        public HospitalModel Hospital
        {
            get;
            set;
        }
        public ObservableCollection<ProductsItemViewModel> ProductsNocode
        {
            get { return this.productsnoCode; }
            set { SetValue(ref this.productsnoCode, value); }
        }
        public ObservableCollection<InventarioModelViewModel> InventarioActual
        {
            get { return this.inventarioActual; }
            set { SetValue(ref this.inventarioActual, value); }
        }

        #endregion
        #region Constructors
        public InventarioViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataServices();
            this.Hospital = mainViewModel.HospitalActual;
            this.HospitalSeleccionado = Hospital.Nombre_Hospital;
            this.CheckDataViewModel();
            
            this.InventarioActual = new ObservableCollection<InventarioModelViewModel>();
            this.InventarioActual = mainViewModel.InventarioActualMWM;
        }
        #endregion
        #region Methods
        private void CheckDataViewModel()
        {
            if (mainViewModel.ProductsNoCode.Count == 0)
            {
                this.LoadProductsNoCode();
            }
            else
            {
                this.isRefreshing = true;
                this.ProductsNocode = new ObservableCollection<ProductsItemViewModel>(
                this.ToProductsNoCodeItemViewModel());
                this.IsRefreshing = false;
            }

        }
        private async void LoadProductsNoCode()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            var listproducts = await this.dataService.GetallProducts();

            if (!connection.IsSuccess && listproducts.Count==0 )
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                     "No hay Internet , No hay datos en BD",
                    "aceptar");

                mainViewModel.Hospital = new HospitalViewModel();
                await App.Navigator.PushAsync(new HospitalPage());

                return;
            }
            if (connection.IsSuccess)
            {
                this.LoadDataFromAPI();
                this.IsRefreshing = false;
                this.IsEnabled = true;
            }
            else
            {
                this.LoadDataFromDB();
                this.IsRefreshing = false;
                this.IsEnabled = true;
            }

        }

        private async Task LoadDataFromDB()
        {
            var listproducts = await this.dataService.GetallProducts();

            if (listproducts.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Sin Datos en BD",
                     "aceptar");

                mainViewModel.Hospital = new HospitalViewModel();
                await App.Navigator.PushAsync(new HospitalPage());

                this.IsRefreshing = false;
                this.IsEnabled = true;

                return;
            }

            mainViewModel.ProductsNoCode = listproducts;
            this.ProductsNocode = new ObservableCollection<ProductsItemViewModel>(
                this.ToProductsNoCodeItemViewModel());
            
        }

        private async void LoadDataFromAPI()
        {
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

                mainViewModel.Hospital = new HospitalViewModel();
                await App.Navigator.PushAsync(new HospitalPage());

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

            mainViewModel.ProductsNoCode = (List<Products>)response.Result;
            this.ProductsNocode = new ObservableCollection<ProductsItemViewModel>(
                this.ToProductsNoCodeItemViewModel());
            this.DeleteAndInsertProducts();
            
        }

        private async Task DeleteAndInsertProducts()
        {
            await this.dataService.DelleteAllProducts();
            this.dataService.Insert(mainViewModel.ProductsNoCode);
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
            var connection = await this.apiService.CheckConnection();
            var listproducts = await this.dataService.GetallProducts();

            if (!connection.IsSuccess && listproducts.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                     "No hay Internet , No hay datos en BD",
                    "aceptar");

                mainViewModel.Hospital = new HospitalViewModel();
                await App.Navigator.PushAsync(new HospitalPage());

                return;
            }
            if (connection.IsSuccess)
            {
                this.FindCodeFromAPI();
                this.IsRefreshing = false;
                this.IsEnabled = true;
            }
            else
            {
                this.FindCodeFromDB();
                this.IsRefreshing = false;
                this.IsEnabled = true;
            }
           
        }

        private async Task FindCodeFromDB()
        {
            this.LoadDataFromDB();
            if (mainViewModel.ProductsNoCode.Exists(x => x.Scanbar == this.ScanResult))
            {
                var tempproduct = this.ProductsNocode.SingleOrDefault(r => r.Scanbar == this.ScanResult);
                scanProduct = tempproduct;
                if (!string.IsNullOrEmpty(this.tempcode128))
                {
                    scanProduct.Scanbar = tempcode128;
                }
                mainViewModel.AddProduct = new AddProductViewModel(scanProduct);
                await App.Navigator.PushAsync(new AddInventarioPage());
            }
        }

        private async void FindCodeFromAPI()
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

                    this.IsRefreshing = false;
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

                this.IsRefreshing = false;
                this.IsEnabled = true;

                return;
            }
            this.IsRefreshing = false;
            this.IsEnabled = true;

            if (result.Contains("error"))
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Codigo inexitente",
                        "Aceptar");
                return;
            }
            if (result.Contains("ID_Producto"))
            {
                var listProducts = JsonConvert.DeserializeObject<List<Products>>(result);
                scanProduct = listProducts[0];
                if (!string.IsNullOrEmpty(this.tempcode128))
                {
                    scanProduct.Scanbar = tempcode128;
                }
                mainViewModel.AddProduct = new AddProductViewModel(scanProduct);
                await App.Navigator.PushAsync(new AddInventarioPage());

            }
        }

        private async void GoToFinal()
        {
            if (mainViewModel.InventarioActualMWM.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No tienes Articulos en tu lista",
                        "Aceptar");
                return;
            }
            MainViewModel.GetInstance().InventarioFinal = new InventarioFinalViewModel();
            await App.Navigator.PushAsync(new InventarioFinalPage());
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
        public ICommand GoToFinalCommand
        {
            get
            {
                return new RelayCommand(GoToFinal);
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

                if (ScanResult.Equals("0"))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No se acepta el valor",
                        "Aceptar");
                    return ScanResult;
                }
                else
                {
                    // hay que separar el codigo en secciones 
                    tempcode128 = this.ScanResult;
                    string cproducto = "";
                    char[] codsc = this.ScanResult.ToCharArray();
                    string Pef1 = codsc[0].ToString() + codsc[1].ToString(); 
                    if (Pef1.Equals("01"))
                    {
                        for (int i = 2; i <= 15; i++)
                        {
                            cproducto = cproducto + codsc[i].ToString();                       
                        }
                        this.ScanResult = cproducto;
                    }
                    
                    this.FindScanProdcuct();
                }
                
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

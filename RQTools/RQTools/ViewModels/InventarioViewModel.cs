namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class InventarioViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Atributtes
        private ObservableCollection<ProductsItemViewModel> productsnoCode;
        private bool isRefreshing;
        private string filter;
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
        #endregion
    }
}

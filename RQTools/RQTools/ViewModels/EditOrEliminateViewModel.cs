namespace RQTools.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using RQTools.Models;
    using RQTools.Views;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditOrEliminateViewModel : BaseViewModel
    {
        #region Atributtes
        private int cantidad;
        private int idlist;
        private string lote;
        private string nombreProducto;
        private bool isEnabled;
        private MainViewModel mainViewModel = MainViewModel.GetInstance();
        #endregion
        #region Properties
        public InventarioModel ItemActual
        {
            get;
            set;
        }
        public int Cantidad
        {
            get { return this.cantidad; }
            set { SetValue(ref this.cantidad, value); }
        }
        public int IdList
        {
            get { return this.idlist; }
            set { SetValue(ref this.idlist, value); }
        }
        public string Lote
        {
            get { return this.lote; }
            set { SetValue(ref this.lote, value); }
        }
        public string NombreProducto
        {
            get { return this.nombreProducto; }
            set { SetValue(ref this.nombreProducto, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion
        #region Contructors
        public EditOrEliminateViewModel(InventarioModel itemList)
        {
            this.ItemActual = new InventarioModel();
            this.ItemActual = itemList;
            this.IdList = ItemActual.Id;
            this.NombreProducto = ItemActual.Producto;
            this.Lote = ItemActual.Lote;
            this.Cantidad = ItemActual.Cantidad;

        }
        #endregion
        #region Commands
        public ICommand EliminateCommand
        {
            get
            {
                return new RelayCommand(Eliminate);
            }
        }
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }
        #endregion
        #region Methods
        private void Edit()
        {
            this.ValidateProduct();
        }

        private async void ValidateProduct()
        {
            if (string.IsNullOrEmpty(this.Lote))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar el lote del producto seleccionado",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Cantidad.ToString()))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar la cantidad del producto seleccionado",
                    "Aceptar");
                return;
            }
            if (this.Cantidad <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar cantidades mayores a 0",
                    "Aceptar");
                return;
            }
            if (this.Cantidad > 8000)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    "Estas Ingresando Cantidades Mayores a 8000 ,Estas seguro",
                    "Aceptar");
            }
            if (this.Cantidad > 4000)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    "Estas Ingresando Cantidades Mayores a 4000 pza, estas seguro ",
                    "Aceptar");

            }
            if (this.ItemActual.Id_Producto < 24)
            {
                //caja de cualquier producto
                if (this.ItemActual.Id_Producto <= 9 && this.Cantidad > 300)
                {
                    await Application.Current.MainPage.DisplayAlert(
                              "Error",
                              "Estas Ingresando Cantidades Mayores a 300 Cajas ",
                              "Aceptar");
                    return;
                }
                if (this.ItemActual.Id_Producto == 14 && this.Cantidad > 222)
                {
                    await Application.Current.MainPage.DisplayAlert(
                              "Error",
                              "Estas Ingresando Cantidades Mayores a 222 Cajas ",
                              "Aceptar");
                    return;
                }
                if (ItemActual.Id_Producto >= 18 && ItemActual.Id_Producto <= 20 && this.Cantidad > 88)
                {
                    await Application.Current.MainPage.DisplayAlert(
                              "Error",
                              "Estas Ingresando Cantidades Mayores a 88 Cajas ",
                              "Aceptar");
                    return;
                }
            }
            #region Eliminar modelo anterior
            var itemtoremove = mainViewModel.InventarioActualMWM.SingleOrDefault(r => r.Id == ItemActual.Id);
            mainViewModel.InventarioActualMWM.Remove(itemtoremove);
            #endregion
            #region construir el nuevo modelo
            mainViewModel.InventarioActualMWM.Add(new InventarioModelViewModel
            {
                Id = this.ItemActual.Id,
                Producto = this.ItemActual.Producto,
                Id_Producto = this.ItemActual.Id_Producto,
                Cantidad = this.Cantidad,
                Lote = this.Lote,
            });
            #endregion
            mainViewModel.Inventario = new InventarioViewModel();
            await App.Navigator.PushAsync(new InventarioTabbedPage());
        }

        private async void Eliminate()
        {
            var itemtoremove =mainViewModel.InventarioActualMWM.SingleOrDefault(r => r.Id == ItemActual.Id);
            mainViewModel.InventarioActualMWM.Remove(itemtoremove);
            mainViewModel.Inventario = new InventarioViewModel();
            await App.Navigator.PushAsync(new InventarioTabbedPage());
        }
        #endregion
    }
}

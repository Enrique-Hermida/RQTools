namespace RQTools.ViewModels
{
    using RQTools.Models;
    using RQTools.Services;
    class AddProductViewModel : BaseViewModel
    {
        #region Services
        private DataServices dataService;
        #endregion
        #region Attributes
        private int cantidad;
        private string lote;
        private string nombreProducto;
        private Products product;
        #endregion
        #region Properties
        public int Cantidad
        {
            get { return this.cantidad; }
            set { SetValue(ref this.cantidad, value); }
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
        public Products Product
        {
            get { return this.product; }
            set { SetValue(ref this.product, value); }
        }
        #endregion
        #region Constructors
        public AddProductViewModel(Products products)
        {
            this.NombreProducto = "si jala";
            this.Product = products;

        }
        #endregion
    }
}

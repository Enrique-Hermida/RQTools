namespace RQTools.ViewModels
{
    using RQTools.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class EditOrEliminateViewModel : BaseViewModel
    {
        #region Properties
        public InventarioModel ItemActual
        {
            get;
            set;
        }
        #endregion
        #region Contructors
        public EditOrEliminateViewModel(InventarioModel itemList)
        {
            this.ItemActual = new InventarioModel();
            this.ItemActual = itemList;

        }
        #endregion
    }
}

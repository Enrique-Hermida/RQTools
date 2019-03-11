namespace RQTools.ViewModels
{
    using RQTools.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InventarioViewModel
    {
        #region Properties
        public HospitalModel Hospital { get; set; }
        #endregion
        #region Constructors
        public InventarioViewModel(HospitalModel hospital)
        {
            this.Hospital = hospital;
        }
        #endregion
    }
}

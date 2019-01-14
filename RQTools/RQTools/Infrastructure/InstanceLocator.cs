using System;
using System.Collections.Generic;
using System.Text;

namespace RQTools.Infrastructure
{
    using ViewModels;
    class InstanceLocator
    {
        #region Propreties
        public MainViewModel Main { get; set; }
        #endregion
        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}

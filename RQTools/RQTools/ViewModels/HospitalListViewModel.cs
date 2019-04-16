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

    public class HospitalListViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataServices dataService;
        #endregion
        #region Atributtes
        private ObservableCollection<HospitalItemViewModel> hospital;
        private bool isRefreshing;
        private string filter;
        #endregion
        #region Properties
        public ObservableCollection<HospitalItemViewModel> Hospital
        {
            get { return this.hospital; }
            set { SetValue(ref this.hospital, value); }
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
        public HospitalListViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataServices();
            this.LoadHospitals();
        }
        #endregion
        #region Methods
        private async void LoadHospitals()
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

            var response = await this.apiService.GetList<HospitalModel>(
               "http://ryqmty.dyndns.org:8181",
                "/apiRest",
                "/public/api/hospitales");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                     "Error en el servidor",
                    "aceptar");
                await App.Navigator.PopAsync();
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

            MainViewModel.GetInstance().HospitalListlist = (List<HospitalModel>)response.Result;
            this.Hospital = new ObservableCollection<HospitalItemViewModel>(
                this.ToHospitalItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<HospitalItemViewModel> ToHospitalItemViewModel()
        {
            return MainViewModel.GetInstance().HospitalListlist.Select(h => new HospitalItemViewModel
            {
                ID_Hospital = h.ID_Hospital,
                Nombre_Hospital = h.Nombre_Hospital,
                Codigo_Hospital = h.Codigo_Hospital,
                Activo = h.Activo,
            });
        }

        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadHospitals);
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
                this.Hospital = new ObservableCollection<HospitalItemViewModel>(
                    this.ToHospitalItemViewModel());
            }
            else
            {
                this.Hospital = new ObservableCollection<HospitalItemViewModel>(
                    this.ToHospitalItemViewModel().Where(
                        h => h.Nombre_Hospital.ToLower().Contains(this.Filter.ToLower()) ||
                             h.Codigo_Hospital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}

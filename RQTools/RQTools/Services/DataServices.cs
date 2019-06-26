namespace RQTools.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RQTools.Interface;
    using RQTools.Models;
    using SQLite;
    using System.Linq;
    using Xamarin.Forms;

    public class DataServices
    {
        private SQLiteAsyncConnection connection;

        public DataServices()
        {
            this.OpenOrCreateDB();
        }

        private async Task OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<DeviceUser>().ConfigureAwait(false);
            await connection.CreateTableAsync<HospitalModel>().ConfigureAwait(false);
            await connection.CreateTableAsync<Products>().ConfigureAwait(false);
        }

        #region Modelo Usuario
        public async Task<List<DeviceUser>> GetAllUsers()
        {
            var query = await this.connection.QueryAsync<DeviceUser>("select * from [DeviceUser]");
            var array = query.ToArray();
            var list = array.Select(u => new DeviceUser
            {
                ID_User = u.ID_User,
                Name_User = u.Name_User,
                Correo = u.Correo,
                Password = u.Password,
                User_Type = u.User_Type
            }).ToList();
            return list;
        }
        public async Task DelleteAllUsers()
        {
            var query = await this.connection.QueryAsync<DeviceUser>("delete from [DeviceUser]");
        }
        #endregion
        #region Modelo Hospitales
        public async Task<List<HospitalModel>> GetAllHospitals()
        {
            var query = await this.connection.QueryAsync<HospitalModel>("select * from [HospitalModel]");
            var array = query.ToArray();
            var list = array.Select(h => new HospitalModel
            {
                ID_Hospital = h.ID_Hospital,
                Nombre_Hospital=h.Nombre_Hospital,
                Codigo_Hospital=h.Codigo_Hospital,
                Activo=h.Activo
            }).ToList();
            return list;
        }
        public async Task DelleteAllHospitals()
        {
            var query = await this.connection.QueryAsync<HospitalModel>("delete from [HospitalModel]");
        }
        #endregion
        #region Modelo Productos
        public async Task<List<Products>> GetallProducts()
        {
            var query = await this.connection.QueryAsync<Products>("select * from [Products]");
            var array = query.ToArray();
            var list = array.Select(p => new Products
            {
                ID_Producto = p.ID_Producto,
                Nombre_Producto = p.Nombre_Producto,
                Clave = p.Clave,
                Tipo_Producto = p.Tipo_Producto,
                Scanbar = p.Scanbar,
                Fecha_Modificacion = p.Fecha_Modificacion, 
            }).ToList();
            return list;
        }
        public async Task DelleteAllProducts()
        {
            var query = await this.connection.QueryAsync<Products>("delete from [Products]");
        }
        #endregion
        #region Public Methods
        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }
        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }
        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }
        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        } 
        #endregion

    }
}

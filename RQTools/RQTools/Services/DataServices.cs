namespace RQTools.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
            var databasePath = DependencyService.Get<IPathService>.GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<DeviceUser>().ConfigureAwait(false);
        }
        public async Task  Insert<T>(T model)
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
        public async Task DelleteAllUsers()
        {
            var query = await this.connection.QueryAsync<DeviceUser>("delete from [DeviceUser]");
        }
    }
}

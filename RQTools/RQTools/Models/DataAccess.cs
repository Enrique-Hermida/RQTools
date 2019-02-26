using RQTools.Interface;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace RQTools.Models
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DirectorioDB, "RQTools.db3"));
            connection.CreateTable<DeviceUser>();
        }
        public void InsertDeviceUser(DeviceUser deviceUser)
        {
            connection.Insert(deviceUser);
        }
        public void UpdateDeviceUser(DeviceUser deviceUser)
        {
            connection.Update(deviceUser);
        }
        public void DeleteDeviceUser(DeviceUser deviceUser)
        {
            connection.Delete(deviceUser);
        }
        public DeviceUser GetDeviceUser(int ID_User)
        {
            return connection.Table<DeviceUser>().FirstOrDefault(du => du.ID_User == ID_User);
        }
        public List<DeviceUser> GetDeviceUsers()
        {
            return connection.Table<DeviceUser>().ToList();
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

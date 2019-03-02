[assembly: Xamarin.Forms.Dependency(typeof(RQTools.Droid.Implementations.PathService))]
namespace RQTools.Droid.Implementations
{
    using Interface;
    using System;
    using System.IO;

   public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "RQTools.db3");
        }
    }
}
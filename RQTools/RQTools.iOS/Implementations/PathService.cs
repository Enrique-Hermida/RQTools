[assembly: Xamarin.Forms.Dependency(typeof(RQTools.iOS.Implementations.PathService))]
namespace RQTools.iOS.Implementations
{
    using RQTools.Interface;
    using System;
    using System.IO;
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, "RQTools");
        }
    }
}
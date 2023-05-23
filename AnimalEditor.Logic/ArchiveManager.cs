using System.Reflection;

using CommonInterfaces.Plugins;

namespace AnimalEditor.Model
{
    public class ArchiveManager
    {
        private readonly Dictionary<string, IArchiver> _archivers;

        public ArchiveManager()
        {
            _archivers = new Dictionary<string, IArchiver>();
            var pluginDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

            var archivers = GetConcreteArchivers(pluginDirectoryPath);

            //var archivers = new List<IArchiver> { new BZip2Archiver(), new GZipArchiver() };

            archivers?.ForEach(x => _archivers?.Add(x.GetExtension(), x));
        }

        public IArchiver? GetArchiverByExtension(string fileExtenstion)
        {
            return !_archivers.ContainsKey(fileExtenstion) ? null : _archivers[fileExtenstion];
        }

        public List<string> GetAllArchiverExtensions()
        {
            return _archivers.Select(x => x.Value.GetExtension()).ToList();
        }

        private List<IArchiver>? GetConcreteArchivers(string directory)
        {
            if (string.IsNullOrEmpty(directory)) { return null; }

            var info = new DirectoryInfo(directory);
            if (!info.Exists) { return null; }

            var implementors = new List<IArchiver>();

            foreach (var file in info.GetFiles("*.dll"))
            {
                Assembly currentAssembly;
                try
                {
                    currentAssembly = Assembly.LoadFrom(file.FullName);
                }
                catch (Exception)
                {
                    continue;
                }

                var types = currentAssembly.GetTypes();

                currentAssembly.GetTypes()
                    .Where(t => t != typeof(IArchiver) && typeof(IArchiver).IsAssignableFrom(t))
                    .ToList()
                    .ForEach(x => implementors.Add((IArchiver)Activator.CreateInstance(x)));
            }
            return implementors.ToList();
        }
    }
}

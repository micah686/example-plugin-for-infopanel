using McMaster.NETCore.Plugins;
using InfoPanel.Contract;
namespace InfoPanelApp
{
    internal class Program
    {

        private static IEnumerable<IPanelData> panelDataCollection;
        static async Task Main(string[] args)
        {
            var loaders = new List<PluginLoader>();
            var pluginsDir = Path.Combine(AppContext.BaseDirectory, "plugins");
            foreach (var dir in Directory.GetDirectories(pluginsDir))
            {
                var dirName = Path.GetFileName(dir);
                var pluginDll = Path.Combine(dir, dirName + ".dll");
                if (File.Exists(pluginDll))
                {
                    var loader = PluginLoader.CreateFromAssemblyFile(
                        pluginDll,
                        sharedTypes: new[] { typeof(IPanelData) });
                    loaders.Add(loader);
                }
            }



            var panelList = new List<IPanelData>();
            foreach (var loader in loaders)
            {
                foreach (var pluginType in loader
                             .LoadDefaultAssembly()
                             .GetTypes()
                             .Where(t => typeof(IPanelData).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPlugin has a parameterless constructor
                    IPanelData plugin = (IPanelData)Activator.CreateInstance(pluginType);
                    panelList.Add(plugin); 
                }
            }
            panelDataCollection = panelList;

        }



        private async Task LoadDataFromPlugins()
        {
            
            foreach(var plugin in panelDataCollection)
            {
                var data = await plugin.GetData();
                foreach (var item in data.EntryList)
                {
                    //var sdi = SensorDisplayItem()
                    //{
                    //    SensorName = item.Name,
                    //    //put rest of properties here
                    //}
                }
            }

        }

        
    }
}
/*
 PanelData pluginData = await plugin.GetData();

                    Console.WriteLine(pluginData.CollectionName);
                    foreach (var entry in pluginData.EntryList)
                    {
                        Console.WriteLine($"Name: {entry.Name}, Value: {entry.Value}");
                    }

 */
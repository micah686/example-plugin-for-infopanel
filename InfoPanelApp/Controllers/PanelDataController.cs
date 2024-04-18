using InfoPanel.Contract;
using Microsoft.AspNetCore.Mvc;
// Add Prise
using Prise;
// Add the Contract
namespace InfoPanelApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PanelDataController: ControllerBase
    {
        private readonly ILogger<PanelDataController> _logger;
        private readonly IPluginLoader _pluginLoader;

        public PanelDataController(ILogger<PanelDataController> logger, IPluginLoader pluginLoader)
        {
            _logger = logger;
            _pluginLoader = pluginLoader; //Use DependencyInjection to inject the plugin loader
        }

        [HttpGet(Name = "GetPlugin")]
        public async Task<PanelData> GetPluginDataAsync()
        {
            var pluginPath = Path.Combine(AppContext.BaseDirectory, "plugins"); //set the path where people should put plugins. I chose a folder called plugins in the same directory as the exe
            var scanResult = await this._pluginLoader.FindPlugin<IPanelData>(pluginPath);


            if (scanResult == null)
            {
                _logger.LogWarning($"No plugin was found for type {typeof(IPanelData).Name}");
                return null;
            }

            // Load the plugin
            var plugin = await this._pluginLoader.LoadPlugin<IPanelData>(scanResult);

            //get the data from the plugin
            var result = await plugin.GetData();
            //now, you can transform the data as needed
            return result;
        }
    }
}

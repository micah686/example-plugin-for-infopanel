using InfoPanel.Contract;

namespace MyCustomPlugin
{
    public class MyPlugin : IPanelData
    {
        public Task<PanelData> GetData()
        {
            var rnd = new Random();

            List<Entry> entries = new List<Entry>();
            for (int i = 0; i < 5; i++)
            {
                entries.Add(new Entry() { Name = $"Entry{i}", Value = rnd.NextDouble().ToString() });
            }

            var data = new PanelData()
            {
                CollectionName = nameof(MyPlugin),
                EntryList = entries
            };
            return Task.FromResult(data);
        }
    }
}

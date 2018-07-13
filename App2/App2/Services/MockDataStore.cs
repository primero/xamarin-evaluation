using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App2.Models;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(App2.Services.MockDataStore))]
namespace App2.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;
        HttpClient client;

        public MockDataStore()
        {
            items = new List<Item>();

            client = new HttpClient();

        }
        public async Task<List<Item>> RefreshDataAsync()
        {
            items = new List<Item>();

            var uri = new Uri(string.Format("https://jsonplaceholder.typicode.com/posts", string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Item>>(content);
                    Debug.WriteLine(@"				INFO {0}", "Success http");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return items;

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> SaveFile(string s)
        {
            System.Diagnostics.Debug.WriteLine("SAVING " + s);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "myfile.txt");
            try
            {
                using (var streamWriter = new StreamWriter(filename, true))
                {
                    streamWriter.WriteLine(s);
                }
            }
            catch (Exception e)
            {

            }
            return await Task.FromResult(true);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reddit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private async Task TryPostJsonAsync()
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            Uri requestUri = new Uri("https://www.reddit.com//top/.json?count=20");
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Model.RootObject>(httpResponseBody);
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            //string provideUri = "https://api.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=aaf0721010d60c8dfdd2df28ed0ac6b9&format=json&nojsoncallback=1&api_sig=ebb3b6eb5686c1c042a9f8b7d4d4cf87";

            //HttpClient client = new HttpClient();
            //string jsonstring = await client.GetStringAsync(provideUri);
            //var obj = JsonConvert.DeserializeObject<RootObject>(jsonstring);
            //if (obj.stat == "ok")
            //{
            //    FlickrGridView.ItemsSource = obj.photos.photo;
            //}
            //Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            //var headers = httpClient.DefaultRequestHeaders;
            //Uri requestUri = new Uri("http://www.reddit.com/top");
            ////Send the GET request
            //string jsonstring = await httpClient.GetStringAsync(requestUri);
            //var obj = JsonConvert.DeserializeObject<Model.RootObject>(jsonstring);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(() => TryPostJsonAsync());
            task.Wait();

        }
    }
}

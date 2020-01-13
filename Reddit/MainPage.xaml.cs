using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Reddit.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reddit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<ItemReddit> CollectionItemsReddit { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            var task = Task.Run(() => TryPostJsonAsync());
            task.Wait();
            // ReadData();
        }

        private void ReadData()
        {

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            Uri requestUri = new Uri("https://www.reddit.com//top/.json?count=20");
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            try
            {
                //Send the GET request
                httpResponse = httpClient.GetAsync(requestUri).GetResults();
                if (httpResponse.IsSuccessStatusCode)
                {
                    httpResponseBody = httpResponse.Content.ReadAsStringAsync().GetResults();

                    var obj = JsonConvert.DeserializeObject<Model.RootObject>(httpResponseBody, new JsonSerializerSettings() { Culture = CultureInfo.CurrentCulture });
                    var list = (from r in obj.data.children
                                select new { ImgUrl = r.data.thumbnail,
                                    Title = r.data.title,
                                    Author = r.data.author,
                                    NumberOfComments = r.data.num_comments
                                }).ToList().Take(1);
                    gvthumbails.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
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
                var obj = JsonConvert.DeserializeObject<Model.RootObject>(httpResponseBody, new JsonSerializerSettings() { Culture = CultureInfo.CurrentCulture });
                CollectionItemsReddit = new ObservableCollection<ItemReddit>( (from r in obj.data.children
                            select new ItemReddit { ImgUrl = r.data.thumbnail,
                                Title = r.data.title,
                                Author = r.data.author,
                                NumberOfComments = r.data.num_comments
                            }).ToList());


               // gvthumbails.ItemsSource = CollectionItemsReddit;

                //await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                // {
                //     gvthumbails.ItemsSource = list;
                // }
                // );
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(() => TryPostJsonAsync());
            task.Wait();

        }
    }
}

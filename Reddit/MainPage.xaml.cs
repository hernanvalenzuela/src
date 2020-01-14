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
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reddit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Data2> CollectionItemsReddit { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            //ApplicationView.PreferredLaunchViewSize = new Size(800, 1600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            var task = Task.Run(() => TryPostJsonAsync());
            task.Wait();
            // ReadData();
        }
        private async Task TryPostJsonAsync()
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            Uri requestUri = new Uri("https://www.reddit.com//top/.json");
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Model.RootObject>(httpResponseBody, new JsonSerializerSettings() { Culture = CultureInfo.CurrentCulture });
                CollectionItemsReddit = new ObservableCollection<Data2>((from r in obj.data.children
                                                                         select r.data).ToList());
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

        private void gvthumbails_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void gvthumbails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (sender as ListView);
            if (lv.SelectedIndex != null)
            {
                var it = CollectionItemsReddit[int.Parse(lv.SelectedIndex.ToString())];
                imageSelected.UriSource = new Uri( it?.thumbnail);
                authoSelected.Text = it.author;
                selftextSelected.Text = it.title;
            }
        }
    }
}

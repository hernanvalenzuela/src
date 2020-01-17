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
        private ObservableCollection<Data2> collectionItemsData2;
        public ObservableCollection<Data2> CollectionData2
        {
            get
            {
                return new ObservableCollection<Data2>(collectionItemsData2.Take(pageSize * currentpage).Cast<Data2>());
            }
            set { collectionItemsData2 = value; }
        }
        private int lastestIndexSeleccion = 0;
        private int pageSize = 10;
        private int currentpage = 1;
        public MainPage()
        {
            this.InitializeComponent();
            //this.SizeChanged += GroupItemsPage_SizeChange;
            var task = Task.Run(() => TryPostJsonAsync());
            task.Wait();
            lbPageSize.SelectionChanged += lbPageSize_SelectionChanged;
        }

        //private void GroupItemsPage_SizeChange(object sender, SizeChangedEventArgs e)
        //{
        //    if (e.NewSize.Width < 500)
        //    {
        //        VisualStateManager.GoToState(this, "MinimalLayaout", true);
        //    }
        //    else if (e.NewSize.Width < e.NewSize.Height)
        //    {
        //        VisualStateManager.GoToState(this, "PortraitLayout", true);
        //    }
        //    else
        //    {
        //        VisualStateManager.GoToState(this, "DefaultLayaout", true);
        //    }
        //}

        //this method get json data from page and save those at observablecolletion
        private async Task TryPostJsonAsync()
        {
            currentpage = 1;
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

                collectionItemsData2 = new ObservableCollection<Data2>((from r in obj.data.children
                                                                        select r.data).ToList());
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
        }

        private void gvthumbails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (sender as ListView);
            if (lv.SelectedIndex != null && lv.SelectedIndex.ToString() != "-1")
            {
                lastestIndexSeleccion = int.Parse(lv.SelectedIndex.ToString());
                var it = collectionItemsData2[lastestIndexSeleccion];
                imageSelected.UriSource = it?.thumbnail == "default" ? null : new Uri(it?.thumbnail);
                authoSelected.Text = it?.author;
                selftextSelected.Text = it?.title;

            }
            else if (lv.SelectedIndex != null && lv.SelectedIndex.ToString() == "-1")
            {
                lastestIndexSeleccion = lastestIndexSeleccion < collectionItemsData2.Count() ? lastestIndexSeleccion : collectionItemsData2.Count() - 1;
                if (collectionItemsData2.Count() == 0)
                {
                    imageSelected.UriSource = null;
                    authoSelected.Text = "";
                    selftextSelected.Text = "";
                }
                else
                {
                    var it = collectionItemsData2[lastestIndexSeleccion];
                    imageSelected.UriSource = it?.thumbnail == "default" ? null : new Uri(it?.thumbnail);
                    authoSelected.Text = it?.author;
                    selftextSelected.Text = it?.title;
                }
            }
        }
        //event fire to remove element
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext != null)
            {
                collectionItemsData2.Remove(((FrameworkElement)sender).DataContext as Data2);
                //when remove elements perhaps currentpage index maybe lost a current paging reference
                if (Math.Ceiling((decimal)collectionItemsData2.Count() / (decimal)pageSize) < currentpage)
                {
                    currentpage = (int)Math.Ceiling((decimal)collectionItemsData2.Count() / (decimal)pageSize);
                }
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var verticalOffset = svReddit.VerticalOffset;
            var maxVerticalOffset = svReddit.ScrollableHeight;
            if (maxVerticalOffset > 0 && verticalOffset == maxVerticalOffset)
            {
                if (Math.Ceiling((decimal)collectionItemsData2.Count / (decimal)pageSize) > currentpage)
                {
                    currentpage++;
                    gvthumbails.ItemsSource = CollectionData2;
                }
                else
                {
                    currentpage++;
                    var task = Task.Run(() => TryPostJsonAsync());
                    task.Wait();
                }
            }
        }

        private void lbPageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageSize = int.Parse(((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString());
            gvthumbails.ItemsSource = CollectionData2;
        }
    }
}

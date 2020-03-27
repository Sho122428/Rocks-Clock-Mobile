using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BreakLogPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<BreakLog> BreakLogs { get; set; }

        public BreakLogPage()
        {
            InitializeComponent();

            GetBreakLog();

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;
        }

        private async void GetBreakLog()
        {
            try {
                var baseAddr = new Uri("https://localhost:44329");
                var client = new HttpClient { BaseAddress = baseAddr };

                var response = await client.GetStringAsync("https://localhost:44329/BreakLog/1");
                var products = JsonConvert.DeserializeObject<ObservableCollection<BreakLog>>(response);

                BreakLogs = products;
            }
            catch (System.Net.WebException e)
            {
                await DisplayAlert("error", e.ToString(), "Ok");
            }
            
        }



    //HttpClient client = new HttpClient();
    //    try
    //    {
    //        var response = await client.GetStringAsync("https://localhost:44330/BreakLog/1");
    //        var products = JsonConvert.DeserializeObject<ObservableCollection<BreakLog>>(response);

    //        BreakLogs = products;
    //    }
    //    catch (System.Net.WebException e) {
    //        await DisplayAlert("error",e.ToString(),"Ok");
    //    }

//}

async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        
    }
}

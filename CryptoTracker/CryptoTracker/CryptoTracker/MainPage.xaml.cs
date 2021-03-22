using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using CryptoTracker.Model;

namespace CryptoTracker
{
    public partial class MainPage : ContentPage
    {
        private string apiKey = "163037B6-48F7-42D3-8B09-EEC5F7E2438E";
        private string baseImageUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_64/";
        private List<Coin> coins;

        public MainPage()
        {
            InitializeComponent();
            coinListView.ItemsSource = GetCoins();
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            coinListView.ItemsSource = GetCoins();
        }

        private List<Coin> GetCoins()
        {
            List<Coin> coins;
            var client = new RestClient("http://rest.coinapi.io/v1/assets?filter_asset_id=BTC;ETH;XMR;LTC;PPC");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", apiKey);

            var response = client.Execute(request);
            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach (var c in coins)
            {
                if (!String.IsNullOrEmpty(c.id_icon))
                    c.icon_url = baseImageUrl + c.id_icon.Replace("-", "") + ".png";

                else
                    c.icon_url = "";
            }

            return coins;
        }
    }
}

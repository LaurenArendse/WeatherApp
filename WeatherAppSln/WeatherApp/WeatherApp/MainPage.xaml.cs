using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.Service;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();

        }

       
        public async Task<Location> GetLocation()
        {
            Location location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                  return location;
               
            }

            throw new Exception("Cannot get location");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Location loc = await GetLocation();
            WeatherInfo info = await GetRemoteWeather(loc);
            BindingContext = info;
        }

       

        private async Task<WeatherInfo> GetRemoteWeather(Location loc)
        {
            double latitude = loc.Latitude; //latitude range = -90 to 90
            double longitude = loc.Longitude; //longitude range = -180 to 180 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "Application/json");
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid=c6a49940e972b93975b65fad74fa1515";
            string response = await client.GetStringAsync(url);

            WeatherInfo info = JsonConvert.DeserializeObject<WeatherInfo>(response);

            return info;
        }
       
    }
}

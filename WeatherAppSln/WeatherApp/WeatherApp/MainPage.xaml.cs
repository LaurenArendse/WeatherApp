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
        private WeatherInfo _weatherInfo;

        private string _mainIcon;

        public string MainIcon
        {
            get { return _mainIcon; }
            set
            {
                _mainIcon = value;
                OnPropertyChanged();
            }
        }

        public WeatherInfo Weather
        {
            get { return _weatherInfo; }
            set
            {
                _weatherInfo = value;
                OnPropertyChanged();
            }
        }



        public MainPage()
        {
            InitializeComponent();


            BindingContext = this;

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

            Weather = info;
            ApplyMainIcon();
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

        public void ApplyMainIcon()
        {
            for (int i = 0; i < _weatherInfo.weather[0].main.Length; i++)
            {
                if (_weatherInfo.weather[0].main.ToLower().Contains("clear"))
                {
                    MainIcon = "sunny.png";
                }
                else if (_weatherInfo.weather[0].main.ToLower().Contains("clouds"))
                {
                    MainIcon = "cloudy.png";
                }
                else if (_weatherInfo.weather[0].main.ToLower().Contains("shower"))
                {
                    MainIcon = "lightrain.png";
                }
                else if (_weatherInfo.weather[0].main.ToLower().Contains("rain"))
                {
                    MainIcon = "heavyrain.png";
                }
                else if (_weatherInfo.weather[0].main.ToLower().Contains("thunderstorm"))
                {
                    MainIcon = "stormy.png";
                }
                else if (_weatherInfo.weather[0].main.ToLower().Contains("snow"))
                {
                    MainIcon = "snowy.png";
                }
                else if (_weatherInfo.weather[0].main.ToLower().Contains("mist"))
                {
                    MainIcon = "cloudy.png";
                }
            }

            
        }
    }
}

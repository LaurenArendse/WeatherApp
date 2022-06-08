using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{

    public class WeatherInfo
    {

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Temperature { get; set; }

        public string Humidity { get; set; }

        public string Pressure { get; set; }

        public string Cloud {get; set; } //all cloud information

        public string Gust { get; set; } //wind gust

        public string Speed { get; set; } //speed for wind

        public string Deg { get; set; } //degree for wind

        public DateTime DateTime { get; set; } //current date and time
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
namespace ParPorApp.Models
{
    class Place
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the raw.
        /// </summary>
        /// <value>The raw.</value>
        public string Raw { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DurianCode.PlacesSearchBar.Place"/> class.
        /// </summary>
        /// <param name="jsonObject">Json object.</param>
        public Place(JObject jsonObject)
        {
            Name = (string)jsonObject["result"]["name"];
            Latitude = (double)jsonObject["result"]["geometry"]["location"]["lat"];
            Longitude = (double)jsonObject["result"]["geometry"]["location"]["lng"];
            Raw = jsonObject.ToString();
        }
    }
}

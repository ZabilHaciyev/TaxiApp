using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Models;

namespace TaxiApp.Services
{
    public class GetSearchResultService
    {

        public GetSearchResultService()
        {

        }
        public static ObservableCollection<SearchResult> GetSearchResults(string PlaceName)
        {
            ObservableCollection<SearchResult> SearchResults = new ObservableCollection<SearchResult>();
            HttpClient client = new HttpClient();
            string MapUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json?query="+ PlaceName+ "&region=AZ&key="+ ConfigurationManager.ConnectionStrings["GoogleApiKey"].ConnectionString;
            dynamic Places= JsonConvert.DeserializeObject(client.GetAsync(MapUrl).Result.Content.ReadAsStringAsync().Result);

            foreach (var result in Places.results)
            {
                SearchResult searchResult = new SearchResult()
                {
                    Address = result["formatted_address"],
                    Name = result["name"],
                    Icon = result["icon"],
                    Rating = double.Parse(result["rating"].ToString()),
                    Location = new Location(double.Parse(result["geometry"]["location"]["lat"].ToString()), double.Parse(result["geometry"]["location"]["lng"].ToString()))
                };

                SearchResults.Add(searchResult);
            }

            return SearchResults;

        }

    }
}

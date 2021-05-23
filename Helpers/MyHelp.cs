using ErxTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ErxTest.Helpers
{
    public static class MyHelp
    {
        //Credit: https://stackoverflow.com/questions/36001833/asp-net-cannot-get-full-list-of-all-countries-in-the-world?answertab=votes#tab-top

        private static List<CountryModel> _MyCountry = null;
        public static async Task<List<CountryModel>> GetCountryList()
        {
            if (_MyCountry != null) return _MyCountry;

            string url = "https://restcountries.eu/rest/v1/all";

            // Web Request with the given url.
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = await request.GetResponseAsync())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string jsonResponse = null;

                // Store the json response into jsonResponse variable.
                jsonResponse = reader.ReadLine();

                if (jsonResponse != null)
                {
                    // Deserialize the jsonRespose object to the CountryModel. You're getting a JSON array [].
                    _MyCountry = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountryModel>>(jsonResponse).ToList();//.Where(x => x.region == "Asia")
                }
            }

            return _MyCountry;
        }
    }
}

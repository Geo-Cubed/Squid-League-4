using SquidLeagueWebsite.CustomExceptions;
using System.Net.Http;
using System.Threading.Tasks;

namespace SquidLeagueWebsite.ApiRepository
{
    public class ApiConnector
    {
        private readonly string domain = @"https://localhost:5001/_apis/";
        private string apiPath;

        public string fullUrl 
        { 
            get { return domain + apiPath; }
            private set { } 
        } 

        public ApiConnector(string apiPath)
        {
            this.apiPath = apiPath;
        }

        public void UpdatePath(string newPath)
        {
            this.apiPath = newPath;
        }

        public async Task<string> GetJsonAsync()
        {
            try
            {
                string data = string.Empty;
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(this.fullUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            data = await content.ReadAsStringAsync();
                        }
                    }
                }

                return data;
            }
            catch
            {
                throw new ApiAccessException("There was an error while trying to access the api");
            }
        }
    }
}

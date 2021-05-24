using System.Net.Http;

namespace GeoCubed.SquidLeague4.Website.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient 
        {
            get
            {
                return this._httpClient;
            }
        }
    }
}

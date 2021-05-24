using System.Net.Http;

namespace GeoCubed.SquidLeague4.Website.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}

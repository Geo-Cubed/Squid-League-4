using Blazored.LocalStorage;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services.Base
{
    public class BaseDataService
    {
        protected readonly ILocalStorageService _localStorage;
        protected readonly IClient _client;

        public BaseDataService(IClient client, ILocalStorageService localStorageService)
        {
            this._client = client;
            this._localStorage = localStorageService;
        }

        protected ApiResponse<int> ConvertApiExceptions(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                var validationErrors = JObject.Parse(ex.Response)["validationErrors"].ToString()
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Replace("\"", string.Empty);
                return new ApiResponse<int>() { Message = "Validation errors have occured.", ValidationErrors = validationErrors, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new ApiResponse<int>() { Message = "The requested item could not be found.", Success = false };
            }
            else
            {
                return new ApiResponse<int>() { Message = "Something went wrong, please try again.", Success = false };
            }
        }

        protected async Task AddBearerToken()
        {
            if (await this._localStorage.ContainKeyAsync("token"))
            {
                this._client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await this._localStorage.GetItemAsync<string>("token"));
            }
        }
    }
}

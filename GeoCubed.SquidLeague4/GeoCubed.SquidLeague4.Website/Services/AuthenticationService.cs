using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Auth;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
            : base(client, localStorageService)
        {
            this._authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            try
            {
                var request = new AuthenticationRequest() { UserName = username, Password = password };
                var authenticateResponse = await this._client.AuthenticateAsync(request);

                if (!string.IsNullOrEmpty(authenticateResponse.Token))
                {
                    await this._localStorage.SetItemAsync("token", authenticateResponse.Token);
                    await this._localStorage.SetItemAsync("user", authenticateResponse.UserName);
                    ((CustomAuthenticationStateProvider)this._authenticationStateProvider).SetUserAuthenticated(username);
                    this._client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticateResponse.Token);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await this._localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)this._authenticationStateProvider).SetUserLoggedOut();
            this._client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> Register(string username, string password)
        {
            await this.AddBearerToken();
            RegistrationRequest registrationRequest = new RegistrationRequest() { UserName = username, Password = password };
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ValidateAuthenticated()
        {
            await this.AddBearerToken();
            try
            {
                var response = await this._client.CheckValidTokenAsync();
                return response;
            }
            catch
            {
                return false;
            }
        }
    }
}

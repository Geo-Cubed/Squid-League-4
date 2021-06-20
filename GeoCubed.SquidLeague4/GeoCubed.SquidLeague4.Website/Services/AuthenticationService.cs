using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Auth;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IMapper _mapper;

        public AuthenticationService(IMapper mapper, IClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
            : base(client, localStorageService)
        {
            this._authenticationStateProvider = authenticationStateProvider;
            this._mapper = mapper;
        }

        public async Task<bool> AssignRole(AdminRoleRequestViewModel roleRequest)
        {
            await this.AddBearerToken();
            var request = this._mapper.Map<RoleRequest>(roleRequest);
            var response = await this._client.AddrolesAsync(request);
            if (response.Status == RoleStatus._0)
            {
                return true;
            }

            return false;
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

        public async Task<bool> CheckIfAdmin()
        {
            await this.AddBearerToken();
            try
            {
                var response = await this._client.CheckUserAdminAsync();
                return response;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAccount(string username)
        {
            await this.AddBearerToken();
            var request = new DeleteRequest() { Username = username };
            var deleteResponse = await this._client.DeleteaccountAsync(request);
            
            if (deleteResponse.Success)
            {
                return true;
            }

            return false;
        }

        public async Task<List<string>> GetAllRoles()
        {
            await this.AddBearerToken();
            var roles = await this._client.GetAllRolesAsync();
            return roles.Cast<string>().ToList();
        }

        public async Task<List<AdminUserViewModel>> GetAllUsers()
        {
            await this.AddBearerToken();
            var users = await this._client.GetAllUsersAsync();
            return this._mapper.Map<List<AdminUserViewModel>>(users);
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

        public async Task<bool> RemoveRole(AdminRoleRequestViewModel roleRequest)
        {
            await this.AddBearerToken();
            var request = this._mapper.Map<RoleRequest>(roleRequest);
            var response = await this._client.RemoverolesAsync(request);
            if (response.Status == RoleStatus._1)
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

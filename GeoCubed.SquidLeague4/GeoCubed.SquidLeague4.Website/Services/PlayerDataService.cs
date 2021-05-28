using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class PlayerDataService : BaseDataService, IPlayerDataService
    {
        private readonly IMapper _mapper;

        public PlayerDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) 
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreatePlayer(AdminPlayerViewModel playerDetailViewModel)
        {
            try
            {
                if (playerDetailViewModel.TeamId <= 0)
                {
                    playerDetailViewModel.TeamId = null;
                }

                await this.AddBearerToken();
                var createPlayerCommand = this._mapper.Map<CreatePlayerCommand>(playerDetailViewModel);
                var newId = await this._client.AddPlayerAsync(createPlayerCommand);
                return new ApiResponse<int>() { Data = newId.Player.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeletePlayer(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeletePlayerAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<AdminPlayerViewModel>> GetAllPlayers()
        {
            await this.AddBearerToken();

            var allPlayers = await this._client.GetAllPlayersAsync();
            var mappedPlayers = this._mapper.Map<ICollection<AdminPlayerViewModel>>(allPlayers);
            return mappedPlayers.ToList();
        }

        public async Task<PlayerDetailViewModel> GetPlayerById(int id)
        {
            var selectedPlayer = await this._client.GetPlayerByIdAsync(id);
            var mappedPlayer = this._mapper.Map<PlayerDetailViewModel>(selectedPlayer);
            return mappedPlayer;
        }

        public async Task<ApiResponse<int>> UpdatePlayer(AdminPlayerViewModel playerDetailViewModel)
        {
            try
            {
                if (playerDetailViewModel.TeamId <= 0)
                {
                    playerDetailViewModel.TeamId = null;
                }

                await this.AddBearerToken();
                var updatePlayerCommand = this._mapper.Map<UpdatePlayerCommand>(playerDetailViewModel);
                await this._client.UpdatePlayerAsync(updatePlayerCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}

using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class GameDataService : BaseDataService, IGameDataService
    {
        private readonly IMapper _mapper;

        public GameDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateGame(GameViewModel gameViewModel)
        {
            try
            {
                var createGameCommand = this._mapper.Map<CreateGameCommand>(gameViewModel);
                var newId = await this._client.AddGameAsync(createGameCommand);
                return new ApiResponse<int>() { Data = newId.Game.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteGame(int id)
        {
            try
            {
                await this._client.DeleteGameAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<GameViewModel>> GetAllGames()
        {
            var allGames = await this._client.GetAllGamesAsync();
            var mappedGames = this._mapper.Map<ICollection<GameViewModel>>(allGames);
            return mappedGames.ToList();
        }

        public async Task<List<TeamGameViewModel>> GetGamesByTeamId(int teamId)
        {
            var allGames = await this._client.GamesByTeamIdAsync(teamId);
            var mappedGames = this._mapper.Map<ICollection<TeamGameViewModel>>(allGames);
            return mappedGames.ToList();
        }

        public async Task<ApiResponse<int>> UpdateGame(GameViewModel gameViewModel)
        {
            try
            {
                var UpdateGameCommand = this._mapper.Map<UpdateGameCommand>(gameViewModel);
                await this._client.UpdateGameAsync(UpdateGameCommand);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}

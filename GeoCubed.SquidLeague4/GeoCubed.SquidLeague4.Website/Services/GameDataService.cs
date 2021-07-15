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

        public async Task<ApiResponse<int>> CreateGame(AdminGameViewModel gameViewModel)
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

        public async Task<ApiResponse<int>> DeleteResultsInfo(int gameId)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteGameInfoAsync(gameId);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<AdminGameViewModel>> GetAllGames()
        {
            var allGames = await this._client.GetAllGamesAsync();
            var mappedGames = this._mapper.Map<ICollection<AdminGameViewModel>>(allGames);
            return mappedGames.ToList();
        }

        public async Task<List<AdminResultsModel>> GetResultsInfo(int matchId)
        {
            await this.AddBearerToken();

            var setInfo = await this._client.GetGameInfoAsync(matchId);
            var mappedSetInfo = this._mapper.Map<ICollection<AdminResultsModel>>(setInfo);
            return mappedSetInfo.ToList();
        }

        public async Task<ApiResponse<int>> SaveResultsInfo(AdminResultsModel adminResultsModel)
        {
            try
            {
                await this.AddBearerToken();
                var saveGameInfoCommand = this._mapper.Map<SaveGameInfoCommand>(adminResultsModel);
                await this._client.SaveGameInfoAsync(saveGameInfoCommand);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> UpdateGame(AdminGameViewModel gameViewModel)
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

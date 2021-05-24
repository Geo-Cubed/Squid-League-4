using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class HelpfulPersonDataService : BaseDataService, IHelpfulPersonDataService
    {
        private readonly IMapper _mapper;

        public HelpfulPersonDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateHelpfulPerson(HelpfulPersonDetailViewModel helpfulPersonDetailViewModel)
        {
            try
            {
                var createHelpfulPersonCommand = this._mapper.Map <CreateHelpfulPersonCommand>(helpfulPersonDetailViewModel);
                var newId = await this._client.AddHelpfulPersonAsync(createHelpfulPersonCommand);
                return new ApiResponse<int>() { Data = newId.HelpfulPerson.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteHelpfulPerson(int id)
        {
            try
            {
                await this._client.DeleteHelpfulPersonAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<HelpfulPersonDetailViewModel>> GetAllHelpfulPersons()
        {
            var allPeople = await this._client.GetAllHelpfulPeopleAsync();
            var mappedPeople = this._mapper.Map<ICollection<HelpfulPersonDetailViewModel>>(allPeople);
            return mappedPeople.ToList();
        }

        public async Task<ApiResponse<int>> UpdateHelpfulPerson(HelpfulPersonDetailViewModel helpfulPersonDetailViewModel)
        {
            try
            {
                var updatePersonCommand = this._mapper.Map<UpdateHelpfulPersonCommand>(helpfulPersonDetailViewModel);
                await this._client.UpdateHelpfulPersonAsync(updatePersonCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}

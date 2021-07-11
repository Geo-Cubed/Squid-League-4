using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo
{
    public class GetSetInfoQueryHandler : IRequestHandler<GetSetInfoQuery, List<SetInformationVm>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameSettingRepository _gameSettingRepository;
        private readonly IMapper _mapper;

        public GetSetInfoQueryHandler(IMapper mapper, IGameRepository gameRepository, IGameSettingRepository gameSettingRepository)
        {
            this._gameRepository = gameRepository;
            this._gameSettingRepository = gameSettingRepository;
            this._mapper = mapper;
        }

        public async Task<List<SetInformationVm>> Handle(GetSetInfoQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<GameSetting> maps;
            IReadOnlyList<Game> setInfo;
            try
            {
                maps = await this._gameSettingRepository.GetMapListByMatchId(request.MatchId);
                setInfo = await this._gameRepository.GetFullSetInfo(request.MatchId);
            }
            catch
            {
                return null;
            }

            var mappedSetInfo = this._mapper.Map<List<SetInformationVm>>(setInfo);
            var orderedSetInfo = new List<SetInformationVm>();
            for (int i = 1; i <= maps.Count; ++i)
            {
                var mappedSet = mappedSetInfo.FirstOrDefault(x => x.SortOrder == i);
                if (mappedSet != null)
                {
                    orderedSetInfo.Add(mappedSet);
                }
                else
                {
                    var mapMode = maps.ElementAt(i - 1);
                    orderedSetInfo.Add(
                        new SetInformationVm() 
                        { 
                            GameId = -1,
                            MatchId = request.MatchId,
                            SortOrder = i,
                            Map = new MapListMapVm(mapMode.GameMap.MapName, mapMode.GameMap.PicturePath),
                            Mode = new MapListModeVm(mapMode.GameMode.ModeName, mapMode.GameMode.PicturePath)
                        });
                }
            }

            return orderedSetInfo;
        }
    }
}

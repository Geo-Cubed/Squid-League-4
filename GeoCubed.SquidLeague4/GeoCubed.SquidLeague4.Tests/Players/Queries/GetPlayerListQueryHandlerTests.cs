using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerList;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Application.Profiles;
using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Tests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GeoCubed.SquidLeague4.Tests.Players.Queries
{
    public class GetPlayerListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Player>> _mockPlayerRepoisotory;

        public GetPlayerListQueryHandlerTests()
        {
            this._mockPlayerRepoisotory = RepositoryMocks.GetPlayerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this._mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetPlayerListTest()
        {
            //var handler = new GetPlayerListQueryHandler(this._mapper, this._mockPlayerRepoisotory.Object);
            //var result = await handler.Handle(new GetPlayerListQuery(), CancellationToken.None);
            //result.ShouldBeOfType<List<PlayerDetailVM>>();
            //result.Count.ShouldBe(2);
            //result.Count.ShouldBe(2);
        }
    }
}

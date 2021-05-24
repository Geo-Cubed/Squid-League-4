using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Tests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Player>> GetPlayerRepository()
        {
            var players = new List<Player>() 
            {
                new Player()
                {
                    Id = 1,
                    InGameName = "test 1",
                    SzRank = "s",
                    TcRank = "s",
                    RmRank = "s",
                    CbRank = "x",
                    IsActive = true
                },
                new Player()
                {
                    Id = 2,
                    InGameName = "test 2",
                    SzRank = "na",
                    TcRank = "un",
                    RmRank = "b",
                    CbRank = "c-",
                    IsActive = true
                }
            };

            var mockPlayerRepository = new Mock<IAsyncRepository<Player>>();
            mockPlayerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(players);

            mockPlayerRepository.Setup(repo => repo.AddAsync(It.IsAny<Player>())).ReturnsAsync(
                    (Player player) =>
                    {
                        players.Add(player);
                        return player;
                    }
                );

            return mockPlayerRepository;
        }
    }
}

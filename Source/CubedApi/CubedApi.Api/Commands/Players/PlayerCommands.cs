using CubedApi.Api.Data;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using CubedApi.Api.Models.Entities;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Common.Utilities.Interfaces;

namespace CubedApi.Api.Commands.Players
{
    public class PlayerCommands
    {
        private readonly SquidLeagueContext _context;
        private readonly IMapping _mapper;

        public PlayerCommands(SquidLeagueContext context, IMapping mapper)
        {
            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._mapper = mapper ?? throw new ArgumentException("Mapper cannot be null.");
        }

        /// <summary>
        /// Gets all players in the database that are active.
        /// </summary>
        /// <returns>A list of active players in the database.</returns>
        public List<PlayerDto> GetAllPlayers()
        {
            var players = this._context.Players
                .Where(p => p.IsActive ?? false)
                .ToList();
            if (players.Count() == 0 || players.IsNull())
            {
                throw new NoDataException("No active players.");
            }

            return players.Select(p => this._mapper.PlayerEntityToDto(p)).ToList();
        } 

        /// <summary>
        /// Gets a single active player info from their Id.
        /// </summary>
        /// <param name="id">The interger id of the player.</param>
        /// <returns>The player profile of the player.</returns>
        public PlayerDto GetPlayerById(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var player = this._context.Players.FirstOrDefault(p => p.Id == id && (p.IsActive ?? false));
            if (player.IsNull())
            {
                throw new DataIsNullException();
            }

            return this._mapper.PlayerEntityToDto(player);
        }

        /// <summary>
        /// Gets a list of active players on an active team.
        /// </summary>
        /// <param name="id">The id of the team.</param>
        /// <returns>A list of active players.</returns>
        public List<PlayerDto> GetPlayersByTeamId(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var players = this._context.Players
                .Where(p => p.TeamId == id && (p.IsActive ?? false))
                .ToList();
            if (!players.Any())
            {
                throw new NoDataException();
            }

            return players.Select(p => this._mapper.PlayerEntityToDto(p)).ToList();
        }

        public List<WeaponDto> GetCommonWeaponsForPlayer(int id)
        {
            if (id.IsInvalid())
            {
                throw new InvalidIdException();
            }

            var player = this._context.Players.FirstOrDefault(p => p.Id == id && (p.IsActive ?? false));
            if (player.IsNull())
            {
                throw new InvalidIdException();
            }

            var weapons = player.GetCommonWeapons(this._context).Select(w => this._mapper.WeaponEntityToDto(w)).ToList();
            if (!weapons.Any())
            {
                throw new NoDataException();
            }

            return weapons;
        }
    }
}

﻿using CubedApi.Api.Commands.Matches;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities.Interfaces;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Linkers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly MatchCommands _matchCommands;

        public MatchController(SquidLeagueContext context, IMapping mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentException("Mapper cannot be null.");
            }

            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._matchCommands = new MatchCommands(this._context, mapper);
        }

        // GET: _apis/<MatchController>
        [HttpGet]
        public ActionResult<List<MatchDto>> GetAllMatches()
        {
            throw new NotImplementedException();
        }

        // GET: _apis/<MatchController>/swiss
        [HttpGet("swiss")]
        public ActionResult<List<MatchDto>> GetSwissMatches()
        {
            try
            {
                return this._matchCommands.GetAllSwissMatches();
            }
            catch (NoDataException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<MatchController>/5
        [HttpGet("{id}")]
        public ActionResult<MatchProfile> Get(int id)
        {
            try
            {
                return this._matchCommands.GetMatchInformationById(id);
            }
            catch (DataIsNullException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("upcomming")]
        public ActionResult<List<MatchDto>> GetUpcommingMatches()
        {
            try
            {
                return this._matchCommands.GetUpcommingMatches();
            }
            catch (NoDataException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<MatchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}

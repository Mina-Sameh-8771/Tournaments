using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public ApiController(ITeamService teamService)
        {
            this._teamService = teamService;
        }

        [HttpGet("GetTopTeams")]
        public async Task<ActionResult<List<TeamViewModel>>> GetTeams(int numberOfTeams)
        {
            return await _teamService.GetTeamsSortByCreationTime(numberOfTeams);
        }
    }
}

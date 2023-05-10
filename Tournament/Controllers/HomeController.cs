using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tournament.Models;

namespace Tournament.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        public HomeController(ILogger<HomeController> logger , ITeamService teamService , ITournamentService tournamentService)
        {
            _logger = logger;
            this._teamService = teamService;
            this._tournamentService = tournamentService;
        }

        public async Task<IActionResult> Index()
        {
            var teamsList = await _teamService.GetAllTeams();
            var tournamentList = await _tournamentService.GetAllTournaments();
            return View(new HomeViewModel 
            {
                Teams = teamsList,
                Tournaments = tournamentList

            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

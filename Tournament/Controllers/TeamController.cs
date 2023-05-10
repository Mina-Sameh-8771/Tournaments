using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Interface;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tournament.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        public TeamController(ITeamService teamService , ITournamentService tournamentService)
        {
            this._teamService = teamService;
            this._tournamentService = tournamentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddTeam()
        {
            TeamModel teamModel = new TeamModel();
            return View(teamModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam(TeamModel teamAddModel)
        {
            
            await _teamService.AddTeam(teamAddModel);
            await _teamService.Commit();

            return RedirectToAction("Index" , "Home");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeam(id);
            await _teamService.Commit();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> EditTeam(int id)
        {
            var TeamModel = await _teamService.GetTeam(id);
            
            return View(TeamModel);
        }

        [HttpPost]
        public async  Task<ActionResult> EditTeam(TeamModel teamModel)
        {
            await _teamService.UpdateTeam(teamModel);
            await _teamService.Commit();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> TeamFilter()
        {
            var tournaments = await _tournamentService.GetAllTournaments();
            ViewBag.Tournaments = tournaments.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View();
        }

        [HttpGet]
        public async Task<List<TeamViewModel>> GetTeamsFilterByTournament(int id)
        {
            var teams = await _teamService.GetTeamsFilterByTournament(id);

            return teams;
        }
    }
}

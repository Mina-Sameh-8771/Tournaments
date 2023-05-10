using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournament.Manager;
using Tournament.Models;

namespace Tournament.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;
        private readonly ITeamService _teamService;
        private readonly ITournamentTeamService _tournamentTeamService;

        public TournamentController(ITournamentService tournamentService , ITeamService teamService , ITournamentTeamService tournamentTeamService)
        {
            this._tournamentService = tournamentService;
            this._teamService = teamService;
            this._tournamentTeamService = tournamentTeamService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AddTournament()
        {
            TournamentModel tournamentModel = new TournamentModel();
            var teams = await _teamService.GetAllTeams();

            ViewBag.Teams = teams.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(tournamentModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddTournament(TournamentModel tournamentModel)
        {
            if (tournamentModel.SelectedTeam == null)
                tournamentModel.SelectedTeam = new int[0];

            var newTournament = await _tournamentService.AddTournament(tournamentModel);
            await _tournamentService.Commit();
            await _tournamentTeamService.AddTeamsToTournament(tournamentModel.SelectedTeam, newTournament.Id);
            await _tournamentService.Commit();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteTournament(int id)
        {
            await _tournamentService.DeleteTournament(id);
            await _tournamentService.Commit();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> EditTournament(int id)
        {
            var tournamentModel = await _tournamentService.GetTournament(id);
            tournamentModel.SelectedTeam = await _tournamentTeamService.GetSelectedTeamsForTournament(id);

            var teams = await _teamService.GetAllTeams();
            ViewBag.Teams = teams.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(tournamentModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditTournament(TournamentModel tournamenModel)
        {
            if (tournamenModel.SelectedTeam == null)
                tournamenModel.SelectedTeam = new int[0];

            await _tournamentService.UpdateTournament(tournamenModel);
            await _tournamentTeamService.RemoveTeamsFromTournament(tournamenModel.SelectedTeam ,tournamenModel.Id);
            await _tournamentTeamService.AddTeamsToTournament(tournamenModel.SelectedTeam , tournamenModel.Id);
            await _tournamentService.Commit();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> TournamentSearch(string currentFilter ,string searchString , int? pageNumber)
        {
            

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var tournamentsQuary = _tournamentService.GetTournamentsQuarable();

            if (!String.IsNullOrEmpty(searchString))
            {
                tournamentsQuary = tournamentsQuary.Where(s => s.Name.Contains(searchString));
            }


            int pageSize = 2;
            return View(await PaginatedList<DomainLayer.Entity.Tournament>.CreateAsync(tournamentsQuary, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> DetailsTournament(int id)
        {
            var tournamentModel = await _tournamentService.GetTournament(id);
            var tournaamentTeams = await _teamService.GetTeamsFilterByTournament(id);

            return View(new TournamentDetailsViewModel 
            { 
                tournamentModel = tournamentModel,
                teams = tournaamentTeams
            });
        }
    }
}

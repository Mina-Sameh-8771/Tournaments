using DomainLayer.Entity;
using Microsoft.AspNetCore.Http;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ITeamService
    {
        Task AddTeam(TeamModel team);
        Task<TeamModel> GetTeam(int id);
        Task UpdateTeam(TeamModel team);
        Task DeleteTeam(int teamId);
        Task<List<TeamViewModel>> GetAllTeams();
        Task<List<TeamViewModel>> GetTeamsFilterByTournament(int tournamentId);
        Task<List<TeamViewModel>> GetTeamsSortByCreationTime(int topTeamsNumber);
        Task Commit();
    }
}

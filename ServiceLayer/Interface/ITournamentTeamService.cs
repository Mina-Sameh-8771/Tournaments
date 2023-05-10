using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ITournamentTeamService
    {
        Task AddTeamsToTournament(int[] teamsId, int tournamentId);
        Task RemoveTeamsFromTournament(int[] teamsId, int tournamentId);
        Task<int[]> GetSelectedTeamsForTournament(int tournamentId);
        Task Commit();
    }
}

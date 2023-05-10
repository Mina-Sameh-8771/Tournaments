using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class TournamentTeamService : ITournamentTeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TournamentTeamService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task AddTeamsToTournament(int[] teamsId , int tournamentId)
        {
            var existTeamsId = await _unitOfWork.TournamentTeamRepository.GetAll()
                .Where(w => w.TournamentId == tournamentId)
                .Select(s => s.TeamId).ToListAsync();

            for(int i = 0; i < teamsId.Length; i++)
            {

                if (existTeamsId.Contains(teamsId[i])) continue;

                 await _unitOfWork.TournamentTeamRepository.Add(new TournamentTeam 
                 { 
                     TeamId = teamsId[i],
                     TournamentId = tournamentId
                 });
            }
        }

        public async Task RemoveTeamsFromTournament(int[] teamsId ,int tournamentId)
        {
            var teams = await _unitOfWork.TournamentTeamRepository.GetAll()
                .Where(w => w.TournamentId == tournamentId && !teamsId.Contains(w.TeamId))
                .ToListAsync();
            
            foreach(var team in teams)
            {
                _unitOfWork.TournamentTeamRepository.Delete(team);
            }
        }

        public async Task<int[]> GetSelectedTeamsForTournament(int tournamentId)
        {
           return await _unitOfWork.TournamentTeamRepository.GetAll()
                .Where(w => w.TournamentId == tournamentId).Select(s => s.TeamId).ToArrayAsync();
        }

        public async Task Commit()
        {
            await _unitOfWork.Commit();
        }

    }
}

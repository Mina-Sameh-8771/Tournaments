using DomainLayer.Entity;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ITournamentService
    {
        Task<Tournament> AddTournament(TournamentModel tournament);
        Task DeleteTournament(int id);
        Task<TournamentModel> GetTournament(int id);
        Task<List<TournamentViewModel>> GetAllTournaments();
        Task UpdateTournament(TournamentModel tournament);
        IQueryable<Tournament> GetTournamentsQuarable();
        Task Commit();
    }
}

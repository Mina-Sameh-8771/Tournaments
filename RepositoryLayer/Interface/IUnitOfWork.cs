using DomainLayer.Entity;
using RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUnitOfWork
    {
        public Repository<Match> MatchRepository { get; }
        public Repository<Player> PlayerRepository { get; }
        public Repository<Team> TeamRepository { get; }
        public Repository<Tournament> TournamentRepository { get; }
        public Repository<TournamentTeam> TournamentTeamRepository { get; }
        Task Commit();
    }
}

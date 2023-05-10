using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Interface;
using RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        private Repository<Match> matchRepository;
        private Repository<Player> playerRepository;
        private Repository<Team> teamRepository;
        private Repository<Tournament> tournamentRepository;
        private Repository<TournamentTeam> tournamentTeamRepository;
        private Repository<MatchTeam> matchTeamRepository;

        public Repository<Match> MatchRepository
        {
            get
            {
                if (this.matchRepository == null)
                {
                    this.matchRepository = new Repository<Match>(_context);
                }
                return matchRepository;
            }
        }
        public Repository<Player> PlayerRepository
        {
            get
            {
                if (this.playerRepository == null)
                {
                    this.playerRepository = new Repository<Player>(_context);
                }
                return playerRepository;
            }
        }
        public Repository<Team> TeamRepository
        {
            get
            {
                if (this.teamRepository == null)
                {
                    this.teamRepository = new Repository<Team>(_context);
                }
                return teamRepository;
            }
        }
        public Repository<Tournament> TournamentRepository
        {
            get
            {
                if (this.tournamentRepository == null)
                {
                    this.tournamentRepository = new Repository<Tournament>(_context);
                }
                return tournamentRepository;
            }
        }
        public Repository<TournamentTeam> TournamentTeamRepository
        {
            get
            {
                if (this.tournamentTeamRepository == null)
                {
                    this.tournamentTeamRepository = new Repository<TournamentTeam>(_context);
                }
                return tournamentTeamRepository;
            }
        }

        public Repository<MatchTeam> MatchTeamRepository
        {
            get
            {
                if (this.matchTeamRepository == null)
                {
                    this.matchTeamRepository = new Repository<MatchTeam>(_context);
                }
                return matchTeamRepository;
            }
        }

        public UnitOfWork(Context context) { _context = context; }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}

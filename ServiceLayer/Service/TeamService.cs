using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interface;
using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace ServiceLayer.Service
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public TeamService(IUnitOfWork unitOfWork , IImageService imageService)
        {
            this._unitOfWork = unitOfWork;
            this._imageService = imageService;
        }

        public async Task AddTeam(TeamModel team)
        {
            string imageName = await _imageService.UploadedFile(team.Logo);
            await _unitOfWork.TeamRepository.Add(new Team 
            { 
                Description = team.Description,
                FoundationDate = team.FoundationDate,
                Name = team.Name,
                OfficialWebsiteURL = team.OfficialWebsiteURL,
                Logo = imageName ?? "",
                CreationTime = DateTime.Now
            });
        }

        public async Task<TeamModel> GetTeam(int id)
        {
            var team = await _unitOfWork.TeamRepository.GetById(id);

            return new TeamModel 
            { 
                Description = team.Description,
                FoundationDate = team.FoundationDate,
                Id = team.Id,
                Name = team.Name,
                OfficialWebsiteURL = team.OfficialWebsiteURL
            };
        }

        public async Task UpdateTeam(TeamModel team)
        {

            Team teamUpdated = new Team
            {
                Id = team.Id,
                Description = team.Description,
                FoundationDate = team.FoundationDate,
                Name = team.Name,
                OfficialWebsiteURL = team.OfficialWebsiteURL
            };

            if(team.Logo != null)
            {
                teamUpdated.Logo = await _imageService.UploadedFile(team.Logo);
            }

             _unitOfWork.TeamRepository.Update(teamUpdated);
        }

        public async Task DeleteTeam(int teamId)
        {
            Team team = await _unitOfWork.TeamRepository.GetById(teamId);
            _unitOfWork.TeamRepository.Delete(team);
        }

        public async Task<List<TeamViewModel>> GetAllTeams()
        {
            return await _unitOfWork.TeamRepository.GetAll().Select(s => new TeamViewModel 
            {
                Id = s.Id,
                Description = s.Description,
                FoundationDate = s.FoundationDate,
                Name = s.Name,
                OfficialWebsiteURL = s.OfficialWebsiteURL,
                Logo = s.Logo
            }).ToListAsync();
        }

        public async Task<List<TeamViewModel>> GetTeamsFilterByTournament(int tournamentId)
        {
            return await _unitOfWork.TournamentTeamRepository.GetAll().Where(w => w.TournamentId == tournamentId)
                .Select(s => new TeamViewModel
                {
                    Id = s.Team.Id,
                    Description = s.Team.Description,
                    FoundationDate = s.Team.FoundationDate,
                    Logo = s.Team.Logo,
                    Name = s.Team.Name,
                    OfficialWebsiteURL = s.Team.OfficialWebsiteURL
                }).ToListAsync();
        }

        public async Task<List<TeamViewModel>> GetTeamsSortByCreationTime(int topTeamsNumber)
        {
            return await _unitOfWork.TeamRepository.GetAll().OrderByDescending(o => o.CreationTime).Take(topTeamsNumber).
                Select(s => new TeamViewModel 
                {
                    Id = s.Id,
                    Description = s.Description,
                    Logo = s.Logo,
                    FoundationDate = s.FoundationDate,
                    Name = s.Name,
                    OfficialWebsiteURL = s.OfficialWebsiteURL
                }).ToListAsync();
        }

        public async Task Commit()
        {
            await _unitOfWork.Commit();
        }
    }
}

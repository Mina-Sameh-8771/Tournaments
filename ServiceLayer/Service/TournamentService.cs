using DomainLayer.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        public TournamentService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            this._unitOfWork = unitOfWork;
            this._imageService = imageService;
        }

        public async Task<Tournament> AddTournament(TournamentModel tournament)
        {
            string imageName = await _imageService.UploadedFile(tournament.Logo);

            return await _unitOfWork.TournamentRepository.Add(new Tournament 
            {
                Id = tournament.Id,
                Logo = imageName,
                YoutubeVideoKey = tournament.YoutubeVideoKey,
                Name = tournament.Name
            });
        }

        public async Task DeleteTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetById(id);
            _unitOfWork.TournamentRepository.Delete(tournament);
        }

        public async Task UpdateTournament(TournamentModel tournament)
        {
            var tournamentUpdated = new Tournament 
            {
                Id = tournament.Id,
                Name = tournament.Name,
                YoutubeVideoKey = tournament.YoutubeVideoKey
            };

            if (tournament.Logo != null)
            {
                tournamentUpdated.Logo = await _imageService.UploadedFile(tournament.Logo);
            }

            _unitOfWork.TournamentRepository.Update(tournamentUpdated);
        }

        public async Task<List<TournamentViewModel>> GetAllTournaments()
        {
            return await _unitOfWork.TournamentRepository.GetAll().Select(s => new TournamentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Logo = s.Logo
            }).ToListAsync();
        }

        public  IQueryable<Tournament> GetTournamentsQuarable()
        {
            return  _unitOfWork.TournamentRepository.GetAll().AsNoTracking();
        }



        public async Task<TournamentModel> GetTournament(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetById(id);

            return new TournamentModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                YoutubeVideoKey = tournament.YoutubeVideoKey
            };
        }

        public async Task Commit()
        {
            await _unitOfWork.Commit();
        }

    }
}

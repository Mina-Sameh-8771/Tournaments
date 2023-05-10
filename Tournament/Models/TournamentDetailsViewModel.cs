using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tournament.Models
{
    public class TournamentDetailsViewModel
    {
        public TournamentModel tournamentModel { get; set; }
        public List<TeamViewModel> teams { get; set; }
    }
}

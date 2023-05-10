using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tournament.Models
{
    public class HomeViewModel
    {
        public List<TournamentViewModel> Tournaments { get; set; }
        public List<TeamViewModel> Teams { get; set; }
    }
}

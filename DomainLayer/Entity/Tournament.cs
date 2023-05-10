using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class Tournament : BaseEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string YoutubeVideoKey { get; set; }
        public virtual ICollection<Match> Matchs { get; set; }
        public virtual ICollection<TournamentTeam> TournamentTeams { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class Team : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string OfficialWebsiteURL { get; set; }
        public DateTime FoundationDate  { get; set; }
        public string Logo  { get; set; }
        public DateTime? CreationTime { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<MatchTeam> MatchTeams { get; set; }
    }
}

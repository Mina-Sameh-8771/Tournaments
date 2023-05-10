using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class TournamentTeam : BaseEntity 
    {
        public int TeamId { get; set; }
        public int TournamentId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; } 

        [ForeignKey("TournamentId")]
        public virtual Tournament Tournament { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class MatchTeam : BaseEntity
    {
        public int TeamId { get; set; }
        public int MatchId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
    }
}

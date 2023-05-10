using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModel
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Official Website URL")]
        public string OfficialWebsiteURL { get; set; }

        [DisplayName("Foundation Date")]
        public DateTime FoundationDate { get; set; }
        [DisplayName("Logo")]
        public string Logo { get; set; }
    }
}

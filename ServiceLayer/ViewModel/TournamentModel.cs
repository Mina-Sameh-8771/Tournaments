using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModel
{
    public class TournamentModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Logo")]
        public IFormFile Logo { get; set; }

        [DisplayName("Youtube Video Key")]
        public string YoutubeVideoKey { get; set; } 

        [DisplayName("Teams")]
        public int[] SelectedTeam { get; set; }

    }
}

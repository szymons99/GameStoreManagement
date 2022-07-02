using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games__MVN_EF_Razor.Models.GameStore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Games_EF.Models.Awards
{
    public class AwardModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public int GameId { get; set; }

        [Display(Name = "Title: ")]
        public GameModel Game { get; set; }
    }
}

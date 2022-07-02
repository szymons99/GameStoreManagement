using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Games_EF.Models.Developers;
using Games_EF.Validators;

namespace Games__MVN_EF_Razor.Models.GameStore
{
    public class GameModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }
        //[DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        [Required(ErrorMessage = "Please enter date")]
        [UIHint("Format dd/mm/yyyy")]
        public DateTime Published { get; set; }
        [Required]
        [Range(0, 1000)]
        public float Price { get; set; }
        public int DeveloperId { get; set; }
        
        [Display(Name = "Name: ")]
        public DeveloperModel Developer{ get; set; }
        //public Role GameRole { get; set; }
    }
}

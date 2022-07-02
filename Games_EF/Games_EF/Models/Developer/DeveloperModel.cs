using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games_EF.Validators;
using System.ComponentModel.DataAnnotations;

namespace Games_EF.Models.Developers
{
    public class DeveloperModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        //[DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        [Required(ErrorMessage = "Please choose date.")]
        [DataType(DataType.Date)]
        [DateValidator(ErrorMessage = "Date must be less than or equal to Today's Date.")]
        public DateTime DateOfEstablishment { get; set; }
    }
}

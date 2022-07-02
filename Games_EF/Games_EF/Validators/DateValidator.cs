using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Games_EF.Validators
{
    public class DateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
             DateTime date = Convert.ToDateTime(value);
             return date <= DateTime.Now;
        }
    }
}

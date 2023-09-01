using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record CategoryForManipulationDto
    {
        [Required(ErrorMessage = "CategoryName is a requered field")]
        [MinLength(3, ErrorMessage = "CategoryName must consist of at least 3 characters")]
        [MaxLength(20, ErrorMessage = "CategoryName must consist of at maximum 20 characters")]
        public string CategoryName { get; set; }
    }
}
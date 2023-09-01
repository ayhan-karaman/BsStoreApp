using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CategoryForUpdateDto : CategoryForManipulationDto
    {
        [Required]
        public int Id { get; set; }
    }
}
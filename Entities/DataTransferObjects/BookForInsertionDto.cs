using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record BookForInsertionDto : BookForManipulationDto
    {
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
    }
}
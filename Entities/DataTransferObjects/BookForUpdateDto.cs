using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record BookForUpdateDto: BookForManipulationDto
    {
        [Required]
        public int Id { get; init; }
    }
}
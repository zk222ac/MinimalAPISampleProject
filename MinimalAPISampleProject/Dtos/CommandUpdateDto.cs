using System.ComponentModel.DataAnnotations;

namespace MinimalAPISampleProject.Dtos
{
    public class CommandUpdateDto
    {
        [Required]
        public string? HotTo { get; set; }

        [Required]
        [MaxLength(5)]
        public string? Plateform { get; set; }

        [Required]
        public string? CommandLine { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MinimalAPISampleProject.Dtos
{
    public class CommandReadDto
    {       
        public int Id { get; set; }
       
        public string? HotTo { get; set; }
        
        //public string? Plateform { get; set; }
        
        public string? CommandLine { get; set; }

    }
}

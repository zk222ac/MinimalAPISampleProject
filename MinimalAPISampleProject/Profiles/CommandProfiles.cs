using AutoMapper;
using MinimalAPISampleProject.Dtos;
using MinimalAPISampleProject.Models;

namespace MinimalAPISampleProject.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            // Source --> Target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto,Command>();
            CreateMap<CommandUpdateDto, Command>();
        }
    }
}

using MinimalAPISampleProject.Models;
using System.Collections.Generic;

namespace MinimalAPISampleProject.Data
{
    public interface ICommandRepo
    {
        Task SaveChanges();
        Task<Command> GetCommandById(int Id);
        Task<IEnumerable<Command>> GetAllCommands();
        Task CreateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}

using PipeManagmentApp.Data.Models;
namespace PipeManagmentApp.Data.Interfaces
{
    public interface IPipeService
    {
        IEnumerable<Pipe> GetAllPipes();
        void CreatePipe(Pipe pipe);
        void EditPipe(Pipe pipe);
        void DeletePipe(int id);
        Pipe GetPipeById(int pipeId);
    }
}

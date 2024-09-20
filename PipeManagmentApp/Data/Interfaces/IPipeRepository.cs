using PipeManagmentApp.Data.Models;
namespace PipeManagmentApp.Data.Interfaces
{
    public interface IPipeRepository
    {
        IEnumerable<Pipe> AllPipes { get; }
        void createPipe(Pipe pipe);
        void editPipe(Pipe pipe);
        void deletePipe(int id);
        Pipe getPipeObj(int pipeId);
    }
}

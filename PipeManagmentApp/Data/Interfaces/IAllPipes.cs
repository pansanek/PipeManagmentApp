using PipeManagmentApp.Data.Models;
namespace PipeManagmentApp.Data.Interfaces
{
    public interface IAllPipes
    {
        IEnumerable<Pipe> AllPipes { get; }
        void createPipe(Pipe pipe);
        void editPipe(Pipe pipe);
        void deletePipe(Pipe pipe);
        Pipe getPipeObj(int pipeId);
    }
}

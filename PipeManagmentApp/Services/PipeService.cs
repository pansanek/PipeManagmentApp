using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Service
{

    public class PipeService : IPipeService
    {
        private readonly IPipeRepository _pipeRepository;

        public PipeService(IPipeRepository pipeRepository)
        {
            _pipeRepository = pipeRepository;
        }

        public IEnumerable<Pipe> GetAllPipes()
        {
            return _pipeRepository.AllPipes;
        }

        public void CreatePipe(Pipe pipe)
        {

            _pipeRepository.createPipe(pipe);
        }

        public void EditPipe(Pipe pipe)
        {

            _pipeRepository.editPipe(pipe);
        }

        public void DeletePipe(int id)
        {
            _pipeRepository.deletePipe(id);
        }

        public Pipe GetPipeById(int pipeId)
        {
            return _pipeRepository.getPipeObj(pipeId);
        }
    }
}

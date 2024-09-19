using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Repository
{
    public class PipeRepository : IAllPipes
    {
        private readonly AppDbContext _appDbContext;

        public PipeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Получение всех труб с базы данных
        public IEnumerable<Pipe> AllPipes => _appDbContext.Pipes.ToList();

        // Создание новой трубы
        public void createPipe(Pipe pipe)
        {
            if (pipe != null)
            {
                var existingPipe = _appDbContext.Pipes.Any(p => p.id == pipe.id);
                if (existingPipe)
                {
                    throw new InvalidOperationException("Запись с таким ID уже существует.");
                }

                _appDbContext.Pipes.Add(pipe);
                _appDbContext.SaveChanges();
            }
        }

        // Удаление трубы
        public void deletePipe(int id)
        {
            var pipe = _appDbContext.Pipes.Find(id);
            if (pipe != null)
            {
                _appDbContext.Pipes.Remove(pipe);
                _appDbContext.SaveChanges();
            }
        }

        // Редактирование существующей трубы
        public void editPipe(Pipe pipe)
        {
            var existingPipe = _appDbContext.Pipes.FirstOrDefault(p => p.id == pipe.id);
            if (existingPipe != null)
            {
                existingPipe.number = pipe.number;
                existingPipe.quality = pipe.quality;
                existingPipe.steelGrade = pipe.steelGrade;
                existingPipe.dimensions = pipe.dimensions;
                existingPipe.weight = pipe.weight;
                _appDbContext.SaveChanges();
            }
        }

        // Получение конкретной трубы по ID
        public Pipe getPipeObj(int pipeId)
        {
            return _appDbContext.Pipes.FirstOrDefault(p => p.id == pipeId);
        }
    }
}

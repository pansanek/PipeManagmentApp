using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Mocks
{
    public class MockPipes : IPipeRepository
    {
        private List<Pipe> _allPipes;
        
        public MockPipes()
        {
            _allPipes = new List<Pipe>
            {
                new Pipe { id = 1, number = 1001,   quality = "Годная", steelGrade = "A1", dimensions = "100x200", weight = 20.5 },
                new Pipe { id = 2, number = 1002, quality = "Брак", steelGrade = "A2", dimensions = "120x250", weight = 25.0 },
                new Pipe { id = 3, number = 1003, quality = "Годная", steelGrade = "B1", dimensions = "150x300", weight = 30.0 }
            };
        }

        public IEnumerable<Pipe> AllPipes  {get { return _allPipes; } }
        
        public void createPipe(Pipe pipe)
        {
            if (pipe != null)
            {
                pipe.id = _allPipes.Max(p => p.id) + 1;
                _allPipes.Add(pipe);
            }
        }

        public void deletePipe(int id)
        {
            var pipe = _allPipes[id];
            if (pipe != null && _allPipes.Contains(pipe))
            {
                _allPipes.Remove(pipe);
            }
        }

        public void editPipe(Pipe pipe)
        {
            var existingPipe = _allPipes.FirstOrDefault(p => p.id == pipe.id);
            if (existingPipe != null)
            {
                existingPipe.number = pipe.number;
                existingPipe.quality = pipe.quality;
                existingPipe.steelGrade = pipe.steelGrade;
                existingPipe.dimensions = pipe.dimensions;
                existingPipe.weight = pipe.weight;
            }
        }

        public Pipe getPipeObj(int pipeId)
        {
            return _allPipes.FirstOrDefault(p => p.id == pipeId);
        }
    }
}

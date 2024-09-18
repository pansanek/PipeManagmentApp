using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.ViewModels
{
    public class PipesListViewModel
    {
        public IEnumerable<Pipe> allPipes { get; set; }
        public int totalPipes { get; set; }
        public int goodPipes { get; set; }
        public int defectivePipes { get; set; }
        public double totalWeight { get; set; }
    }
}

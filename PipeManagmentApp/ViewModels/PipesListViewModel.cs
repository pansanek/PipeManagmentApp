using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.ViewModels
{
    public class PipesListViewModel
    {
        public IEnumerable<Pipe> AllPipes { get; set; }
        public int TotalPipes { get; set; }
        public int GoodPipes { get; set; }
        public int DefectivePipes { get; set; }
        public double? TotalWeight { get; set; }
        public string FilterNumber { get; set; }
        public string FilterQuality { get; set; }
        public string FilterSteelGrade { get; set; }
        public string Title { get; set; }
        public bool IsFilterApplied { get; set; }
    }
}

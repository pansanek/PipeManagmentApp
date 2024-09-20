using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.ViewModels
{
    public class CreateBundleViewModel
    {
        public Bundle Bundle { get; set; }
        public IEnumerable<Pipe> AvailablePipes { get; set; }
        public List<int> AddedPipes { get; set; }
    }
}

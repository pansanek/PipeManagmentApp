using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.ViewModels
{
    public class EditBundleViewModel
    {
        public Bundle Bundle { get; set; }
        public IEnumerable<Pipe> AvailablePipes { get; set; }
        public List<int> RemovedPipes { get; set; }
        public List<int> AddedPipes { get; set; }
    }
}

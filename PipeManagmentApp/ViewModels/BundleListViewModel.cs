using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.ViewModels
{
    public class BundleListViewModel
    {
        public IEnumerable<Bundle> AllBundles { get; set; }
        public string Title { get; set; }
        public bool IsFilterApplied { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }
}

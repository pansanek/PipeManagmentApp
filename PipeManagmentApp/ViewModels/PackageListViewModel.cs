using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.ViewModels
{
    public class PackageListViewModel
    {
        public IEnumerable<Package> allPackages { get; set; }
    }
}

using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Interfaces
{
    public interface IAllPackages
    {
        IEnumerable<Package> AllPackages { get; }
        void createPackage(Package package);
        void editPackage(Package package);
        void deletePackage(Package package);
        void deletePipeFromPackage(Package package, int pipeId);
    }
}

using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Interfaces
{
    public interface IAllBundles
    {
        IEnumerable<Bundle> AllBundles { get; }
        void createBundle(Bundle bundle);
        void editBundle(Bundle bundle);
        void deleteBundle(int id);
        void deletePipeFromBundle(Bundle bundle, int pipeId);
        void addPipeToBundle(Bundle bundle, int pipeId);
        Bundle getBundleObj(int bundleId);
    }
}

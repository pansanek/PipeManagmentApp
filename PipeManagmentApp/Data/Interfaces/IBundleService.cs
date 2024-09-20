using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Interfaces
{
    public interface IBundleService
    {
        IEnumerable<Bundle> GetAllBundles();
        void CreateBundle(Bundle bundle);
        void EditBundle(Bundle bundle);
        void DeleteBundle(int id);
        void AddPipeToBundle(Bundle bundle, int pipeId);
        void RemovePipeFromBundle(Bundle bundle, int pipeId);
        Bundle GetBundleById(int bundleId);
    }
}

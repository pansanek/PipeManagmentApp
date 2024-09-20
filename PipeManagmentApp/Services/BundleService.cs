using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Service
{

    public class BundleService : IBundleService
    {
        private readonly IBundleRepository _bundleRepository;

        public BundleService(IBundleRepository bundleRepository)
        {
            _bundleRepository = bundleRepository;
        }

        public IEnumerable<Bundle> GetAllBundles()
        {
            return _bundleRepository.AllBundles;
        }

        public void CreateBundle(Bundle bundle)
        {

            _bundleRepository.createBundle(bundle);
        }

        public void EditBundle(Bundle bundle)
        {

            _bundleRepository.editBundle(bundle);
        }

        public void DeleteBundle(int id)
        {
            _bundleRepository.deleteBundle(id);
        }

        public void AddPipeToBundle(Bundle bundle, int pipeId)
        {
            
            if (bundle == null) throw new ArgumentException("Пакет не найден.");

            _bundleRepository.addPipeToBundle(bundle, pipeId);
        }

        public void RemovePipeFromBundle(Bundle bundle, int pipeId)
        {
         
            if (bundle == null) throw new ArgumentException("Пакет не найден.");

            _bundleRepository.deletePipeFromBundle(bundle, pipeId);
        }

        public Bundle GetBundleById(int bundleId)
        {
            var bundle = _bundleRepository.getBundleObj(bundleId);
            if (bundle == null) return null;

            return bundle;
        }
    }
}
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PipeManagmentApp.Data.Repository
{
    public class BundleRepository : IAllBundles
    {
        private readonly AppDbContext _appDbContext;

        public BundleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Получение всех пакетов с включением связанных труб
        public IEnumerable<Bundle> AllBundles => _appDbContext.Bundles.Include(p => p.pipes).ToList();

        // Создание нового пакета
        public void createBundle(Bundle bundle)
        {
            if (bundle != null)
            {
                _appDbContext.Bundles.Add(bundle);
                _appDbContext.SaveChanges();
            }
        }

        // Удаление пакета
        public void deleteBundle(int id)
        {
            var bundle = _appDbContext.Bundles.Find(id);
            if (bundle != null)
            {
                _appDbContext.Bundles.Remove(bundle);
                _appDbContext.SaveChanges();
            }
        }

        // Удаление трубы из пакета
        public void deletePipeFromBundle(Bundle bundle, int pipeId)
        {
            var existingBundle = _appDbContext.Bundles.Include(p => p.pipes).FirstOrDefault(p => p.id == bundle.id);
            if (existingBundle != null)
            {
                var pipeToRemove = existingBundle.pipes.FirstOrDefault(p => p.id == pipeId);
                if (pipeToRemove != null)
                {
                    existingBundle.pipes.Remove(pipeToRemove);
                    _appDbContext.SaveChanges();
                }
            }
        }

        // Редактирование пакета
        public void editBundle(Bundle bundle)
        {
            var existingBundle = _appDbContext.Bundles.Include(p => p.pipes).FirstOrDefault(p => p.id == bundle.id);
            if (existingBundle != null)
            {
                existingBundle.bundleNumber = bundle.bundleNumber;
                existingBundle.bundleDate = bundle.bundleDate;
                existingBundle.pipes = bundle.pipes;
                _appDbContext.SaveChanges();
            }
        }

        public Bundle getBundleObj(int bundleId)
        {
            return _appDbContext.Bundles.Include(p => p.pipes).FirstOrDefault(p => p.id == bundleId);
        }
    }
}


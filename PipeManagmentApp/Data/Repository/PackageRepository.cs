using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PipeManagmentApp.Data.Repository
{
    public class PackageRepository : IAllPackages
    {
        private readonly AppDbContext _appDbContext;

        public PackageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Получение всех пакетов с включением связанных труб
        public IEnumerable<Package> AllPackages => _appDbContext.Packages.Include(p => p.pipes).ToList();

        // Создание нового пакета
        public void createPackage(Package package)
        {
            if (package != null)
            {
                _appDbContext.Packages.Add(package);
                _appDbContext.SaveChanges();
            }
        }

        // Удаление пакета
        public void deletePackage(Package package)
        {
            if (package != null)
            {
                _appDbContext.Packages.Remove(package);
                _appDbContext.SaveChanges();
            }
        }

        // Удаление трубы из пакета
        public void deletePipeFromPackage(Package package, int pipeId)
        {
            var existingPackage = _appDbContext.Packages.Include(p => p.pipes).FirstOrDefault(p => p.id == package.id);
            if (existingPackage != null)
            {
                var pipeToRemove = existingPackage.pipes.FirstOrDefault(p => p.id == pipeId);
                if (pipeToRemove != null)
                {
                    existingPackage.pipes.Remove(pipeToRemove);
                    _appDbContext.SaveChanges();
                }
            }
        }

        // Редактирование пакета
        public void editPackage(Package package)
        {
            var existingPackage = _appDbContext.Packages.Include(p => p.pipes).FirstOrDefault(p => p.id == package.id);
            if (existingPackage != null)
            {
                existingPackage.packageNumber = package.packageNumber;
                existingPackage.packageDate = package.packageDate;
                existingPackage.pipes = package.pipes;
                _appDbContext.SaveChanges();
            }
        }
    }
}


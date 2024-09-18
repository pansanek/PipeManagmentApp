using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Mocks
{
    public class MockPackages : IAllPackages
    {
        private List<Package> _allPackages;

        // Инициализируем тестовые данные для пакетов
        public MockPackages()
        {
            _allPackages = new List<Package>
            {
                new Package
                {
                    id = 1,
                    packageNumber = 1001,
                    packageDate = DateTime.Now.AddDays(-2),
                    pipes = new List<Pipe>
                    {
                        new Pipe { id = 2, number = 1002, quality = "Брак", steelGrade = "A2", dimensions = "120x250", weight = 25.0 },
                        new Pipe { id = 1, number = 1001, quality = "Годная", steelGrade = "A1", dimensions = "100x200", weight = 20.5 }
                    }
                },
                new Package
                {
                    id = 2,
                    packageNumber = 1002,
                    packageDate = DateTime.Now.AddDays(-1),
                    pipes = new List<Pipe>
                    {
                        new Pipe { id = 3, number = 1003, quality = "Годная", steelGrade = "B1", dimensions = "150x300", weight = 30.0 }
                    }
                }
            };
        }

        public IEnumerable<Package> AllPackages => _allPackages;

        public void createPackage(Package package)
        {
            if (package != null)
            {
               
                package.id = _allPackages.Max(p => p.id) + 1;
                _allPackages.Add(package);
            }
        }

        public void deletePackage(Package package)
        {
            if (package != null && _allPackages.Contains(package))
            {
                _allPackages.Remove(package);
            }
        }

        public void deletePipeFromPackage(Package package, int pipeId)
        {
            var existingPackage = _allPackages.FirstOrDefault(p => p.id == package.id);
            if (existingPackage != null)
            {
                var pipeToRemove = existingPackage.pipes.FirstOrDefault(p => p.id == pipeId);
                if (pipeToRemove != null)
                {
                    existingPackage.pipes.Remove(pipeToRemove);
                }
            }
        }

        public void editPackage(Package package)
        {
            var existingPackage = _allPackages.FirstOrDefault(p => p.id == package.id);
            if (existingPackage != null)
            {
                existingPackage.packageNumber = package.packageNumber;
                existingPackage.packageDate = package.packageDate;
                existingPackage.pipes = package.pipes;
            }
        }
    }
}

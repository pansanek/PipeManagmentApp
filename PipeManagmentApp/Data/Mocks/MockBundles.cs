using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data.Mocks
{
    public class MockBundles : IBundleRepository
    {
        private List<Bundle> _allBundle;

        // Инициализируем тестовые данные для пакетов
        public MockBundles()
        {
            _allBundle = new List<Bundle>
            {
                new Bundle
                {
                    id = 1,
                    bundleNumber = 1001,
                    bundleDate = DateTime.Now.AddDays(-2),
                    pipes = new List<Pipe>
                    {
                        new Pipe { id = 2, number = 1002, quality = "Брак", steelGrade = "A2", dimensions = "120x250", weight = 25.0 },
                        new Pipe { id = 1, number = 1001, quality = "Годная", steelGrade = "A1", dimensions = "100x200", weight = 20.5 }
                    }
                },
                new Bundle
                {
                    id = 2,
                    bundleNumber = 1002,
                    bundleDate = DateTime.Now.AddDays(-1),
                    pipes = new List<Pipe>
                    {
                        new Pipe { id = 3, number = 1003, quality = "Годная", steelGrade = "B1", dimensions = "150x300", weight = 30.0 }
                    }
                }
            };
        }

        public IEnumerable<Bundle> AllBundles => _allBundle;

        public void createBundle(Bundle bundle)
        {
            if (bundle != null)
            {
               
                bundle.id = _allBundle.Max(p => p.id) + 1;
                _allBundle.Add(bundle);
            }
        }

        public void deleteBundle(int id)
        {
            var bundle = _allBundle[id];
            if (bundle != null && _allBundle.Contains(bundle))
            {
                _allBundle.Remove(bundle);
            }
        }

        public void deletePipeFromBundle(Bundle bundle, int pipeId)
        {
            var existingBundle = _allBundle.FirstOrDefault(p => p.id == bundle.id);
            if (existingBundle != null)
            {
                var pipeToRemove = existingBundle.pipes.FirstOrDefault(p => p.id == pipeId);
                if (pipeToRemove != null)
                {
                    existingBundle.pipes.Remove(pipeToRemove);
                }
            }
        }

        public void addPipeToBundle(Bundle bundle, int pipeId)
        {
            var existingBundle = _allBundle.FirstOrDefault(p => p.id == bundle.id);
            if (existingBundle != null)
            {
                var pipeToAdd = existingBundle.pipes.FirstOrDefault(p => p.id == pipeId);
                if (pipeToAdd != null)
                {
                    existingBundle.pipes.Add(pipeToAdd);
                }
            }
        }

        public void editBundle(Bundle bundle)
        {
            var existingBundle = _allBundle.FirstOrDefault(p => p.id == bundle.id);
            if (existingBundle != null)
            {
                existingBundle.bundleNumber = bundle.bundleNumber;
                existingBundle.bundleDate = bundle.bundleDate;
                existingBundle.pipes = bundle.pipes;
            }
        }

        public Bundle getBundleObj(int bundleId)
        {
            return _allBundle.FirstOrDefault(p => p.id == bundleId);
        }
    }
}

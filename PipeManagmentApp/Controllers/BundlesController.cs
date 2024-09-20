using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using PipeManagmentApp.ViewModels;
namespace PipeManagmentApp.Controllers
{
    public class BundlesController : Controller
    {
        private readonly IPipeService _pipeService;
        private readonly IBundleService _bundleService;
        public BundlesController(IBundleService bundleService, IPipeService pipeService)
        {
            _pipeService = pipeService;
            _bundleService = bundleService;
        }

    
        public ViewResult Index(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            IEnumerable<Bundle> bundles = _bundleService.GetAllBundles().OrderBy(p => p.id);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                bundles = bundles
                    .Where(p => p.bundleDate.Date >= dateFrom.Value.Date && p.bundleDate.Date <= dateTo.Value.Date)
                    .ToList();
            }
            else if (dateFrom.HasValue) // Фильтр только по "дате от"
            {
                bundles = bundles.Where(p => p.bundleDate.Date >= dateFrom.Value.Date).ToList();
            }
            else if (dateTo.HasValue) // Фильтр только по "дате до"
            {
                bundles = bundles.Where(p => p.bundleDate.Date <= dateTo.Value.Date).ToList();
            }
            var bundleListViewModel = new BundleListViewModel
            {
                AllBundles = bundles.ToList(),
                Title = "Список пакетов",
                IsFilterApplied = dateFrom.HasValue || dateTo.HasValue,
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            return View(bundleListViewModel);
        }
        public IActionResult Delete(int id)
        {
            var bundle = _bundleService.GetBundleById(id);
            if (bundle == null)
            {
                return NotFound();
            }

            var deleteBundleViewModel = new DeleteBundleViewModel
            {
                Bundle = bundle
            };

            ViewBag.Title = "Удаление пакета";
            return View(deleteBundleViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var bundle = _bundleService.GetBundleById(id);
            if (bundle == null)
            {
                return RedirectToAction("Index", "Bundles");
            }

            _bundleService.DeleteBundle(bundle.id);


            return RedirectToAction("Index", "Bundles");
        }

        public IActionResult Edit(int id)
        {
            var bundle = _bundleService.GetBundleById(id);
            if (bundle == null)
            {
                return NotFound();
            }

            var editBundleViewModel = new EditBundleViewModel
            {
                Bundle = bundle,
                AvailablePipes = _pipeService.GetAllPipes().Where(p => p.bundleId == null).ToList(),
                RemovedPipes = new List<int>(),
                AddedPipes = new List<int>()
            };

            ViewBag.Title = "Редактирование пакета";
            return View(editBundleViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, string removedPipes = null, string addedPipes = null)
        {
            var bundle = _bundleService.GetBundleById(id);
            if (ModelState.IsValid)
            {
                if (bundle != null)
                {
                    // Обработка удалённых труб
                    if (!string.IsNullOrEmpty(removedPipes))
                    {
                        var removedPipeIds = removedPipes.Split(',').Select(int.Parse);
                        foreach (var pipeId in removedPipeIds)
                        {
                            _bundleService.RemovePipeFromBundle(bundle, pipeId);
                        }
                    }

                    // Обработка добавленных труб
                    if (!string.IsNullOrEmpty(addedPipes))
                    {
                        var addedPipeIds = addedPipes.Split(',').Select(int.Parse);
                        foreach (var pipeId in addedPipeIds)
                        {
                            _bundleService.AddPipeToBundle(bundle, pipeId);
                        }
                    }

                 
                    _bundleService.EditBundle(bundle);

                    return RedirectToAction("Index", "Bundles");
                }
            }
            if (bundle == null)
            {
                return NotFound();
            }
            var pipeService = _pipeService.GetAllPipes()
                .Where(p => p.bundleId == null)
                   .ToList();
           

            var editBundleViewModel = new EditBundleViewModel
            {
                Bundle = bundle,
                AvailablePipes = _pipeService.GetAllPipes().Where(p => p.bundleId == null).ToList(),
                RemovedPipes = new List<int>(),
                AddedPipes = new List<int>()
            };

            ViewBag.Title = "Редактирование пакета";
            return View(editBundleViewModel);
        }
        public IActionResult Create(string selectedPipes = null)
        {
            var createBundleViewModel = new CreateBundleViewModel
            {
                Bundle = new Bundle(),
                AvailablePipes = _pipeService.GetAllPipes().Where(p => p.bundleId == null).ToList(),
                AddedPipes = string.IsNullOrEmpty(selectedPipes) ? new List<int>() : selectedPipes.Split(',').Select(int.Parse).ToList()
            };

            ViewBag.Title = "Создание пакета";
            return View(createBundleViewModel);
        }
        [HttpPost]
        public IActionResult Create(Bundle bundle, string addedPipes = null)
        {
            if (ModelState.IsValid)
            {
                
                _bundleService.CreateBundle(bundle); 

                if (!string.IsNullOrEmpty(addedPipes))
                {
                    var addedPipeIds = addedPipes.Split(',').Select(int.Parse);
                    foreach (var pipeId in addedPipeIds)
                    {
                        _bundleService.AddPipeToBundle(bundle, pipeId); 
                    }
                }

                return RedirectToAction("Index", "Bundles");
            }

            var pipeService = _pipeService.GetAllPipes()
                .Where(p => p.bundleId == null)
                .ToList();

            var createBundleViewModel = new CreateBundleViewModel
            {
                Bundle = new Bundle(),
                AvailablePipes = _pipeService.GetAllPipes().Where(p => p.bundleId == null).ToList(),
                AddedPipes = string.IsNullOrEmpty(addedPipes) ? new List<int>() : addedPipes.Split(',').Select(int.Parse).ToList()
            };

            ViewBag.Title = "Создание пакета";

            return View(createBundleViewModel);
        }
       
    }
}

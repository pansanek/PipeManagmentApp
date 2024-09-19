using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using PipeManagmentApp.ViewModels;
namespace PipeManagmentApp.Controllers
{
    public class BundlesController : Controller
    {
        private readonly IAllBundles _allBundles;
        private readonly IAllPipes _allPipes;

        public BundlesController(IAllBundles allBundles,IAllPipes allPipes)
        {
            _allBundles = allBundles;
            _allPipes = allPipes;
        }

    
        public ViewResult Index(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            IEnumerable<Bundle> bundles = _allBundles.AllBundles.OrderBy(p => p.id);

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
                allBundles = bundles
            };

            ViewBag.Title = "Список пакетов";
            return View(bundleListViewModel);
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Удаление трубы";
            var bundle = _allBundles.getBundleObj(id);
            if (bundle == null)
            {
                return NotFound();
            }
            return View(bundle);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var bundle = _allBundles.getBundleObj(id);
            if (bundle == null)
            {
                return RedirectToAction("Index", "Bundles");
            }

            _allBundles.deleteBundle(bundle.id);


            return RedirectToAction("Index", "Bundles");
        }

        //public IActionResult Edit(int id)
        //{
        //    ViewBag.Title = "Редактирование пакета";
        //    var bundle = _allBundles.getBundleObj(id);
        //    if (bundle == null)
        //    {
        //        return NotFound();
        //    }
        //    // Get all pipes except those already in the bundle
        //    var allPipes = _allPipes.AllPipes.Where(p => !bundle.pipes.Any(pp => pp.id == p.id)).ToList();
        //    ViewBag.AvailablePipes = allPipes;

        //    return View(bundle);
        //}

        //[HttpPost]
        //public IActionResult Edit(Bundle bundle, string removedPipes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Process removed pipes
        //        if (!string.IsNullOrEmpty(removedPipes))
        //        {
        //            var removedPipeIds = removedPipes.Split(',').Select(int.Parse);
        //            foreach (var pipeId in removedPipeIds)
        //            {
        //                _allBundles.deletePipeFromBundle(bundle, pipeId);
        //            }
        //        }

        //        // Process added pipes
        //        var existingBundle = _allBundles.getBundleObj(bundle.id);
        //        if (existingBundle != null)
        //        {
        //            // Update bundle properties
        //            existingBundle.bundleNumber = bundle.bundleNumber;
        //            existingBundle.bundleDate = bundle.bundleDate;

        //            // Save the updated bundle
        //            _allBundles.editBundle(existingBundle);
        //        }

        //        return RedirectToAction("Index", "Bundles");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewBag.Title = "Редактирование пакета";
        //    var allPipes = _allPipes.AllPipes.Where(p => !bundle.pipes.Any(pp => pp.id == p.id)).ToList();
        //    ViewBag.AvailablePipes = allPipes;
        //    return View(bundle);
        //}
        //public IActionResult Create(List<int> selectedPipes)
        //{
        //    if (selectedPipes == null || selectedPipes.Count < 2)
        //    {
        //        return RedirectToAction("Index", "Pipes"); // Или другой подходящий маршрут
        //    }


        //    var model = new Bundle
        //    {

        //        pipes = pipes
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Create(Bundle Bundle)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.PipeBundles.Add(pipeBundle);
        //        foreach (var pipe in pipeBundle.Pipes)
        //        {
        //            pipe.BundleId = pipeBundle.Id;
        //        }
        //        _context.SaveChanges();
        //        return RedirectToAction("Index", "Pipes");
        //    }
        //    return View(pipeBundle);
        //}
    }
}

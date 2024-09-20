using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using PipeManagmentApp.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
            ViewBag.IsFilterApplied = !(dateFrom ==null) || !(dateTo == null);
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

        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Редактирование пакета";
            var bundle = _allBundles.getBundleObj(id);
            if (bundle == null)
            {
                return NotFound();
            }

            // Получение всех труб, которые не включены в текущий пакет
            var allPipes = _allPipes.AllPipes
                .Where(p => p.bundleId == null)
                   .ToList();
            ViewBag.AvailablePipes = allPipes;

            return View(bundle);
        }

        [HttpPost]
        public IActionResult Edit(int id, string removedPipes = null, string addedPipes = null)
        {
            var bundle = _allBundles.getBundleObj(id);
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
                            _allBundles.deletePipeFromBundle(bundle, pipeId);
                        }
                    }

                    // Обработка добавленных труб
                    if (!string.IsNullOrEmpty(addedPipes))
                    {
                        var addedPipeIds = addedPipes.Split(',').Select(int.Parse);
                        foreach (var pipeId in addedPipeIds)
                        {
                            _allBundles.addPipeToBundle(bundle, pipeId);
                        }
                    }

                    // Сохранение изменений
                    _allBundles.editBundle(bundle);

                    return RedirectToAction("Index", "Bundles");
                }
            }
            if (bundle == null)
            {
                return NotFound();
            }
            var allPipes = _allPipes.AllPipes
                .Where(p => p.bundleId == null)
                   .ToList();
            ViewBag.Title = "Редактирование пакета";
            ViewBag.AvailablePipes = allPipes;
            return View(bundle);
        }
        public IActionResult Create(string selectedPipes = null)
        {
            ViewBag.Title = "Создание пакета";

            // Получение всех труб, которые не находятся в пакетах
            var allPipes = _allPipes.AllPipes
                .Where(p => p.bundleId == null)
                .ToList();

            ViewBag.AvailablePipes = allPipes;

            ViewBag.AddedPipes = string.IsNullOrEmpty(selectedPipes)
                ? new List<int>()
                : selectedPipes.Split(',').Select(int.Parse).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Bundle bundle, string addedPipes = null)
        {
            if (ModelState.IsValid)
            {
                // Сохранение нового пакета
                _allBundles.createBundle(bundle); // Метод для добавления нового пакета

                // Обработка добавленных труб
                if (!string.IsNullOrEmpty(addedPipes))
                {
                    var addedPipeIds = addedPipes.Split(',').Select(int.Parse);
                    foreach (var pipeId in addedPipeIds)
                    {
                        _allBundles.addPipeToBundle(bundle, pipeId); // Добавляем трубы в пакет
                    }
                }

                return RedirectToAction("Index", "Bundles");
            }

            // Если валидация не прошла, нужно заново загрузить трубы, которые не находятся в пакетах
            var allPipes = _allPipes.AllPipes
                .Where(p => p.bundleId == null)
                .ToList();

            ViewBag.Title = "Создание пакета";
            ViewBag.AvailablePipes = allPipes;

            return View(bundle);
        }
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

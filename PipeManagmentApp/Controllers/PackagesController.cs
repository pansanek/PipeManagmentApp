using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using PipeManagmentApp.ViewModels;
namespace PipeManagmentApp.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IAllPackages _allPackages;

        public PackagesController(IAllPackages allPackages)
        {
            _allPackages = allPackages;
        }

    
        public ViewResult Index()
        {
            IEnumerable<Package> packages = _allPackages.AllPackages.OrderBy(p => p.id);

            var packageListViewModel = new PackageListViewModel
            {
                allPackages = packages
            };

            ViewBag.Title = "Список пакетов";
            return View(packageListViewModel);
        }

        //public IActionResult Create(List<int> selectedPipes)
        //{
        //    if (selectedPipes == null || selectedPipes.Count < 2)
        //    {
        //        return RedirectToAction("Index", "Pipes"); // Или другой подходящий маршрут
        //    }

    
        //    var model = new Package
        //    {

        //        pipes = pipes
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Create(Package Package)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.PipePackages.Add(pipePackage);
        //        foreach (var pipe in pipePackage.Pipes)
        //        {
        //            pipe.PackageId = pipePackage.Id;
        //        }
        //        _context.SaveChanges();
        //        return RedirectToAction("Index", "Pipes");
        //    }
        //    return View(pipePackage);
        //}
    }
}

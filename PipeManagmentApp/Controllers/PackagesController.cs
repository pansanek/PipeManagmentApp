using Microsoft.AspNetCore.Mvc;
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

        //[Route("Packages/Details/{id}")]
        //public ViewResult Details(int id)
        //{
        //    var package = _allPackages.AllPackages.FirstOrDefault(p => p.id == id);

        //    if (package == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var packageDetailsViewModel = new PackageDetailsViewModel
        //    {
        //        Package = package,
        //        PipesInPackage = package.pipes
        //    };

        //    ViewBag.Title = $"Детали пакета №{package.packageNumber}";
        //    return View(packageDetailsViewModel);
        //}
    }
}

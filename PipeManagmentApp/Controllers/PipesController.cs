using Microsoft.AspNetCore.Mvc;
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using PipeManagmentApp.ViewModels;
namespace PipeManagmentApp.Controllers
{
    public class PipesController : Controller
    {
        private readonly IAllPipes _allPipes;

        public PipesController(IAllPipes iAllPipes)
        {
            _allPipes = iAllPipes;
        }


       
        public ViewResult List()
        {
            // Получаем все трубы, отсортированные по идентификатору
            IEnumerable<Pipe> pipes = _allPipes.AllPipes.OrderBy(p => p.id);

            PipesListViewModel model = new PipesListViewModel();
            model.allPipes = pipes;


            ViewBag.Title = "Список всех труб"; 
            return View(model); 
        }
    }
}

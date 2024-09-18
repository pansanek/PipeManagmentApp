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
            int totalPipes = pipes.Count();
            int goodPipes = pipes.Count(p => p.quality == "Годная");
            int defectivePipes = pipes.Count(p => p.quality == "Брак");
            double totalWeight = pipes.Sum(p => p.weight);
            var pipeListViewModel = new PipesListViewModel
            {
                allPipes = pipes,
                totalPipes = totalPipes,
                goodPipes = goodPipes,
                defectivePipes = defectivePipes,
                totalWeight = totalWeight
            };

            ViewBag.Title = "Список всех труб";
            return View(pipeListViewModel);
        }


        [HttpGet]
        public JsonResult GetPipeById(int id)
        {
            var pipe = _allPipes.getPipeObj(id);
            if (pipe == null)
            {
                return Json(new { error = "Труба не найдена" });
            }
            return Json(pipe);
        }
        // Создание новой трубы
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pipe pipe)
        {
            if (ModelState.IsValid)
            {
                _allPipes.createPipe(pipe);
                return RedirectToAction("List");
            }
            return View(pipe);
        }

        // Редактирование существующей трубы
        public IActionResult Edit(int id)
        {
            var pipe = _allPipes.getPipeObj(id);
            if (pipe == null)
            {
                return NotFound();
            }
            return View(pipe);
        }

        [HttpPost]
        public IActionResult Edit(Pipe pipe)
        {
            if (ModelState.IsValid)
            {
                _allPipes.editPipe(pipe);
                return RedirectToAction("List");
            }
            return View(pipe);
        }

        // Удаление трубы
        public IActionResult Delete(int id)
        {
            var pipe = _allPipes.getPipeObj(id);
            if (pipe == null)
            {
                return NotFound();
            }
            return View(pipe);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var pipe = _allPipes.getPipeObj(id);
            if (pipe != null)
            {
                _allPipes.deletePipe(pipe);
            }
            return RedirectToAction("List");
        }
    }
}

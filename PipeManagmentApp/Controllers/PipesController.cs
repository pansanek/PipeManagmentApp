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

  

        public ViewResult Index(string number = null, string quality = null, string steelGrade = null)
        {
            // Получаем все трубы
            IEnumerable<Pipe> pipes = _allPipes.AllPipes;

            // Применяем фильтры
            if (!string.IsNullOrEmpty(number))
            {
                int pipeNumber;
                if (int.TryParse(number, out pipeNumber))
                {
                    pipes = pipes.Where(p => p.number == pipeNumber);
                }
            }

            if (!string.IsNullOrEmpty(quality))
            {
                pipes = pipes.Where(p => p.quality.Equals(quality, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(steelGrade))
            {
                pipes = pipes.Where(p => p.steelGrade.Contains(steelGrade, StringComparison.OrdinalIgnoreCase));
            }

            // Вычисляем итоговые данные
            int totalPipes = pipes.Count();
            int goodPipes = pipes.Count(p => p.quality == "Годная");
            int defectivePipes = pipes.Count(p => p.quality == "Брак");
            double totalWeight = pipes.Sum(p => p.weight);

            // Формируем модель для передачи в представление
            var pipeListViewModel = new PipesListViewModel
            {
                allPipes = pipes,
                totalPipes = totalPipes,
                goodPipes = goodPipes,
                defectivePipes = defectivePipes,
                totalWeight = totalWeight
            };

            // Передача значений фильтров во ViewBag для отображения в форме
            ViewBag.FilterNumber = number;
            ViewBag.FilterQuality = quality;
            ViewBag.FilterSteelGrade = steelGrade;

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

        public IActionResult Create()
        {
            ViewBag.Title = "Добавление трубы";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pipe pipe)
        {
            if (ModelState.IsValid) 
            {
                
                _allPipes.createPipe(pipe); 

                return RedirectToAction("Index", "Pipes"); 
            }

            ViewBag.Title = "Добавление трубы";
            return View(pipe); // Если данные некорректны, возвращаем форму с ошибками
        }

        // Редактирование существующей трубы
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Редактирование трубы";
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
                return RedirectToAction("Index", "Pipes");
            }
            ViewBag.Title = "Редактирование трубы";
            return View(pipe); // Если данные некорректны, возвращаем форму с ошибками
        }

        // Удаление трубы
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Удаление трубы";
            var pipe = _allPipes.getPipeObj(id);
            if (pipe == null)
            {
                return NotFound();
            }
            return View(pipe);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var pipe = _allPipes.getPipeObj(id);
            if (pipe == null)
            {
                return RedirectToAction("Index", "Pipes"); 
            }

            _allPipes.deletePipe(pipe.id);

           
            return RedirectToAction("Index", "Pipes"); 
        }



    }
}

using Microsoft.AspNetCore.Mvc;
using PipeManagmentApp.Data.Interfaces;
using PipeManagmentApp.Data.Models;
using PipeManagmentApp.ViewModels;
using System.Reflection.Metadata;
namespace PipeManagmentApp.Controllers
{
    public class PipesController : Controller
    {
        private readonly IPipeService _pipeService;

        public PipesController(IPipeService pipeService)
        {
            _pipeService = pipeService;
        }

  

        public ViewResult Index(string number = null, string quality = null, string steelGrade = null)
        {
            // Получаем все трубы
            IEnumerable<Pipe> pipes = _pipeService.GetAllPipes();

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
            double? totalWeight = pipes.Sum(p => p.weight);

            // Формируем модель для передачи в представление
            var pipeListViewModel = new PipesListViewModel
            {
                AllPipes = pipes,
                TotalPipes = totalPipes,
                GoodPipes = goodPipes,
                DefectivePipes = defectivePipes,
                TotalWeight = totalWeight,
                FilterNumber = number,
                FilterQuality = quality,
                FilterSteelGrade = steelGrade,
                Title = "Список всех труб",
                IsFilterApplied = !string.IsNullOrEmpty(number) || !string.IsNullOrEmpty(quality) || !string.IsNullOrEmpty(steelGrade)
            };

            return View(pipeListViewModel);
        }


        [HttpGet]
        public JsonResult GetPipeById(int id)
        {
            var pipe = _pipeService.GetPipeById(id);
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
                
                _pipeService.CreatePipe(pipe); 

                return RedirectToAction("Index", "Pipes"); 
            }
            var createPipeViewModel = new CreatePipeViewModel
            {
                Pipe = pipe
            };
            ViewBag.Title = "Добавление трубы";
            return View(createPipeViewModel); // Если данные некорректны, возвращаем форму с ошибками
        }

        // Редактирование существующей трубы
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Редактирование трубы";
            var pipe = _pipeService.GetPipeById(id);
            if (pipe == null)
            {
                return NotFound();
            }
            var editPipeViewModel = new EditPipeViewModel
            {
                Pipe = pipe
            };
            return View(editPipeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(Pipe pipe)
        {
            if (ModelState.IsValid)
            {
                _pipeService.EditPipe(pipe);
                return RedirectToAction("Index", "Pipes");
            }
            ViewBag.Title = "Редактирование трубы";
            var editPipeViewModel = new EditPipeViewModel
            {
                Pipe = pipe
            };
            return View(editPipeViewModel); 
        }

        // Удаление трубы
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Удаление трубы";
            var pipe = _pipeService.GetPipeById(id);
            if (pipe == null)
            {
                return NotFound();
            }

            var deletePipeViewModel = new DeletePipeViewModel
            {
                Pipe = pipe
            };
            return View(deletePipeViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var pipe = _pipeService.GetPipeById(id);
            if (pipe == null)
            {
                return RedirectToAction("Index", "Pipes"); 
            }

            _pipeService.DeletePipe(pipe.id);

           
            return RedirectToAction("Index", "Pipes"); 
        }


        [HttpPost]
        public IActionResult CreateBundle(List<int> selectedPipes)
        {
            if (selectedPipes == null || selectedPipes.Count < 2)
            {
                // Можно вернуть ошибку или перенаправить на страницу с предупреждением
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create", "Bundles", new { selectedPipes = string.Join(",", selectedPipes) });
        }
    }
}

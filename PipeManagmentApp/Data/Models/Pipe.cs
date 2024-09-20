using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PipeManagmentApp.Data.Models
{
    public class Pipe
    {
        public int id { get; set; }

        [Display(Name = "Введите номер трубы")]
        [Required(ErrorMessage = "Номер трубы обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Номер трубы должен быть положительным числом")]
        public int? number { get; set; }
        [Display(Name = "Введите качество трубы")]
        [Required(ErrorMessage = "Качество обязательно")]
        [RegularExpression("^(Брак|Годное)$", ErrorMessage = "Допустимые значения: Брак или Годное")]
        public string quality { get; set; }
        [Display(Name = "Введите марку стали трубы")]
        [Required(ErrorMessage = "Марка стали обязательна")]
        public string steelGrade { get; set; }

        [Display(Name = "Введите размеры трубы")]
        [RegularExpression(@"^\d+x\d+$", ErrorMessage = "Размеры должны быть в формате 'число'x'число', например, 300x500")]
        [Required(ErrorMessage = "Размеры обязательны")]
        public string dimensions { get; set; }

        [Display(Name = "Введите вес трубы")]
        [Required(ErrorMessage = "Вес обязателен")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Вес должен быть положительным числом")]
        public double? weight { get; set; }

        public int? bundleId { get; set; }
        public Bundle? bundle { get; set; }
    }
}

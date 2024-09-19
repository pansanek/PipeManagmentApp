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
        public int number { get; set; }
        [Display(Name = "Введите качество трубы")]
        [Required(ErrorMessage = "Качество обязательно")]
        public string quality { get; set; }
        [Display(Name = "Введите марку стали трубы")]
        [Required(ErrorMessage = "Марка стали обязательна")]
        public string steelGrade { get; set; }

        [Display(Name = "Введите размеры трубы")]
        [Required(ErrorMessage = "Размеры обязательны")]
        public string dimensions { get; set; }

        [Display(Name = "Введите вес трубы")]
        [Required(ErrorMessage = "Вес обязателен")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Вес должен быть положительным числом")]
        public double weight { get; set; }

        public int? packageId { get; set; }
        public Package? package { get; set; }
    }
}

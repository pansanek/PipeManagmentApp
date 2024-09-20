using System.ComponentModel.DataAnnotations;

namespace PipeManagmentApp.Data.Models
{
    public class Bundle
    {
        public int id { get; set; }

        [Display(Name = "Введите номер пакета")]
        [Required(ErrorMessage = "Номер пакета обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Номер пакета должен быть положительным числом")]
        public int? bundleNumber { get; set; }

        private DateTime _bundleDate;

        [Display(Name = "Введите дату пакета")]
        [Required(ErrorMessage = "Дата пакета обязательна")]
        [DataType(DataType.Date, ErrorMessage = "Введите корректную дату пакета")]
        public DateTime bundleDate
        {
            get => _bundleDate;
            set => _bundleDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public List<Pipe> pipes { get; set; } = new List<Pipe>();
    }
}

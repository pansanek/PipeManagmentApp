namespace PipeManagmentApp.Data.Models
{
    public class Bundle
    {
        public int id { get; set; }

        public int bundleNumber { get; set; }

        private DateTime _bundleDate;

        public DateTime bundleDate
        {
            get => _bundleDate;
            set => _bundleDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public List<Pipe> pipes { get; set; }
    }
}

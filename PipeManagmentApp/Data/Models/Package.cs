namespace PipeManagmentApp.Data.Models
{
    public class Package
    {
        public int id { get; set; }

        public int packageNumber { get; set; }

        private DateTime _packageDate;

        public DateTime packageDate
        {
            get => _packageDate;
            set => _packageDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public List<Pipe> pipes { get; set; }
    }
}

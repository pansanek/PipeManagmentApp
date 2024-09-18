namespace PipeManagmentApp.Data.Models
{
    public class Package
    {
        public int id { get; set; }

        public int packageNumber { get; set; }

        public DateTime packageDate { get; set; }

        public List<Pipe> pipes { get; set; }
    }
}

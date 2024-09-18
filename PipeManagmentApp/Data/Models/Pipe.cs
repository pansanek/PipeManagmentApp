namespace PipeManagmentApp.Data.Models
{
    public class Pipe
    {
        public int id { get; set; }

        public int number { get; set; }

        public string quality { get; set; }

        public string steelGrade { get; set; }

        public string dimensions { get; set; }

        public double weight { get; set; }

        public int packageId { get; set; }

        public Package package { get; set; }
    }
}

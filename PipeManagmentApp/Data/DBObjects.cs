using PipeManagmentApp.Data.Models;

namespace PipeManagmentApp.Data
{
    public class DBObjects
    {
        public static void Initialize(AppDbContext context)
        {
            // Проверка, существует ли что-то в таблице Pipes
            if (!context.Pipes.Any())
            {
                context.Pipes.AddRange(
                
                    new Pipe { number = 1004, quality = "Годная", steelGrade = "A1", dimensions = "100x200", weight = 30.0 }
                );
            }

            // Проверка, существует ли что-то в таблице Bundles
            if (!context.Bundles.Any())
            {
                context.Bundles.AddRange(
                    new Bundle
                    {
                        id = 1,
                        bundleNumber = 1001,
                        bundleDate = DateTime.UtcNow.AddDays(-2),
                        pipes = new List<Pipe>
                        {
                    new Pipe { number = 1001, quality = "Годная", steelGrade = "С", dimensions = "100x200", weight = 20.5 },
                    new Pipe { number = 1002, quality = "Брак", steelGrade = "К", dimensions = "120x250", weight = 25.0 }
                        }
                    },
                    new Bundle
                    {
                        id = 2,
                        bundleNumber = 1002,
                        bundleDate = DateTime.UtcNow.AddDays(-1),
                        pipes = new List<Pipe>
                        {
                    new Pipe { number = 1003, quality = "Годная", steelGrade = "Л", dimensions = "150x300", weight = 30.0 }
                        }
                    }
                );
            }

            // Сохранение изменений в базе данных
            context.SaveChanges();
        }
    }
}

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
                
                    new Pipe { id = 4, number = 1004, quality = "Годная", steelGrade = "A1", dimensions = "100x200", weight = 30.0 }
                );
            }

            // Проверка, существует ли что-то в таблице Packages
            if (!context.Packages.Any())
            {
                context.Packages.AddRange(
                    new Package
                    {
                        id = 1,
                        packageNumber = 1001,
                        packageDate = DateTime.UtcNow.AddDays(-2),
                        pipes = new List<Pipe>
                        {
                    new Pipe { id = 1, number = 1001, quality = "Годная", steelGrade = "A1", dimensions = "100x200", weight = 20.5 },
                    new Pipe { id = 2, number = 1002, quality = "Брак", steelGrade = "A2", dimensions = "120x250", weight = 25.0 }
                        }
                    },
                    new Package
                    {
                        id = 2,
                        packageNumber = 1002,
                        packageDate = DateTime.UtcNow.AddDays(-1),
                        pipes = new List<Pipe>
                        {
                    new Pipe { id = 3, number = 1003, quality = "Годная", steelGrade = "B1", dimensions = "150x300", weight = 30.0 }
                        }
                    }
                );
            }

            // Сохранение изменений в базе данных
            context.SaveChanges();
        }
    }
}

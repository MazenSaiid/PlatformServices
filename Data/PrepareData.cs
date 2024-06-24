using Microsoft.Extensions.Hosting;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepareData
    {

        public static void PreparePopulation(IApplicationBuilder application)
        {
            using(var serviceScope = application.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
            }
        }
        private static void SeedData(ApplicationDbContext context)
        {
            if(!context.Platforms.Any()) 
            {
                Console.WriteLine("Seeding data...!");

                context.Platforms.AddRange(
                new Platform()
                {
                    Name = ".NET",
                    Publisher = "Microsoft",
                    Cost = "FREE"
                }, new Platform()
                {
                    Name = "SQL SERVER",
                    Publisher = "Microsoft",
                    Cost = "FREE"
                }, new Platform()
                {
                    Name = "Docker",
                    Publisher = "Cloud Native Computing Foundation",
                    Cost = "FREE"
                }, new Platform()
                {
                    Name = "Kubernetes",
                    Publisher = "Cloud Native Computing Foundation",
                    Cost = "FREE"
                });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data!");
            }
        }
    }
}

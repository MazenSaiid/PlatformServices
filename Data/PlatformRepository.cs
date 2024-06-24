using Microsoft.AspNetCore.Http.HttpResults;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _context;
        public PlatformRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            _context.Add(platform);
            SaveChanges();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            var platform = _context.Platforms.FirstOrDefault(p => p.Id == id);
            if (platform == null)
            {
                throw new KeyNotFoundException($"Platform with id {id} was not found.");
            }
            return platform;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

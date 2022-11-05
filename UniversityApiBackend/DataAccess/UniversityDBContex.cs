using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContex : DbContext
    {
        public UniversityDBContex(DbContextOptions<UniversityDBContex> options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
    }
}

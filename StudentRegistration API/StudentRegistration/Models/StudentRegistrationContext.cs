using Microsoft.EntityFrameworkCore;

namespace StudentRegistration.Models
{
    public class StudentRegistrationContext : DbContext
    {
        public StudentRegistrationContext(DbContextOptions<StudentRegistrationContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}

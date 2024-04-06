using Microsoft.EntityFrameworkCore;
using StudentRegistration.Models;

namespace StudentRegistration.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentRegistrationContext context;

        public StudentRepository(StudentRegistrationContext context) 
        {
            this.context = context;
        }

        public async Task<Student> AddStudent(Student request)
        {
            var student = await context.Students.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudent(studentId);

            if (student != null) 
            {
                context.Students.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }

            return null;
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Students.AnyAsync(x=>x.Id == studentId);
        }

        public async Task<List<Gender>> GetGenders()
        {
            return await context.Genders.ToListAsync();
        }

        public async Task<Student> GetStudent(Guid studentId)
        {
            return await context.Students
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Student>> GetStudents()
        {
            return await context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudent(studentId);
            if (existingStudent != null) 
            {
                existingStudent.FirstName= request.FirstName;
                existingStudent.LastName= request.LastName;
                existingStudent.Email= request.Email;
                existingStudent.DateOfBirth= request.DateOfBirth;
                existingStudent.Mobile= request.Mobile;
                existingStudent.GenderId= request.GenderId;
                existingStudent.Address.PhysicalAddress= request.Address.PhysicalAddress;

                await context.SaveChangesAsync();
                return existingStudent;
            }

            return null;
        }
    }
}

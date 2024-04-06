using StudentRegistration.Models;

namespace StudentRegistration.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();

        Task<Student> GetStudent(Guid studentId);

        Task<bool> Exists(Guid studentId);

        Task<List<Gender>> GetGenders();

        Task<Student> UpdateStudent(Guid studentId, Student request);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> AddStudent(Student request);
    }
}

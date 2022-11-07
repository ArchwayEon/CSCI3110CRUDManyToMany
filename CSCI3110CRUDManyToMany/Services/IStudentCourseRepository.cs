using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public interface IStudentCourseRepository
{
    Task<StudentCourseGrade?> ReadAsync(int id);
    Task<ICollection<StudentCourseGrade>> ReadAllAsync();
    Task<StudentCourseGrade?> CreateAsync(string enumber, int courseId);
    Task UpdateStudentGradeAsync(int studentCourseId, string letterGrade);
    Task RemoveAsync(string enumber, int studentCourseId);
}



using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public interface IStudentCourseRepository
{
    StudentCourseGrade? Read(int id);
    ICollection<StudentCourseGrade> ReadAll();
    StudentCourseGrade? Create(string enumber, int courseId);
}



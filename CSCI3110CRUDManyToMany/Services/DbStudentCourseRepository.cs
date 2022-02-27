using CSCI3110CRUDManyToMany.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110CRUDManyToMany.Services;

public class DbStudentCourseRepository : IStudentCourseRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;

    public DbStudentCourseRepository(
        ApplicationDbContext db, 
        IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _db = db;
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }

    public StudentCourseGrade? Read(int id)
    {
        return _db.StudentCourseGrades
           .Include(scg => scg.Student)
              .ThenInclude(s => s!.Internship)
           .Include(scg => scg.Course)
           .FirstOrDefault(scg => scg.Id == id);
    }

    public ICollection<StudentCourseGrade> ReadAll()
    {
        return _db.StudentCourseGrades
           .Include(scg => scg.Student)
              .ThenInclude(s => s!.Internship)
           .Include(scg => scg.Course)
           .ToList();
    }

    public StudentCourseGrade? Create(string enumber, int courseId)
    {
        var student = _studentRepo.Read(enumber);
        if(student == null)
        {
            // The student was not found
            return null;
        }
        var courseGrade = student.CourseGrades
            .FirstOrDefault(scg => scg.CourseId == courseId);
        if (courseGrade != null)
        {
            // The student already has a course grade for this course
            return null;
        }
        var course = _courseRepo.Read(courseId);
        if(course == null)
        {
            // The course was not found
            return null;
        }
        var studentCourseGrade = new StudentCourseGrade
        {
            Student = student,
            Course = course
        };
        student.CourseGrades.Add(studentCourseGrade);
        course.StudentGrades.Add(studentCourseGrade);
        _db.SaveChanges();
        return studentCourseGrade;
    }

    public void UpdateStudentGrade(int studentCourseId, string letterGrade)
    {
        var studentCourse = Read(studentCourseId);
        if(studentCourse != null)
        {
            studentCourse.LetterGrade = letterGrade;
            _db.SaveChanges();
        }
    }
}


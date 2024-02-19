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

    public async Task<StudentCourseGrade?> ReadAsync(int id)
    {
        return await _db.StudentCourseGrades
           .Include(scg => scg.Student)
              .ThenInclude(s => s!.Internship)
           .Include(scg => scg.Course)
           .FirstOrDefaultAsync(scg => scg.Id == id);
    }

    public async Task<ICollection<StudentCourseGrade>> ReadAllAsync()
    {
        return await _db.StudentCourseGrades
           .Include(scg => scg.Student)
              .ThenInclude(s => s!.Internship)
           .Include(scg => scg.Course)
           .ToListAsync();
    }

    public async Task<StudentCourseGrade?> CreateAsync(string enumber, int courseId)
    {
        var student = await _studentRepo.ReadAsync(enumber);
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
        var course = await _courseRepo.ReadAsync(courseId);
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
        await _db.SaveChangesAsync();
        return studentCourseGrade;
    }

    public async Task UpdateStudentGradeAsync(
        int studentCourseId, string letterGrade)
    {
        var studentCourse = await ReadAsync(studentCourseId);
        if(studentCourse != null)
        {
            studentCourse.LetterGrade = letterGrade;
            await _db.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(string enumber, int studentCourseId)
    {
        var student = await _studentRepo.ReadAsync(enumber);
        var studentCourse = student!.CourseGrades
            .FirstOrDefault(scg => scg.Id == studentCourseId);
        var course = studentCourse!.Course;
        student!.CourseGrades.Remove(studentCourse);
        course!.StudentGrades.Remove(studentCourse);
        await _db.SaveChangesAsync();
    }
}


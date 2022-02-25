using CSCI3110CRUDManyToMany.Models.ViewModels;
using CSCI3110CRUDManyToMany.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110CRUDManyToMany.Controllers;

public class StudentCourseController : Controller
{
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly IStudentCourseRepository _studentCourseRepo;

    public StudentCourseController(
        IStudentRepository studentRepo, 
        ICourseRepository courseRepo, 
        IStudentCourseRepository studentCourseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
        _studentCourseRepo = studentCourseRepo;
    }
    public IActionResult Create([Bind(Prefix ="id")]string ENumber, int courseId)
    {
        var student = _studentRepo.Read(ENumber);
        if(student == null)
        {
            return RedirectToAction("Index", "Student");
        }
        var course = _courseRepo.Read(courseId);
        if(course == null)
        {
            return RedirectToAction("Details", "Student", new {id = ENumber});
        }
        var studentCourse = student.CourseGrades
            .SingleOrDefault(scg => scg.CourseId == courseId);
        if(studentCourse != null)
        {
            return RedirectToAction("Details", "Student", new { id = ENumber });
        }
        var studentCourseVM = new StudentCourseVM
        {
            Student = student,
            Course = course
        };
        return View(studentCourseVM);
    }

    [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
    public IActionResult CreateConfirmed(string ENumber, int courseId)
    {
        _studentCourseRepo.Create(ENumber, courseId);
        return RedirectToAction("Details", "Student", new { id = ENumber });
    }
}


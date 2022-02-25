using CSCI3110CRUDManyToMany.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110CRUDManyToMany.Controllers;

public class CourseController : Controller
{
    private IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;

    public CourseController(IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }

    public IActionResult Register([Bind(Prefix ="id")]string studentId)
    {
        var student = _studentRepo.Read(studentId);
        if(student == null)
        {
            return RedirectToAction("Index", "Student");
        }
        var allCourses = _courseRepo.ReadAll();
        var coursesRegistered = student.CourseGrades
            .Select(scg => scg.Course).ToList();
        var coursesNotRegistered = allCourses.Except(coursesRegistered);
        ViewData["Student"] = student;
        return View(coursesNotRegistered);
    }
}


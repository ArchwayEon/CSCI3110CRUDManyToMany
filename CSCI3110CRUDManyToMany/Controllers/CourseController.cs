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

    public async Task<IActionResult> Index()
    {
        var allCourses = await _courseRepo.ReadAllAsync();
        return View(allCourses);
    }

    public async Task<IActionResult> Register(
        [Bind(Prefix ="id")]string studentId)
    {
        var student = await _studentRepo.ReadAsync(studentId);
        if(student == null)
        {
            return RedirectToAction("Index", "Student");
        }
        var allCourses = await _courseRepo.ReadAllAsync();
        var coursesRegistered = student.CourseGrades
            .Select(scg => scg.Course).ToList();
        var coursesNotRegistered = allCourses.Except(coursesRegistered);
        ViewData["Student"] = student;
        return View(coursesNotRegistered);
    }
}


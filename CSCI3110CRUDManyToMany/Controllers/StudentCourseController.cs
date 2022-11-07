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

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Create(
        [Bind(Prefix = "id")] string ENumber, int courseId)
    {
        var student = await _studentRepo.ReadAsync(ENumber);
        if (student == null)
        {
            return RedirectToAction("Index", "Student");
        }
        var course = await _courseRepo.ReadAsync(courseId);
        if (course == null)
        {
            return RedirectToAction("Details", "Student", new { id = ENumber });
        }
        var studentCourse = student.CourseGrades
            .SingleOrDefault(scg => scg.CourseId == courseId);
        if (studentCourse != null)
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
    public async Task<IActionResult> CreateConfirmed(string ENumber, int courseId)
    {
        await _studentCourseRepo.CreateAsync(ENumber, courseId);
        return RedirectToAction("Details", "Student", new { id = ENumber });
    }

    public async Task<IActionResult> AssignGrade(
        [Bind(Prefix ="id")]string ENumber, int courseId)
    {
        var student = await _studentRepo.ReadAsync(ENumber);
        if (student == null)
        {
            return RedirectToAction("Index", "Student");
        }
        var studentCourse = student.CourseGrades
            .FirstOrDefault(scg => scg.CourseId==courseId);
        if (studentCourse == null)
        {
            return RedirectToAction("Details", "Student", new { id = ENumber });
        }
        return View(studentCourse);
    }

    [HttpPost, ValidateAntiForgeryToken, ActionName("AssignGrade")]
    public async Task<IActionResult> AssignGradeConfirmed(
        string ENumber, int studentCourseId, string LetterGrade)
    {
        await _studentCourseRepo.UpdateStudentGradeAsync(
            studentCourseId, LetterGrade);
        return RedirectToAction("Details", "Student", new { id = ENumber });
    }

    public async Task<IActionResult> Remove(
        [Bind(Prefix = "id")] string ENumber, int courseId)
    {
        var student = await _studentRepo.ReadAsync(ENumber);
        if (student == null)
        {
            return RedirectToAction("Index", "Student");
        }
        var studentCourse = student.CourseGrades
            .FirstOrDefault(scg => scg.CourseId == courseId);
        if (studentCourse == null)
        {
            return RedirectToAction("Details", "Student", new { id = ENumber });
        }
        return View(studentCourse);
    }

    [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
    public async Task<IActionResult> RemoveConfirmed(
        string ENumber, int studentCourseId)
    {
        await _studentCourseRepo.RemoveAsync(ENumber, studentCourseId);
        return RedirectToAction("Details", "Student", new { id = ENumber });
    }
}


using CSCI3110CRUDManyToMany.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110CRUDManyToMany.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentCourseAPIController : ControllerBase
{
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly IStudentCourseRepository _studentCourseRepo;

    public StudentCourseAPIController(
        IStudentRepository studentRepo,
        ICourseRepository courseRepo,
        IStudentCourseRepository studentCourseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
        _studentCourseRepo = studentCourseRepo;
    }

    [HttpPost("create")]
    public IActionResult Post([FromForm] string ENumber, [FromForm]int courseId)
    {
        var studentCourseGrade = _studentCourseRepo.Create(ENumber, courseId);
        // Remove the circular reference for the JSON
        studentCourseGrade?.Student?.CourseGrades.Clear();
        studentCourseGrade?.Course?.StudentGrades.Clear();
        return CreatedAtAction("Get", 
            new { id = studentCourseGrade?.Id }, studentCourseGrade);
    }

}

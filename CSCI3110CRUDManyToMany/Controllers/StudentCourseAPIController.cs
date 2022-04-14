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

    [HttpPut("assigngrade")]
    public IActionResult Put(
        [FromForm] string ENumber,
        [FromForm] int studentCourseId,
        [FromForm] string LetterGrade)
    {
        _studentCourseRepo.UpdateStudentGrade(studentCourseId, LetterGrade);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpDelete("remove")]
    public IActionResult Delete(
        [FromForm] string ENumber,
        [FromForm] int studentCourseId)
    {
        _studentCourseRepo.Remove(ENumber, studentCourseId);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpGet("studentgradesreport")]
    public IActionResult Get()
    {
        var students = _studentRepo.ReadAll();
        var studentCourseGrades =
           _studentCourseRepo.ReadAll();
        var model = from s in students
                    join scg in studentCourseGrades
                        on s.ENumber equals scg.StudentENumber
                    orderby s.LastName, s.FirstName
                    select new 
                    {
                        StudentName = s.FirstName + " " + s.LastName,
                        CourseFullCode = scg.Course!.Code,
                        scg.LetterGrade
                    };

        return Ok(model);
    }

}

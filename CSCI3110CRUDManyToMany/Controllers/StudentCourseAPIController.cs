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
    public async Task<IActionResult> PostAsync([FromForm] string ENumber, [FromForm]int courseId)
    {
        var studentCourseGrade = await _studentCourseRepo.CreateAsync(ENumber, courseId);
        // Remove the circular reference for the JSON
        studentCourseGrade?.Student?.CourseGrades.Clear();
        studentCourseGrade?.Course?.StudentGrades.Clear();
        return CreatedAtAction("Get", 
            new { id = studentCourseGrade?.Id }, studentCourseGrade);
    }

    [HttpPut("assigngrade")]
    public async Task<IActionResult> PutAsync(
        [FromForm] string ENumber,
        [FromForm] int studentCourseId,
        [FromForm] string LetterGrade)
    {
        await _studentCourseRepo.UpdateStudentGradeAsync(studentCourseId, LetterGrade);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> DeleteAsync(
        [FromForm] string ENumber,
        [FromForm] int studentCourseId)
    {
        await _studentCourseRepo.RemoveAsync(ENumber, studentCourseId);
        return NoContent(); // 204 as per HTTP specification
    }

    [HttpGet("studentgradesreport")]
    public async Task<IActionResult> GetAsync()
    {
        var students = await _studentRepo.ReadAllAsync();
        var studentCourseGrades =
           await _studentCourseRepo.ReadAllAsync();
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

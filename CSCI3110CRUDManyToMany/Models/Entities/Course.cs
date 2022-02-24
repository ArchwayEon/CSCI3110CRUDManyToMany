using System.ComponentModel.DataAnnotations;

namespace CSCI3110CRUDManyToMany.Models.Entities;

public class Course
{
    public int Id { get; set; }
    [StringLength(10)]
    public string Code { get; set; } = String.Empty;
    public int CreditHours { get; set; }
    public ICollection<StudentCourseGrade> StudentGrades { get; set; }
       = new List<StudentCourseGrade>();
}



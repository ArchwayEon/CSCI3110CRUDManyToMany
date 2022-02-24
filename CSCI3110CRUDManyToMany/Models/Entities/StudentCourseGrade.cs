using System.ComponentModel.DataAnnotations;

namespace CSCI3110CRUDManyToMany.Models.Entities;

public class StudentCourseGrade
{
    public int Id { get; set; }
    [StringLength(2, MinimumLength = 1)]
    public string LetterGrade { get; set; } = String.Empty;

    public string StudentENumber { get; set; } = String.Empty;
    public Student? Student { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
}



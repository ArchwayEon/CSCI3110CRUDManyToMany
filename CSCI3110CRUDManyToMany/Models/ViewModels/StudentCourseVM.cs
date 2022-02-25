using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Models.ViewModels;

public class StudentCourseVM
{
    public Student? Student { get; set; }
    public Course? Course { get; set; }
}


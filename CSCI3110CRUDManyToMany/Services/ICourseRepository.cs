using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public interface ICourseRepository
{
    Course? Read(int id);
    ICollection<Course> ReadAll();
}


using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public interface ICourseRepository
{
    Task<Course?> ReadAsync(int id);
    Task<ICollection<Course>> ReadAllAsync();
}


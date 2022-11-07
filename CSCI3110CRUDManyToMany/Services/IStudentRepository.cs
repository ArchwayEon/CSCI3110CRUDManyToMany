using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public interface IStudentRepository
{
    Task<Student?> ReadAsync(string enumber);
    Task<ICollection<Student>> ReadAllAsync();
}



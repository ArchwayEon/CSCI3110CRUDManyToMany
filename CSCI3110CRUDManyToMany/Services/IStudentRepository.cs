using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public interface IStudentRepository
{
    Student? Read(string enumber);
    ICollection<Student> ReadAll();
}



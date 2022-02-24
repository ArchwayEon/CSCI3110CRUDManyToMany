using CSCI3110CRUDManyToMany.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110CRUDManyToMany.Services;

public class DbStudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _db;

    public DbStudentRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public Student? Read(string enumber)
    {
        return _db.Students
           .Include(s => s.CourseGrades)
              .ThenInclude(scg => scg.Course)
           .Include(s => s.Internship)
           .FirstOrDefault(s => s.ENumber == enumber);
    }

    public ICollection<Student> ReadAll()
    {
        return _db.Students
           .Include(s => s.CourseGrades)
              .ThenInclude(scg => scg.Course)
           .Include(s => s.Internship)
           .ToList();
    }
}



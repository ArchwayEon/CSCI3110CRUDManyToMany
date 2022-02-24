using CSCI3110CRUDManyToMany.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110CRUDManyToMany.Services;

public class DbCourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _db;

    public DbCourseRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public Course? Read(int id)
    {
        return _db.Courses
           .Include(s => s.StudentGrades)
              .ThenInclude(scg => scg.Course)
           .FirstOrDefault(c => c.Id == id);
    }

    public ICollection<Course> ReadAll()
    {
        return _db.Courses
           .Include(s => s.StudentGrades)
              .ThenInclude(scg => scg.Course)
           .ToList();
    }
}



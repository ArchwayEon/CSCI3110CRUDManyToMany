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
    public async Task<Course?> ReadAsync(int id)
    {
        return await _db.Courses
           .Include(s => s.StudentGrades)
              .ThenInclude(scg => scg.Student)
           .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ICollection<Course>> ReadAllAsync()
    {
        return await _db.Courses
           .Include(s => s.StudentGrades)
              .ThenInclude(scg => scg.Student)
           .ToListAsync();
    }
}



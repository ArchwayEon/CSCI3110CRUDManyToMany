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
    public async Task<Student?> ReadAsync(string enumber)
    {
        return await _db.Students
           .Include(s => s.CourseGrades)
              .ThenInclude(scg => scg.Course)
           .Include(s => s.Internship)
           .FirstOrDefaultAsync(s => s.ENumber == enumber);
    }

    public async Task<ICollection<Student>> ReadAllAsync()
    {
        return await _db.Students
           .Include(s => s.CourseGrades)
              .ThenInclude(scg => scg.Course)
           .Include(s => s.Internship)
           .ToListAsync();
    }
}



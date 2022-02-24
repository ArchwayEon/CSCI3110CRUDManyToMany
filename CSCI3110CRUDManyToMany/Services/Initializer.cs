using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public class Initializer
{
    private readonly ApplicationDbContext _db;

    public Initializer(ApplicationDbContext db)
    {
        _db = db;
    }

    public void SeedDatabase()
    {
        _db.Database.EnsureCreated();

        // If there are any students then assume the database is already
        // seeded.
        if (_db.Students.Any()) return;

        var students = new List<Student>
        {
           new Student
              { ENumber = "E00000001", FirstName = "James", LastName = "Smith" },
           new Student
              { ENumber = "E00000002", FirstName = "Maria", LastName = "Garcia" },
           new Student
              { ENumber = "E00000003", FirstName = "Chen", LastName = "Li" },
           new Student
              { ENumber = "E00000004", FirstName = "Aban", LastName = "Hakim" }
        };

        _db.Students.AddRange(students);
        _db.SaveChanges();

        var courses = new List<Course>
        {
           new Course { Code = "CSCI3110", CreditHours = 3 },
           new Course { Code = "CSCI1250", CreditHours = 4 },
          new Course { Code = "CSCI1260", CreditHours = 4 }
        };

        _db.Courses.AddRange(courses);
        _db.SaveChanges();
    }
}



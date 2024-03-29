﻿using CSCI3110CRUDManyToMany.Models.Entities;

namespace CSCI3110CRUDManyToMany.Services;

public class Initializer
{
    private readonly ApplicationDbContext _db;

    public Initializer(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task SeedDatabaseAsync()
    {
        _db.Database.EnsureCreated();

        // If there are any students then assume the database is already
        // seeded.
        if (_db.Students.Any()) return;

        var students = new List<Student>
        {
           new() { ENumber = "E00000001", FirstName = "James", LastName = "Smith" },
           new() { ENumber = "E00000002", FirstName = "Maria", LastName = "Garcia" },
           new() { ENumber = "E00000003", FirstName = "Chen", LastName = "Li" },
           new() { ENumber = "E00000004", FirstName = "Aban", LastName = "Hakim" }
        };

        await _db.Students.AddRangeAsync(students);
        await _db.SaveChangesAsync();

        var courses = new List<Course>
        {
           new() { Code = "CSCI3110", CreditHours = 3 },
           new() { Code = "CSCI1250", CreditHours = 4 },
          new() { Code = "CSCI1260", CreditHours = 4 }
        };

        await _db.Courses.AddRangeAsync(courses);
        await _db.SaveChangesAsync();
    }
}



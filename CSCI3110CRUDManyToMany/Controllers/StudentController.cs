using CSCI3110CRUDManyToMany.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110CRUDManyToMany.Controllers;

public class StudentController : Controller
{
    private IStudentRepository _studentRepo;

    public StudentController(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _studentRepo.ReadAllAsync());
    }

    public async Task<IActionResult> Details(string id)
    {
        var student = await _studentRepo.ReadAsync(id);
        if(student == null)
        {
            return RedirectToAction("Index");
        }
        return View(student);
    }
}


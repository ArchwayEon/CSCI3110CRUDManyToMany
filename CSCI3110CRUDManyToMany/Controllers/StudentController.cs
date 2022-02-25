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

    public IActionResult Index()
    {
        return View(_studentRepo.ReadAll());
    }

    public IActionResult Details(string id)
    {
        var student = _studentRepo.Read(id);
        if(student == null)
        {
            return RedirectToAction("Index");
        }
        return View(student);
    }
}


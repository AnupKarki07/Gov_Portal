using ModelBindingDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ModelBindingDemo.Controllers;

public class EmployeeController : Controller
{
    // GET: Employee/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        return View("Result", employee);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{

    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            return View(_employeeRepository.GetAllEmployee());
        }
        //GET Create
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }
        //POST Create
        [HttpPost] 
        public IActionResult Create(Employee employee)
        {
            return RedirectToAction("Details",new { id = _employeeRepository.Add(employee).Id});
        }


        public IActionResult Details(int? id)
        {
            return View(_employeeRepository.GetEmployee(id??1));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

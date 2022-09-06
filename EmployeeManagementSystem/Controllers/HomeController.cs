using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagementSystem.Controllers
{

    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
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
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model, IFormFile? file)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();        // + "_" + file.FileName;
                var uploads = Path.Combine(wwwRootPath, @"img");
                var extenstion = Path.GetExtension(file.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                // obj.PhotoPath = @"\Imag\Product\" + fileName + extenstion;

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = @"\Img\" + fileName + extenstion
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }
            return View();
        }
        //GET Edit
        public IActionResult Edit (int? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id??0);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        //POST Edit
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employeeEditViewModel, IFormFile file)
        {
            string wwwRootPath = webHostEnvironment.WebRootPath;
            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"img");
                var extension = Path.GetExtension(file.FileName);
                if(employeeEditViewModel.ExistingPhotoPath != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, employeeEditViewModel.ExistingPhotoPath.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using(var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                employeeEditViewModel.ExistingPhotoPath = @"img\" + fileName + extension;
            }
            Employee employee = new Employee
            {
                Id = employeeEditViewModel.Id,
                Name = employeeEditViewModel.Name,
                Email = employeeEditViewModel.Email,
                PhotoPath = employeeEditViewModel.ExistingPhotoPath,
                Department = employeeEditViewModel.Department
            };
            //_employeeRepository.Add(employee);
            _employeeRepository.Update(employee);
            
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

            return View(employee);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


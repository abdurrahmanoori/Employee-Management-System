using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeCreateViewModel
    {

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public Dept? Department { get; set; }
        //public IFormFile Photo { get; set; }

    }
}

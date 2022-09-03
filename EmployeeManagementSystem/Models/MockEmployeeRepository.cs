﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        List<Employee> _employees;
        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee{Id=1, Name="Ahmad",Department= Dept.HR.ToString(),Email="Ahmad@gmail.com" },
                new Employee{Id=2, Name="Maryam",Department= Dept.IT.ToString(),Email="Maryam@gmail.com" },
                new Employee{Id=3, Name="Arezo",Department=Dept.Payroll.ToString(),Email="Arezo@gmail.com" }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employees.Max(e =>e.Id)+ 1;
            _employees.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employees;
        }

        public Employee GetEmployee(int Id)
        {
            return _employees.FirstOrDefault(u => u.Id == Id);
            
        }
    }
}

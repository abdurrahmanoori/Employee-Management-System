using System;
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
                new Employee{Id=1, Name="Ahmad",Department="HR",Email="Ahmad@gmail.com" },
                new Employee{Id=2, Name="Maryam",Department="IT",Email="Maryam@gmail.com" },
                new Employee{Id=1, Name="Arezo",Department="IT",Email="Arezo@gmail.com" }
            };
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

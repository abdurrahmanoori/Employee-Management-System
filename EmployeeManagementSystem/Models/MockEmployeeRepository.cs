using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public Employee GetEmployee(int Id)
        {
            throw new NotImplementedException();
        }
    }
}

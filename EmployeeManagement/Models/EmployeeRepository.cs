using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class EmployeeRepository:IEmployeeRepository
    {
        public List<Employee> employeeList;
        public EmployeeRepository()
        {
            employeeList = new List<Employee>() { new Employee(1, "Nani", "nani@gmail.com", Dept.CSE),
                                                  new Employee(2, "Vijay", "vijay@gmail.com",Dept.ECE),
                                                  new Employee(3, "Ajay", "ajay@gmail.com", Dept.EEE)   
                                                };

        }
        public Employee GetEmployee(int id)
        {
            Employee e = employeeList.FirstOrDefault(e => e.Id == id);

            return e;
        }

        public List<Employee> DisplayDetails()
        {
            return employeeList;
        }

        public bool AddEmployee(Employee emp)
        {
            emp.Id = employeeList.Max(e => e.Id) + 1;
            employeeList.Add(emp);
            return true;
        }

        
    }
}

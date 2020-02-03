using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    //view model class to help in taking data from multiple sources
    public class EmployeeProjectViewModel
    {
        public Employee employee { get; set; }
        public string projectName { get; set; }
    }
}

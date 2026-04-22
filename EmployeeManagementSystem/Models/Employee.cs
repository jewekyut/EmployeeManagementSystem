using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public string emp_id { get; set; }
        public string emp_firstname { get; set; }
        public string emp_lastname { get; set; }
        public string emp_username { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public string gross_salary { get; set; }
        public string status { get; set; }
    }
}
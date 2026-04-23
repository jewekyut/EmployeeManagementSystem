using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int emp_id { get; set; }
        public string emp_username { get; set; }
        public string emp_lastname { get; set; }
        public string emp_firstname { get; set; }
        public string emp_middlename { get; set; }
        public string emp_email { get; set; }
        public string emp_contact { get; set; }
        public string tin_num { get; set; }
        public string sss_num { get; set; }
        public string pagibig_num { get; set; }
        public string philhealth_num { get; set; }
        public string status { get; set; }
        public int gross_salary { get; set; }
        public string position { get; set; }
        public string department { get; set; }
    }
}
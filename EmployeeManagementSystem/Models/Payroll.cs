using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Payroll
    {
        public int id { get; set; }
        public string emp_username { get; set; }
        public float gross_salary { get; set; }
        public float sss { get; set; }
        public float philhealth { get; set; }
        public float pagibig { get; set; }
        public float wtax { get; set; }
        public float other_deduction { get; set; }
        public float net_pay { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Attendance
    {
        public int id { get; set; }
        public string username { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public int emp_id { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public string status { get; set; }
        public string date { get; set; }
    }
}

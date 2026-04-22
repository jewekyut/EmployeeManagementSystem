using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiUrl = "http://localhost/payrollproject/index.php/employeeapi/getEmployees";

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await client.GetStringAsync(apiUrl);
            return JsonConvert.DeserializeObject<List<Employee>>(response);
        }
    }
}
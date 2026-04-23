using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string baseUrl = "http://localhost/payrollproject/index.php/employeeapi";

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await client.GetStringAsync($"{baseUrl}/getEmployees");
            return JsonConvert.DeserializeObject<List<Employee>>(response);
        }

        public async Task<string> AddEmployeeAsync(Employee emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{baseUrl}/addEmployee", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateEmployeeAsync(Employee emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{baseUrl}/updateEmployee", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteEmployeeAsync(int emp_id)
        {
            var json = JsonConvert.SerializeObject(new { emp_id });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{baseUrl}/deleteEmployee", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
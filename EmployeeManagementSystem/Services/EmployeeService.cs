using EmployeeManagementSystem.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string baseUrl = "http://localhost/codeigniter-payroll-project/index.php/employeeapi";

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{baseUrl}/getEmployees");
                return JsonConvert.DeserializeObject<List<Employee>>(response);
            }
        }

        public async Task<string> AddEmployeeAsync(Employee emp)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}/addEmployee", content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> UpdateEmployeeAsync(Employee emp)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}/updateEmployee", content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> DeleteEmployeeAsync(int emp_id)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new { emp_id });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}/deleteEmployee", content);
                return await response.Content.ReadAsStringAsync();
            }
        }
        public async Task<string> UpdateGovNumbersAsync(Employee emp)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}/updateGovNumbers", content);
                return await response.Content.ReadAsStringAsync();
            }
        }
        public async Task<List<Payroll>> GetPayrollAsync(string emp_username)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new { emp_username });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}/getPayroll", content);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Payroll>>(result);
            }
        }

        public async Task<string> AddPayrollAsync(Payroll payroll)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(payroll);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}/addPayroll", content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
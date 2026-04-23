using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class BasicInfoForm : Form
    {
        EmployeeService service = new EmployeeService();

        public BasicInfoForm()
        {
            InitializeComponent();
        }

        private async void BasicInformation_Load(object sender, EventArgs e)
        {
            await LoadEmployees();
        }

        private async Task LoadEmployees()
        {
            var employees = await service.GetEmployeesAsync();
            basicGridView.DataSource = employees;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.emp_firstname = txtFirstName.Text.Trim();
            emp.emp_lastname = txtLastName.Text.Trim();
            emp.emp_middlename = txtMiddleName.Text.Trim();
            emp.emp_username = txtUsername.Text.Trim();
            emp.emp_email = txtEmail.Text.Trim();
            emp.emp_contact = txtContact.Text.Trim();
            emp.position = txtPosition.Text.Trim();
            emp.department = txtDepartment.Text.Trim();
            emp.gross_salary = Convert.ToInt32(txtGrossSalary.Text.Trim());

            var result = await service.AddEmployeeAsync(emp);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"] == "success")
                MessageBox.Show("Employee saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error saving employee.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            await LoadEmployees();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter Employee ID", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Employee emp = new Employee();
            emp.emp_id = Convert.ToInt32(txtEmpId.Text.Trim());
            emp.emp_firstname = txtFirstName.Text.Trim();
            emp.emp_lastname = txtLastName.Text.Trim();
            emp.emp_middlename = txtMiddleName.Text.Trim();
            emp.emp_username = txtUsername.Text.Trim();
            emp.emp_email = txtEmail.Text.Trim();
            emp.emp_contact = txtContact.Text.Trim();
            emp.position = txtPosition.Text.Trim();
            emp.department = txtDepartment.Text.Trim();
            emp.gross_salary = Convert.ToInt32(txtGrossSalary.Text.Trim());

            var result = await service.UpdateEmployeeAsync(emp);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"] == "success")
                MessageBox.Show("Employee updated successfully!", "Success",
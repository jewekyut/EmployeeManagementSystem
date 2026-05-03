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
            emp.gross_salary = string.IsNullOrEmpty(txtGrossSalary.Text.Trim()) ? 0 : Convert.ToInt32(txtGrossSalary.Text.Trim());

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
            emp.gross_salary = string.IsNullOrEmpty(txtGrossSalary.Text.Trim()) ? 0 : Convert.ToInt32(txtGrossSalary.Text.Trim());

            var result = await service.UpdateEmployeeAsync(emp);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"] == "success")
                MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error updating employee.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            await LoadEmployees();
        }

        private async void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter Employee ID", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delete this employee?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(txtEmpId.Text.Trim());
                var result = await service.DeleteEmployeeAsync(id);
                var response = JsonConvert.DeserializeObject<dynamic>(result);

                if (response["status"].ToString() == "success")
                    MessageBox.Show("Employee deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error deleting employee.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                await LoadEmployees();
            }
        }

      

       

    
        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void basicGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = basicGridView.Rows[e.RowIndex];

                txtEmpId.Text = row.Cells["emp_id"].Value?.ToString();
                txtFirstName.Text = row.Cells["emp_firstname"].Value?.ToString();
                txtLastName.Text = row.Cells["emp_lastname"].Value?.ToString();
                txtMiddleName.Text = row.Cells["emp_middlename"].Value?.ToString();
                txtUsername.Text = row.Cells["emp_username"].Value?.ToString();
                txtEmail.Text = row.Cells["emp_email"].Value?.ToString();
                txtContact.Text = row.Cells["emp_contact"].Value?.ToString();
                txtPosition.Text = row.Cells["position"].Value?.ToString();
                txtDepartment.Text = row.Cells["department"].Value?.ToString();
                txtGrossSalary.Text = row.Cells["gross_salary"].Value?.ToString();
            }
        }

        private void basicGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = basicGridView.Rows[e.RowIndex];

                txtEmpId.Text = row.Cells["emp_id"].Value?.ToString();
                txtFirstName.Text = row.Cells["emp_firstname"].Value?.ToString();
                txtLastName.Text = row.Cells["emp_lastname"].Value?.ToString();
                txtMiddleName.Text = row.Cells["emp_middlename"].Value?.ToString();
                txtUsername.Text = row.Cells["emp_username"].Value?.ToString();
                txtEmail.Text = row.Cells["emp_email"].Value?.ToString();
                txtContact.Text = row.Cells["emp_contact"].Value?.ToString();
                txtPosition.Text = row.Cells["position"].Value?.ToString();
                txtDepartment.Text = row.Cells["department"].Value?.ToString();
                txtGrossSalary.Text = row.Cells["gross_salary"].Value?.ToString();
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtPosition.Clear();
            txtDepartment.Clear();
            txtGrossSalary.Clear();
            txtEmpId.Clear();
        }

        private void btnnext1_Click(object sender, EventArgs e)
        {
            EmgContactInfoForm emgcontactinfo = new EmgContactInfoForm();
            this.Hide();
            emgcontactinfo.Show();
        }

        private void txtGrossSalary_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
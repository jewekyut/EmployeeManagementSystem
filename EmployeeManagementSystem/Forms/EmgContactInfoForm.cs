using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class EmgContactInfoForm : System.Windows.Forms.Form
    {
        EmployeeService service = new EmployeeService();

        public EmgContactInfoForm()
        {
            InitializeComponent();
        }
        private void ClearFields()
        {
            txtEmpId.Text = "";
            txtTinNum.Text = "";
            txtSssNum.Text = "";
            txtPagibigNum.Text = "";
            txtPhilhealthNum.Text = "";
        }
        private async void EmgContactInfoForm_Load(object sender, EventArgs e)
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
            if (string.IsNullOrEmpty(txtEmpId.Text))
            {
                MessageBox.Show("Please select an employee first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Employee emp = new Employee();
            emp.emp_id = Convert.ToInt32(txtEmpId.Text.Trim());
            emp.tin_num = txtTinNum.Text.Trim();
            emp.sss_num = txtSssNum.Text.Trim();
            emp.pagibig_num = txtPagibigNum.Text.Trim();
            emp.philhealth_num = txtPhilhealthNum.Text.Trim();

            var result = await service.UpdateGovNumbersAsync(emp);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"].ToString() == "success")
                MessageBox.Show("Government numbers updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error updating.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            ClearFields();
            await LoadEmployees();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEmpId.Clear();
            txtTinNum.Clear();
            txtSssNum.Clear();
            txtPagibigNum.Clear();
            txtPhilhealthNum.Clear();
        }

       

        private void btnnext1_Click(object sender, EventArgs e)
        {
            SalaryInfoForm salaryinfo = new SalaryInfoForm();
            this.Hide();
            salaryinfo.Show();
        }

        private void basicGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = basicGridView.Rows[e.RowIndex];
                txtEmpId.Text = row.Cells["emp_id"].Value?.ToString();
                txtTinNum.Text = row.Cells["tin_num"].Value?.ToString();
                txtSssNum.Text = row.Cells["sss_num"].Value?.ToString();
                txtPagibigNum.Text = row.Cells["pagibig_num"].Value?.ToString();
                txtPhilhealthNum.Text = row.Cells["philhealth_num"].Value?.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            BasicInfoForm basicinfo = new BasicInfoForm();
            this.Hide();
            basicinfo.Show();
        }
    }
}
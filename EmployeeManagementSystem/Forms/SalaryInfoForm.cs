using EmployeeManagementSystem.Forms;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EmployeeManagementSystem
{
    public partial class SalaryInfoForm : Form
    {
        EmployeeService service = new EmployeeService();

        public SalaryInfoForm()
        {
            InitializeComponent();
        }

        private async void SalaryInfoForm_Load(object sender, EventArgs e)
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
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please select an employee first.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Payroll payroll = new Payroll();
            payroll.emp_username = txtUsername.Text.Trim();
            payroll.gross_salary = float.Parse(txtGrossSalary.Text.Trim());
            payroll.sss = float.Parse(txtSss.Text.Trim());
            payroll.philhealth = float.Parse(txtPhilhealth.Text.Trim());
            payroll.pagibig = float.Parse(txtPagibig.Text.Trim());
            payroll.wtax = float.Parse(txtWtax.Text.Trim());
            payroll.other_deduction = float.Parse(txtOtherDeduction.Text.Trim());
            payroll.net_pay = float.Parse(txtNetPay.Text.Trim());

            var result = await service.AddPayrollAsync(payroll);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"].ToString() == "success")
                MessageBox.Show("Payroll saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error saving payroll.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            await LoadEmployees();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtGrossSalary.Clear();
            txtSss.Clear();
            txtPhilhealth.Clear();
            txtPagibig.Clear();
            txtWtax.Clear();
            txtOtherDeduction.Clear();
            txtNetPay.Clear();
        }

        private void btnnext1_Click(object sender, EventArgs e)
        {
            HolidayInfoForm holidayinfo = new HolidayInfoForm();
            this.Hide();
            holidayinfo.Show();
        }

        private void basicGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = basicGridView.Rows[e.RowIndex];
                txtUsername.Text = row.Cells["emp_username"].Value?.ToString();
                txtGrossSalary.Text = row.Cells["gross_salary"].Value?.ToString();
            }
        }
    }
}
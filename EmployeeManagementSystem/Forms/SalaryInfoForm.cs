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
        private void ClearFields()
        {
            txtUsername.Text = "";
            txtGrossSalary.Text = "";
            txtSss.Text = "";
            txtPhilhealth.Text = "";
            txtPagibig.Text = "";
            txtWtax.Text = "";
            txtOtherDeduction.Text = "";
            txtNetPay.Text = "";
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
            payroll.gross_salary = string.IsNullOrEmpty(txtGrossSalary.Text) ? 0 : float.Parse(txtGrossSalary.Text.Trim());
            payroll.sss = string.IsNullOrEmpty(txtSss.Text) ? 0 : float.Parse(txtSss.Text.Trim());
            payroll.philhealth = string.IsNullOrEmpty(txtPhilhealth.Text) ? 0 : float.Parse(txtPhilhealth.Text.Trim());
            payroll.pagibig = string.IsNullOrEmpty(txtPagibig.Text) ? 0 : float.Parse(txtPagibig.Text.Trim());
            payroll.wtax = string.IsNullOrEmpty(txtWtax.Text) ? 0 : float.Parse(txtWtax.Text.Trim());
            payroll.other_deduction = string.IsNullOrEmpty(txtOtherDeduction.Text) ? 0 : float.Parse(txtOtherDeduction.Text.Trim());
            payroll.net_pay = string.IsNullOrEmpty(txtNetPay.Text) ? 0 : float.Parse(txtNetPay.Text.Trim());

            var result = await service.AddPayrollAsync(payroll);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"].ToString() == "success")
                MessageBox.Show("Payroll saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error saving payroll.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            ClearFields();
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

        private async void txtGrossSalary_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGrossSalary.Text))
            {
                try
                {
                    float gross = float.Parse(txtGrossSalary.Text);
                    var contributions = await service.GetContributionsAsync(gross);

                    if (contributions["status"].ToString() == "success")
                    {
                        txtSss.Text = contributions["sss"].ToString();
                        txtPhilhealth.Text = contributions["philhealth"].ToString();
                        txtPagibig.Text = contributions["pagibig"].ToString();
                        ComputeNetPay();
                    }
                }
                catch { }
            }
        }
        private void ComputeNetPay()
        {
            try
            {
                float gross = string.IsNullOrEmpty(txtGrossSalary.Text) ? 0 : float.Parse(txtGrossSalary.Text);
                float sss = string.IsNullOrEmpty(txtSss.Text) ? 0 : float.Parse(txtSss.Text);
                float philhealth = string.IsNullOrEmpty(txtPhilhealth.Text) ? 0 : float.Parse(txtPhilhealth.Text);
                float pagibig = string.IsNullOrEmpty(txtPagibig.Text) ? 0 : float.Parse(txtPagibig.Text);
                float wtax = string.IsNullOrEmpty(txtWtax.Text) ? 0 : float.Parse(txtWtax.Text);
                float otherDeduction = string.IsNullOrEmpty(txtOtherDeduction.Text) ? 0 : float.Parse(txtOtherDeduction.Text);

                float netPay = gross - sss - philhealth - pagibig - wtax - otherDeduction;
                txtNetPay.Text = netPay.ToString("F2");
            }
            catch
            {
                txtNetPay.Text = "0.00";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            EmgContactInfoForm emginfo = new EmgContactInfoForm();
            this.Hide();
            emginfo.Show();

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpId.Text))
            {
                MessageBox.Show("Please enter ID", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Payroll payroll = new Payroll();
            payroll.id = Convert.ToInt32(txtEmpId.Text.Trim());
            payroll.gross_salary = string.IsNullOrEmpty(txtGrossSalary.Text) ? 0 : float.Parse(txtGrossSalary.Text.Trim());
            payroll.sss = string.IsNullOrEmpty(txtSss.Text) ? 0 : float.Parse(txtSss.Text.Trim());
            payroll.philhealth = string.IsNullOrEmpty(txtPhilhealth.Text) ? 0 : float.Parse(txtPhilhealth.Text.Trim());
            payroll.pagibig = string.IsNullOrEmpty(txtPagibig.Text) ? 0 : float.Parse(txtPagibig.Text.Trim());
            payroll.wtax = string.IsNullOrEmpty(txtWtax.Text) ? 0 : float.Parse(txtWtax.Text.Trim());
            payroll.other_deduction = string.IsNullOrEmpty(txtOtherDeduction.Text) ? 0 : float.Parse(txtOtherDeduction.Text.Trim());
            payroll.net_pay = string.IsNullOrEmpty(txtNetPay.Text) ? 0 : float.Parse(txtNetPay.Text.Trim());

            var result = await service.UpdatePayrollAsync(payroll);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"].ToString() == "success")
                MessageBox.Show("Payroll updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error updating payroll.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            await LoadEmployees();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpId.Text))
            {
                MessageBox.Show("Please enter ID", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delete this payroll?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                int id = Convert.ToInt32(txtEmpId.Text.Trim());
                var result = await service.DeletePayrollAsync(id);
                var response = JsonConvert.DeserializeObject<dynamic>(result);

                if (response["status"].ToString() == "success")
                    MessageBox.Show("Payroll deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error deleting payroll.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearFields();
                await LoadEmployees();
            }

        }
    }
}
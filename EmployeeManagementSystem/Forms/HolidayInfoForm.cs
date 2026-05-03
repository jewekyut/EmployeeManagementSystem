using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Mysqlx.Crud;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class HolidayInfoForm : Form
    {
        EmployeeService service = new EmployeeService();

        public HolidayInfoForm()
        {
            InitializeComponent();
        }

        private async void HolidayInfoForm_Load(object sender, EventArgs e)
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

            var employees = await service.GetEmployeesAsync();
            var emp = employees.Find(x => x.emp_id.ToString() == txtEmpId.Text);

            Attendance attendance = new Attendance();
            attendance.emp_id = Convert.ToInt32(txtEmpId.Text.Trim());
            attendance.username = emp?.emp_username;
            attendance.lastname = emp?.emp_lastname;
            attendance.firstname = emp?.emp_firstname;
            attendance.time_in = dtpTimeIn.Value.ToString("HH:mm:ss");
            attendance.time_out = dtpTimeOut.Value.ToString("HH:mm:ss");
            attendance.status = cmbStatus.Text;
            attendance.date = dtpDate.Value.ToString("yyyy-MM-dd");

            var result = await service.AddAttendanceAsync(attendance);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response["status"].ToString() == "success")
                MessageBox.Show("Attendance saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error saving attendance.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            await LoadEmployees();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEmpId.Clear();
            cmbStatus.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
            dtpTimeIn.Value = DateTime.Now;
            dtpTimeOut.Value = DateTime.Now;
        }

        private void basicGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = basicGridView.Rows[e.RowIndex];
                txtEmpId.Text = row.Cells["emp_id"].Value?.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SalaryInfoForm salaryInfo = new SalaryInfoForm();
            this.Hide();
            salaryInfo.Show();
        }
    }
}
using EmployeeManagementSystem.Models;
using EmployManagementSystemAPIs.Services.EmgContactInfoServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class EmgContactInfoForm : System.Windows.Forms.Form
    {
        public EmgContactInfoForm()
        {
            InitializeComponent();
        }
        public async void populateEmgContactInfo()
        {
            EmgContactInfoServices objEmgContactInfo = new EmgContactInfoServices();
            var emgcntInfoList = await objEmgContactInfo.GetAllEmgContactInfo();
            basicGridView.DataSource = emgcntInfoList;

        }
        private void EmgContactInfoForm_Load(object sender, EventArgs e)
        {
            populateEmgContactInfo();

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            EmgContactInfoServices ECobj = new EmgContactInfoServices();
            EmgContactInfo emgcntInfo = new EmgContactInfo();
            emgcntInfo.EmgContactName = txtHolidyMnth.Text.Trim();
            emgcntInfo.EmgContactPhone = txtHoidays.Text.Trim();
            emgcntInfo.EmgContactEmail = txtLeaves.Text.Trim();
            emgcntInfo.BasicId = Convert.ToInt32(txtEmpId.Text.Trim());
            int result = await ECobj.PostEmgContactInfo(emgcntInfo);
            if (result > 0)
            {
                MessageBox.Show("Employee Emergancy Contact Information saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            populateEmgContactInfo();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtHolidyMnth.Clear();
            txtHoidays.Clear();
            txtLeaves.Clear();
            txtEmpId.Clear();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Employee Not Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var confirmResult = MessageBox.Show("Are you sure to delete this Data ?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                EmgContactInfoServices ECobj = new EmgContactInfoServices();
                int id = Convert.ToInt32(txtEmpId.Text);
                int result = await ECobj.DeleteEmgContactInfo(id);
                if (result > 0)
                    MessageBox.Show("Employee Emergancy Contact Information deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                populateEmgContactInfo();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            EmgContactInfoServices ECobj = new EmgContactInfoServices();
            EmgContactInfo emgcntInfo = new EmgContactInfo();
            emgcntInfo.EmgContactName = txtHolidyMnth.Text.Trim();
            emgcntInfo.EmgContactPhone = txtHoidays.Text.Trim();
            emgcntInfo.EmgContactEmail = txtLeaves.Text.Trim();
            emgcntInfo.BasicId = Convert.ToInt32(txtEmpId.Text.Trim());


            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter Employ Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int result = await ECobj.UpdateEmgContactInfo(emgcntInfo);
            if (result > 0)
                MessageBox.Show("Employee Emergancey Contact Information updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            populateEmgContactInfo();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnnext1_Click(object sender, EventArgs e)
        {
            SalaryInfoForm salaryinfo = new SalaryInfoForm();
            this.Hide();
            salaryinfo.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

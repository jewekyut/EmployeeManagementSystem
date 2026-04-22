using EmployeeManagementSystem.Models;
using EmployManagementSystemAPIs.Services.BasicInfoServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmployeeManagementSystem
{
    public partial class BasicInfoForm : Form
    {
        public BasicInfoForm()
        {
            InitializeComponent();
        }
        public async void populateBasicInfo()
        {
            BasicInfoServices objBasicInfo = new BasicInfoServices();
            var basicInfoList = await objBasicInfo.GetAllBasicInfo();
            basicGridView.DataSource = basicInfoList;

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private  void BasicInformation_Load(object sender, EventArgs e)
        {
            populateBasicInfo();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            BasicInfoServices BDobj = new BasicInfoServices();
            BasicInfo basicinfo = new BasicInfo();
            basicinfo.Name = txtName.Text.Trim();
            basicinfo.Email = txtEmail.Text.Trim();
            basicinfo.Address = txtAddress.Text.Trim();

            if (rdoFemale.Checked == true)
                basicinfo.Gender = "Female";
            else
                basicinfo.Gender = "Male";


            basicinfo.Position = cmbPosition.Text;
          

            int result = await BDobj.PostBasicInfo(basicinfo);
            if (result > 0)
            {
                MessageBox.Show("Employee Basic Information saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }
            else
            {
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            populateBasicInfo();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            cmbPosition.SelectedValue = null;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            BasicInfoServices BDobj = new BasicInfoServices();
            BasicInfo basicinfo = new BasicInfo();
            basicinfo.Id = Convert.ToInt32(txtEmpId.Text.Trim());
            basicinfo.Name = txtName.Text.Trim();
            basicinfo.Email = txtEmail.Text.Trim();
            basicinfo.Address = txtAddress.Text.Trim();
            if (rdoFemale.Checked == true)
                basicinfo.Gender = "Female";
            else
                basicinfo.Gender = "Male";
            basicinfo.Position = cmbPosition.Text;

            if (txtEmpId.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter Employ Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }          

            int result = await BDobj.UpdateBasicInfo(basicinfo);
            if (result > 0)
                MessageBox.Show("Employee Basic Information updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            populateBasicInfo();
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

                BasicInfoServices BDobj = new BasicInfoServices();
                int id = Convert.ToInt32(txtEmpId.Text);
                int result = await BDobj.DeleteBasicInfo(id);
                if (result > 0)
                    MessageBox.Show("Employee Basic Information deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                populateBasicInfo();
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnnext1_Click(object sender, EventArgs e)
        {
            EmgContactInfoForm emgcontactinfo = new EmgContactInfoForm();
            this.Hide();
            emgcontactinfo.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
    
}

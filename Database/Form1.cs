using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class Form1 : Form
    {
        DAL db = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            dgvView.DataSource = null;
            txtFirstName.Clear();
            txtID.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            lblID.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you want to save new data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Employee emp = new Employee();
                int resultUpdate = 0;

                if (result == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(txtLastName.Text))
                    {
                        MessageBox.Show("Please enter last name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLastName.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtFirstName.Text))
                    {
                        MessageBox.Show("Please enter first name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFirstName.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtMiddleName.Text))
                    {
                        MessageBox.Show("Please enter middle name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMiddleName.Focus();
                    }
                    else
                    {
                        db = new DAL();

                        emp.LastName = txtLastName.Text;
                        emp.FirstName = txtFirstName.Text;
                        emp.MiddleName = txtMiddleName.Text;

                        resultUpdate = db.InsertEmployee(emp);

                        if (resultUpdate > 0)
                        {
                            MessageBox.Show("Data saved succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                        }
                        else
                            MessageBox.Show("Failed to save data. Please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               if(dgvView.Rows.Count > 0)
                {
                    if(string.IsNullOrEmpty(txtLastName.Text))
                    {
                        MessageBox.Show("Please enter last name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLastName.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtFirstName.Text))
                    {
                        MessageBox.Show("Please enter first name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFirstName.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtMiddleName.Text))
                    {
                        MessageBox.Show("Please enter middle name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMiddleName.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Are you want to update data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        Employee emp = new Employee();
                        int resultUpdate = 0;

                        if (result == DialogResult.Yes)
                        {
                            db = new DAL();

                            emp.ID = Convert.ToInt32(lblID.Text);
                            emp.LastName = txtLastName.Text;
                            emp.FirstName = txtFirstName.Text;
                            emp.MiddleName = txtMiddleName.Text;

                            resultUpdate = db.UpdateEmployee(emp);

                            if (resultUpdate > 0)
                            {
                                MessageBox.Show("Data updated succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            else
                                MessageBox.Show("Failed to update data. Please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<Employee> data = new List<Employee>();

                db = new DAL();
                data = db.GetEmployees(txtID.Text);

                if (data.Count > 0)
                {
                    dgvView.DataSource = data;
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvView.Rows.Count > 0)
            {
                lblID.Text = dgvView.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtLastName.Text = dgvView.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtFirstName.Text = dgvView.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtMiddleName.Text = dgvView.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}

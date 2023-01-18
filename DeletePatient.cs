using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CabinetDentaire
{
    public partial class DeletePatient : Form
    {
        public DeletePatient()
        {
            InitializeComponent();
        }

        private void DeletePatient_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = UpdatePatient.GetPatients();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (current_id != -1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from patient where id=@id", MenuCD.Cnx);
                    MenuCD.Cnx.Open();
                    cmd.Parameters.AddWithValue("@id", current_id);
                    cmd.ExecuteNonQuery();
                    MenuCD.Cnx.Close();
                    dataGridView1.DataSource = UpdatePatient.GetPatients();

                    MessageBox.Show("Patient supprime avec succes.", "Supprimer un patient", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Impossible de supprimer ce patient.", "Supprimer un patient", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    if (MenuCD.Cnx.State == ConnectionState.Open)
                        MenuCD.Cnx.Close();
                }
            }
        }

        int current_id = -1;

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                current_id = int.Parse(row.Cells["id"].Value.ToString());
            }
            else
                current_id = -1;
        }
    }
}

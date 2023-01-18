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
    public partial class UpdateRdv : Form
    {
        public UpdateRdv()
        {
            InitializeComponent();
        }

        private void UpdateRdv_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DiscardRdv.GetPatients3();
            heureRdv.Format = DateTimePickerFormat.Time;
            heureRdv.ShowUpDown = true;
            dateRdv.CustomFormat = "dd-MM-yyyy";
            dateRdv.Format = DateTimePickerFormat.Custom;
        }

        int current_id = -1;
        int current_id_rdv = -1;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                current_id = int.Parse(row.Cells["id"].Value.ToString());
                dataGridView2.DataSource = DiscardRdv.GetRdv(current_id);
            }
            else
                current_id = -1;
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                current_id_rdv = int.Parse(row.Cells["id"].Value.ToString());
                dateRdv.Text = row.Cells["dateRdv"].Value.ToString();
                heureRdv.Text = row.Cells["heureRdv"].Value.ToString();
                txtType.Text = row.Cells["typeRdv"].Value.ToString();
            }
            else
                current_id = -1;
        }

        private void btnAddRdv_Click(object sender, EventArgs e)
        {
            if (current_id != -1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update rdv set dateRdv=@dateRdv, heureRdv=@heureRdv, typeRdv=@typeRdv where id=@id", MenuCD.Cnx);
                    MenuCD.Cnx.Open();
                    cmd.Parameters.AddWithValue("@dateRdv", dateRdv.Text);
                    cmd.Parameters.AddWithValue("@heureRdv", heureRdv.Text);
                    cmd.Parameters.AddWithValue("@typeRdv", txtType.Text);
                    cmd.Parameters.AddWithValue("@id", current_id_rdv);
                    cmd.ExecuteNonQuery();
                    MenuCD.Cnx.Close();
                    dataGridView2.DataSource = DiscardRdv.GetRdv(current_id);


                    MessageBox.Show("Rendez vous modifie avec succes.", "Modifier un patient", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Impossible de modifier ce rendez vous.", "Modifier un patient", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }   
                finally
                {
                    if (MenuCD.Cnx.State == ConnectionState.Open)
                        MenuCD.Cnx.Close();
                }
            }
        }
    }
}

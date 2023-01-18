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
    public partial class AddRdv : Form
    {
        public AddRdv()
        {
            InitializeComponent();
        }

        private int current_id = -1;

        private void AddRdv_Load(object sender, EventArgs e)
        {
            heureRdv.Format = DateTimePickerFormat.Time;
            heureRdv.ShowUpDown = true;
            dateRdv.CustomFormat = "dd-MM-yyyy";
            dateRdv.Format = DateTimePickerFormat.Custom;
            dataGridView1.DataSource = UpdatePatient.GetPatients();

        }

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

        private void btnAddRdv_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into rdv values(@dateRdv, @heureRdv, @typeRdv, @idPatient)", MenuCD.Cnx);
                MenuCD.Cnx.Open();
                cmd.Parameters.AddWithValue("@dateRdv", dateRdv.Text);
                cmd.Parameters.AddWithValue("@heureRdv", heureRdv.Text);
                cmd.Parameters.AddWithValue("@typeRdv", txtType.Text);
                cmd.Parameters.AddWithValue("@idPatient", current_id);
                cmd.ExecuteNonQuery();
                MenuCD.Cnx.Close();
                MessageBox.Show("Rendez vous ajoute avec succes.", "Ajouter un rendez vous", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            catch
            {
                MessageBox.Show("Impossible d'ajouter ce rendez vous.", "Ajouter un rendez vous", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (MenuCD.Cnx.State == ConnectionState.Open)
                    MenuCD.Cnx.Close();
            }
        }
    }
}

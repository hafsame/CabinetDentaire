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
    public partial class DiscardRdv : Form
    {
        public DiscardRdv()
        {
            InitializeComponent();
        }

        int current_id = -1;
        int current_id_rdv = -1;
        public static DataTable GetRdv(int id)
        {
            DataTable dtRdv = new DataTable();
            if (id != -1)
            {
                SqlCommand cmd = new SqlCommand("select id, dateRdv, heureRdv, typeRdv from rdv where idPatient=@id", MenuCD.Cnx);
                MenuCD.Cnx.Open();
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                dtRdv.Load(reader);
                MenuCD.Cnx.Close();
            }
            return dtRdv;
        }

        public static DataTable GetPatients3()
        {
            DataTable dtPatients = new DataTable();
            SqlCommand cmd = new SqlCommand("select id, nom, prenom, tel from patient", MenuCD.Cnx);
            MenuCD.Cnx.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dtPatients.Load(reader);
            MenuCD.Cnx.Close();

            return dtPatients;
        }

        private void DiscardRdv_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetPatients3();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                current_id = int.Parse(row.Cells["id"].Value.ToString());
                dataGridView2.DataSource = GetRdv(current_id);
            }
            else
                current_id = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (current_id_rdv != -1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from rdv where id=@id", MenuCD.Cnx);
                    MenuCD.Cnx.Open();
                    cmd.Parameters.AddWithValue("@id", current_id_rdv);
                    cmd.ExecuteNonQuery();
                    MenuCD.Cnx.Close();
                    dataGridView2.DataSource = GetRdv(current_id);

                    MessageBox.Show("Rendez vous supprime avec succes.", "Supprimer un Rendez vous", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Impossible de supprimer ce Rendez vous.", "Supprimer un Rendez vous", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    if (MenuCD.Cnx.State == ConnectionState.Open)
                        MenuCD.Cnx.Close();
                }
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                current_id_rdv = int.Parse(row.Cells["id"].Value.ToString());

            }
            else
                current_id_rdv = -1;
        }
    }
}

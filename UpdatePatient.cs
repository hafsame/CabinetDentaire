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
    public partial class UpdatePatient : Form
    {

        public int current_id = -1;
        public UpdatePatient()
        {
            InitializeComponent();
        }

        public static DataTable GetPatients()
        {
            DataTable dtPatients = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from patient", MenuCD.Cnx);
            MenuCD.Cnx.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            dtPatients.Load(reader);
            MenuCD.Cnx.Close();

            return dtPatients;
        }

        private void UpdatePatient_Load(object sender, EventArgs e)
        {
            dateNaissance.CustomFormat = "dd-MM-yyyy";
            dateNaissance.Format = DateTimePickerFormat.Custom;
            dataGridView1.DataSource = GetPatients();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                current_id = int.Parse(row.Cells["id"].Value.ToString());
                txtNom.Text = row.Cells["nom"].Value.ToString();
                txtPrenom.Text = row.Cells["prenom"].Value.ToString();
                txtAdresse.Text = row.Cells["adresse"].Value.ToString();
                txtGenre.Text = row.Cells["genre"].Value.ToString();
                txtTel.Text = row.Cells["tel"].Value.ToString();
                dateNaissance.Text = row.Cells["dateNaissance"].Value.ToString();

            }
            else
                current_id = -1;
        }

        private void txtUpdate_Click(object sender, EventArgs e)
        {
            if (current_id != -1)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update patient set nom=@nom, prenom=@prenom, genre=@genre, adresse=@adresse, tel=@tel, dateNaissance=@dateNaissance where id=@id", MenuCD.Cnx);
                    MenuCD.Cnx.Open();
                    cmd.Parameters.AddWithValue("@nom", txtNom.Text);
                    cmd.Parameters.AddWithValue("@prenom", txtPrenom.Text);
                    cmd.Parameters.AddWithValue("@genre", txtGenre.Text);
                    cmd.Parameters.AddWithValue("@dateNaissance", dateNaissance.Text);
                    cmd.Parameters.AddWithValue("@adresse", txtAdresse.Text);
                    cmd.Parameters.AddWithValue("@tel", txtTel.Text);
                    cmd.Parameters.AddWithValue("@id", current_id);
                    cmd.ExecuteNonQuery();
                    MenuCD.Cnx.Close();
                    dataGridView1.DataSource = GetPatients();

                    MessageBox.Show("Patient modifie avec succes.", "Modifier un patient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Impossible de modifier ce patient.", "Modifier un patient", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

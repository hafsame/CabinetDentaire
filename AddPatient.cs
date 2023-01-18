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
    public partial class AddPatient : Form
    {
        public AddPatient()
        {
            InitializeComponent();
        }

        

        private void txtAdd_Click(object sender, EventArgs e)
        {
            try 
            {
                SqlCommand cmd = new SqlCommand("insert into patient values(@nom, @prenom, @genre, @dateNaissance, @adresse, @tel)", MenuCD.Cnx);
                MenuCD.Cnx.Open();
                cmd.Parameters.AddWithValue("@nom", txtNom.Text);
                cmd.Parameters.AddWithValue("@prenom", txtPrenom.Text);
                cmd.Parameters.AddWithValue("@genre", txtGenre.Text);
                cmd.Parameters.AddWithValue("@dateNaissance", dateNaissance.Text);
                cmd.Parameters.AddWithValue("@adresse", txtAdresse.Text);
                cmd.Parameters.AddWithValue("@tel", txtTel.Text);
                cmd.ExecuteNonQuery();
                MenuCD.Cnx.Close();
                MessageBox.Show("Patient ajoute avec succes.", "Ajouter un patient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Impossible d'ajouter ce patient.", "Ajouter un patient", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (MenuCD.Cnx.State == ConnectionState.Open)
                    MenuCD.Cnx.Close();
            }
        }

        private void AddPatient_Load(object sender, EventArgs e)
        {
            dateNaissance.CustomFormat = "dd-MM-yyyy";
            dateNaissance.Format = DateTimePickerFormat.Custom;
            //MessageBox.Show(dateNaissance.Text);
        }
    }
}

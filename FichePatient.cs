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
    public partial class FichePatient : Form
    {
        public FichePatient()
        {
            InitializeComponent();
        }

        private void FichePatient_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = UpdatePatient.GetPatients();

        }

        Bitmap bmp;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics ng = Graphics.FromImage(bmp);
            ng.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.Show();
        }

        int current_id = -1;

        public int getNbRdv(int id)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from rdv where idPatient=@id", MenuCD.Cnx);
            MenuCD.Cnx.Open();
            cmd.Parameters.AddWithValue("@id", id);
            int nbRdv = int.Parse(cmd.ExecuteScalar().ToString());
            MenuCD.Cnx.Close();
            return nbRdv;
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                current_id = int.Parse(row.Cells["id"].Value.ToString());
                label1.Text = "Nom:" + row.Cells["nom"].Value.ToString();
                label2.Text = "Prenom:" + row.Cells["prenom"].Value.ToString();
                label3.Text = "Adresse:" + row.Cells["adresse"].Value.ToString();
                label4.Text = "Genre:" + row.Cells["genre"].Value.ToString();
                label5.Text = "Tel:" + row.Cells["tel"].Value.ToString();
                label6.Text = "Date de naissance:" + row.Cells["dateNaissance"].Value.ToString();
                label7.Text = "Nombre de rendez vous:" + getNbRdv(current_id).ToString();

            }
            else
                current_id = -1;
        }
    }
}

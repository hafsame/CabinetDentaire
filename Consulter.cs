using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CabinetDentaire
{
    public partial class Consulter : Form
    {
        public Consulter()
        {
            InitializeComponent();
        }

        private void Consulter_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DiscardRdv.GetPatients3();

        }
        int current_id = -1;
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
    }
}

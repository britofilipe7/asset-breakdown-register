using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssetBreakdownRegisterV3.Utility;

namespace AssetBreakdownRegisterV2
{
    public partial class CostForm : Form
    {
        private string id;
        private double cost;
        private string observations;

        public CostForm(string id, string observations)
        {
            InitializeComponent();
            this.Id = id;
            this.observations = observations;
        }

        private void btnCostSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cost = double.Parse(txtCost.Text);
                string newObservations = observations + ". " + txtObservations.Text;
                SqliteDataAccess sqlData = new SqliteDataAccess();
                sqlData.UpdateData("breakdown", "state", "cost", "observations", "Concluded", Cost.ToString(), newObservations, id);
                Close();
            } 
            catch (FormatException) { 
                MessageBox.Show("Introduza um valor válido.");
            }
            

        }

        private void btnCostCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string Id { get => id; set => id = value; }
        public double Cost { get => cost; set => cost = value; }
        public string Observations { get => observations; set => observations = value; }
    }
}

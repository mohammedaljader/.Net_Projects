using ContainerVervoer.Models;
using System;
using System.Windows.Forms;

namespace ContainerVervoer.Forms
{
    public partial class CreationForm : Form
    {
        Port port;

        public CreationForm()
        {
            InitializeComponent();
        }

        private void CreateShipBtn_Click(object sender, EventArgs e)
        {
            int shipLength = Convert.ToInt32(shipLengthValue.Value);
            int shipWidth = Convert.ToInt32(shipWidthValue.Value);
            int shipWeight = Convert.ToInt32(shipWeightValue.Value);

            if (shipLength >= 3 && shipWidth >= 3)
            {
                port = new Port(shipLength, shipWidth, shipWeight);
                LogisticsForm logisticsForm = new LogisticsForm(port);
                logisticsForm.Show();
                this.Hide();
                return;
            } 
            else
            {
                MessageBox.Show("Please increase the size of the ship. Lenght & Width can't be smaller tha n 3!", "Incorrect Size!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

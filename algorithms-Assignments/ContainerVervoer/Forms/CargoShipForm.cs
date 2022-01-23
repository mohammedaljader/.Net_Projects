using ContainerVervoer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerVervoer.Forms
{
    public partial class CargoShipForm : Form
    {
        private Port port;
        public CargoShipForm(Port port)
        {
            InitializeComponent();
            this.port = port;
        }

        /// <summary>
        /// Loads the Grid with the rows and stacks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CargoShipForm_Load(object sender, EventArgs e)
        {
            int boxWidth = 75;
            int boxHeight = 50;

            int x = 0;
            int z = 0;

            foreach (Row row in port.cargoShip.Rows)
            {  
                foreach (Stack stack in row.Stacks)
                {
                    Label locationBox = new Label
                    {
                        Text = "Stack: " + x + " x " + z + " ",
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Black,
                        BackColor = SystemColors.Menu,
                        Size = new Size(boxWidth, boxHeight),
                        Location = new Point((boxWidth + 5) * x + 5, (boxHeight + 5) * z + 5)

                    };
                    locationBox.Click += new EventHandler(txtBox_Click);
                    Controls.Add(locationBox);
                    z++;
                }
                x++;
                z = 0;
            }
            if (port.cargoShip.IsShipInBalance())
            {
                MessageBox.Show("CargoShip is filled and ready to go!", "CargoShip is safe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("CargoShip is not in balance", "CargoShip is not safe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Load container information
        private void txtBox_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label != null)
            {
                int x = int.Parse(label.Text.Substring(7, 2));
                int z = int.Parse(label.Text.Substring(11, 2));

                Row row = port.cargoShip.Rows[x];
                Stack stack = row.Stacks[z];

                MessageBox.Show(stack.ToString(), "Location: " + x + " - " + z, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

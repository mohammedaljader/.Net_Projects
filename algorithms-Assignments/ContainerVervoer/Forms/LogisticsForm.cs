using System;
using System.Windows.Forms;
using ContainerVervoer.Models;
using ContainerVervoer.Enums;
using System.Linq;

namespace ContainerVervoer.Forms
{
    public partial class LogisticsForm : Form
    {
        Port port;
        public LogisticsForm(Port port)
        {
            InitializeComponent();
            CenterToScreen();

            this.port = port;

            ShipSizeLabel.Text = port.cargoShip.length + " x " + port.cargoShip.width;
            containerType.DataSource = Enum.GetValues(typeof(ContainerType));
        }

        /// <summary>
        /// Creates Container with the inserted and selected values.
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmContainerButton_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(addContainerWeight.Text, out int weight))
            {
                Container container = new Container(weight, (ContainerType)containerType.SelectedValue);
                cargoDeckBox.Items.Add(container);
                port.containersToPlace.Add(container);

            }
            else
            {
                MessageBox.Show("Please enter a valid weight");
            }
        }

        /// <summary>
        /// Deletes the selecter container.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteContainerButton_Click(object sender, EventArgs e)
        {
            Container container = cargoDeckBox.SelectedItem as Container;
            cargoDeckBox.Items.Remove(container);
            port.containersToPlace.Remove(container); 
        }

        /// <summary>
        /// Starts the sorting of the made containers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortButton_Click(object sender, EventArgs e)
        {
            if (port.StartSorting())
            {
                CargoShipForm cargoShip = new CargoShipForm(port);
                cargoShip.Show();
                this.Hide();
                return;
            }
        }

        private void GMDButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < port.cargoShip.length; x++)
            {
                for (int z = 0; z < port.cargoShip.width; z++)
                {
                    Container container = new Container(GetRandomWeight(), GetRandomType());
                    if (port.containersToPlace.Sum(foundContainers => foundContainers.Weight) + container.Weight <= port.cargoShip.maximumWeight)
                    {
                        cargoDeckBox.Items.Add(container);
                        port.containersToPlace.Add(container);
                    }
                }
            }
        }

        private ContainerType GetRandomType()
        {
            switch (new Random(Guid.NewGuid().GetHashCode()).Next(1, 4))
            {
                case 1:
                    return ContainerType.Cooled;
                case 2:
                    return ContainerType.Normal;
                case 3:
                    return ContainerType.Valuable;
                default:
                    return ContainerType.Normal;
            }
        }

        private int GetRandomWeight()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(4000, 30000);
        }
    }
}

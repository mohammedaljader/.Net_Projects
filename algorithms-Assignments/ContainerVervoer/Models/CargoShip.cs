using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerVervoer.Models
{
    public class CargoShip
    {
        #region Variables
        public List<Row> Rows = new List<Row>();
        public int length { get; }
        public int width { get; }
        public int maximumWeight { get; }
        #endregion

        #region Constructor
        public CargoShip(int length, int width, int maximumWeight)
        {
            this.width = width;
            this.length = length;
            this.maximumWeight = maximumWeight;
            GenerateRows();
        }
        #endregion

        #region Generates the rows
        public void GenerateRows()
        {
            for (int i = 0; i < width; i++)
            {
                Rows.Add(new Row(length));
            }
        }
        #endregion

        #region Gets the current weight of the Ship
        public int GetWeight()
        {
            return Rows.Sum(row => row.GetWeight());
        }
        #endregion

        #region Checks weight of all containers and loops to place them.
        /// <summary>
        /// Checks weight of all containers, then loops through all the containers to place them.
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        public bool AddContainers(List<Container> containers)
        {
            if (containers.Sum(container => container.Weight) + GetWeight() < maximumWeight / 2 || 
                containers.Sum(container => container.Weight) + GetWeight() > maximumWeight)
            {
                MessageBox.Show("Weight too low or over the maximum weight.", "Problem with Weight!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            foreach (Container container in containers)
            {
                PlaceContainer(container);
            }
            return true;
        }
        #endregion

        #region Checks and places container on the middle of the ship if placeable.
        /// <summary>
        /// Checks if the container can be placed in the middle and places it there, otherwise sends it further to check if need to get placed left or right.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceContainer(Container container)
        {
            if (Rows.Count % 2 != 0)
            {
                if (Rows[Rows.Count / 2].AddContainer(container))
                {
                    return true;
                }
            }
            return PlaceAtLocation(container);
        }
        #endregion

        #region Check if container should be placed on the left or right side of the ship
        /// <summary>
        /// Checks if the Container should be placed on the left, or on the right side of the ship
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceAtLocation(Container container)
        {
            return (GetLeftRows().Sum(row => row.GetWeight()) <= GetRightRows().Sum(row => row.GetWeight()) 
                ? PlaceOnTheLeft(container) 
                : PlaceOnTheRight(container));
        }
        #endregion

        #region Place Container on the left side of the ship
        /// <summary>
        /// Places the given container on the left side of the ship for balance.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceOnTheLeft(Container container)
        {
            foreach (Row row in GetLeftRows())
            {
                if (row.AddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Place Container on the right side of the ship
        /// <summary>
        /// Places the given container on the right side of the ship for balance.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool PlaceOnTheRight(Container container)
        {
            foreach (Row row in GetRightRows())
            {
                if (row.AddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Get both sides of the rows
        /// <summary>
        /// Get the Left rows
        /// </summary>
        /// <returns></returns>
        public List<Row> GetLeftRows()
        {
            List<Row> leftRows = Rows.GetRange(0, Rows.Count / 2);
            leftRows.Reverse();
            return leftRows;
        }

        /// <summary>
        /// Get the Right rows
        /// </summary>
        /// <returns></returns>
        public List<Row> GetRightRows()
        {
            return Rows.GetRange(Rows.Count % 2 == 0 ? Rows.Count / 2 : Rows.Count / 2 + 1, Rows.Count / 2);
        }
        #endregion

        #region Get Weight in both sides of the rows.
        /// <summary>
        /// Get the weight of the left sides of the rows
        /// </summary>
        /// <returns></returns>
        public int GetWeightLeftRows()
        {
            return GetLeftRows().Sum(row => row.GetWeight());
        }

        /// <summary>
        /// Get the weight of the right sides of the rows
        /// </summary>
        /// <returns></returns>
        public int GetWeightRightRows()
        {
            return GetRightRows().Sum(row => row.GetWeight());
        }
        #endregion

        #region Checks if the Ship is in Balance
        public bool IsShipInBalance()
        {
            if (Math.Abs(GetWeightRightRows() - GetWeightLeftRows()) <= 0.2 * GetWeight())
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}

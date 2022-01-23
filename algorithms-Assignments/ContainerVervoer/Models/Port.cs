using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Port
    {
        #region Variables
        public List<Container> containersToPlace = new List<Container>();
        public CargoShip cargoShip;
        #endregion

        #region Constructor
        public Port(int length, int width, int maximumweight)
        {
            cargoShip = new CargoShip(length, width, maximumweight);
        }
        #endregion

        #region Starts the placing of containers
        /// <summary>
        /// Starts the placing algoritm
        /// </summary>
        /// <returns></returns>
        public bool StartSorting()
        {
            return cargoShip.AddContainers(SortContainersList(containersToPlace));
        }
        #endregion

        #region Sorts the list of containers
        /// <summary>
        /// Sorts the list of containers that need to get placed.
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        public List<Container> SortContainersList(List<Container> containers)
        {
            return containers.Where(container => container.Type == ContainerType.Cooled).ToList()
                .Concat(containers.Where(container => container.Type == ContainerType.Normal).ToList())
                .Concat(containers.Where(container => container.Type == ContainerType.Valuable).ToList())
                .ToList();
        }
        #endregion
    }
}

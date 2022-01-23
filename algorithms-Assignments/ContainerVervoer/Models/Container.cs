using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Container
    {
        #region Variables
        public int Weight { get; set; }
        public ContainerType Type { get; }
        #endregion

        #region Constructor
        public Container(int weight, ContainerType type)
        {
            SetWeight(weight);
            Type = type;
        }
        #endregion

        #region Checks if valid weight and sets the Weight of the Container
        public void SetWeight(int weight)
        {
            if (weight < 4000 || weight > 30000)
            {
                throw new ArgumentException("Weight must be between the minimum of 4000 and the maximum of 30000.");
            }
            Weight = weight;
        }
        #endregion

        #region ToString override to show information of the Container
        public override string ToString()
        {
            return $"Container: Type: {Type}, Weight: {Weight}";
        }
        #endregion
    }
}

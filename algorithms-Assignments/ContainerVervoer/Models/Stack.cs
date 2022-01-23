using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Stack
    {
        #region Variables
        public List<Container> Containers = new List<Container>();
        #endregion

        #region ToString override to show information of the Stack
        public override string ToString()
        {
            string result = "Stack Information: ";
            if (Containers.Count > 0)
            {
                Containers.ForEach(container => result += "\n" + container.ToString());
                return result;
            }
            return "Stack is Empty";
        }
        #endregion

        #region 2 checks before adding to stack.
        public bool AddContainer(Container container)
        {
            if (ContainsValuable() || !WeightWithinLimit(container))
            {
                return false;
            }
            Containers.Add(container);
            return true;
        }
        #endregion

        #region Check if weight is within limit
        public bool WeightWithinLimit(Container container)
        {
            return Containers.Skip(1).Sum(foundContainers => foundContainers.Weight) + container.Weight <= 120000;
        }
        #endregion

        #region Check if Stack containers any valuables
        public bool ContainsValuable()
        {
            return Containers.Any(container => container.Type == ContainerType.Valuable);
        }
        #endregion

        #region Get the Height of the Stack
        public int GetHeight()
        {
            return Containers.Count;
        }
        #endregion

        #region Get the total weight of the Stack
        public int GetWeight()
        {
            return Containers.Sum(container => container.Weight);
        }
        #endregion
    }
}

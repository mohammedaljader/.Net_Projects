using ContainerVervoer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer.Models
{
    public class Row
    {
        #region Variables
        public List<Stack> Stacks = new List<Stack>();
        #endregion

        #region Constructor
        public Row(int length)
        {
            GenerateStacks(length);
        }
        #endregion

        #region Generates the stacks
        public void GenerateStacks(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Stacks.Add(new Stack());
            }
        }
        #endregion

        #region Checks the Container Type
        public bool AddContainer(Container container)
        {
            switch (container.Type)
            {
                case ContainerType.Cooled:
                    return AddCooledContainers(container);
                case ContainerType.Normal:
                    return AddNormalContainers(container);
                case ContainerType.Valuable:
                    return AddValuableContainers(container);
            }
            return false;
        }
        #endregion

        #region Cooled Containers
        public bool AddCooledContainers(Container container)
        {
            return Stacks.First().AddContainer(container);
        }
        #endregion

        #region Normal Containers
        public bool AddNormalContainers(Container container)
        {
            foreach (Stack stack in Stacks.Skip(1))
            {
                if (stack.AddContainer(container))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Valuable Containers
        public bool AddValuableContainers(Container container)
        {
            for (int stackindex = 0; stackindex < Stacks.Count; stackindex++)
            {
                if (stackindex % 2 == 0) 
                {
                    if (CheckHeightOfFrontStack(stackindex) || CheckHeightOfBackStack(stackindex))
                    {
                        if (Stacks[stackindex].AddContainer(container))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region Checks the Height of the Stacks infront and behind of a certain stack
        public bool CheckHeightOfFrontStack(int stackindex)
        {
            try
            {
                return Stacks[stackindex].GetHeight() <= Stacks.ElementAt(stackindex - 1).GetHeight();
            }
            catch (ArgumentOutOfRangeException)
            {
                return true;
            }
        }

        public bool CheckHeightOfBackStack(int stackindex)
        {
            try
            {
                return Stacks[stackindex].GetHeight() <= Stacks.ElementAt(stackindex - 1).GetHeight();
            }
            catch (ArgumentOutOfRangeException)
            {
                return true;
            }
        }
        #endregion

        #region Getter for Weight
        public int GetWeight()
        {
            return Stacks.Sum(stacks => stacks.GetWeight());
        }
        #endregion
    }
}

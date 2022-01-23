using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction_To_Algorithms
{
    public class Sorting
    {
        #region Selection Sort
        //Selection Sort way
        public void SelectionSort(int[] numbers)
        {
            int swap, minIndex;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                //Assuming Min. Index
                minIndex = i;

                //Finding the accurate Minimun index
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }

                //Swapping 
                swap = numbers[minIndex];
                numbers[minIndex] = numbers[i];
                numbers[i] = swap;
            }
        }
        #endregion

        #region Insertion Sort
        //Insertion Sort way
        public void InsertionSorting(int[] numbers)
        {
            var i = 0;
            var j = 1;
            int key;
            while (i < numbers.Length)
            {
                key = numbers[i];
                j = i - 1;
                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = key;
                i++;
            }
        }
        #endregion

        #region Sell sort
        //Sell sorting way 
        public void SellSorting(int[] numbers)
        {
            int i = 0, j = 0, key;

            //Algorithm
            int increment = numbers.Length / 2;
            while(increment != 0)
            {
                i = increment;
                while(i < numbers.Length)
                {
                    key = numbers[i];
                    j = i - increment;
                    while(j >= 0 && numbers[j] > key)
                    {
                        numbers[j + increment] = numbers[j];
                        j = j - increment;
                    }
                    numbers[j + increment] = key;
                    i++;
                }
                increment = increment / 2;
            }
        }
        #endregion

        #region Bubble Sort
        //Bubble Sort
        public void BubbleSorting(int[] numbers)
        {
            int swap;
            //Algorithm
            for (int i = 0; i < numbers.Length ; i++)
            {
                for (int j = 0; j < numbers.Length - 1- i ; j++)
                {
                    if(numbers[j + 1] < numbers[j])
                    {
                        swap = numbers[j + 1];
                        numbers[j + 1] = numbers[j];
                        numbers[j] = swap;
                    }
                }
            }
        }
        #endregion

        #region Merge Sort
        //Merge Sort

        //first dividing the array 
        public int[] Divide(int[] numbers)
        {
            if(numbers.Length == 1)
            {
                return numbers;
            }
            int mid_point = numbers.Length / 2;

            int[] left = new int[mid_point];
            int[] right = new int[numbers.Length - mid_point];
            for(int i = 0; i < left.Length; i++)
            {
                left[i] = numbers[i];
            }
            for (int i = 0; i < right.Length; i++)
            {
                right[i] = numbers[mid_point + i];
            }
            left = Divide(left);
            right = Divide(right);

            return (Arrange(left, right));
        }
        //than arrange the array
        int[] Arrange(int[] left , int[] right)
        {
            int[] sorted_numbers = new int[left.Length + right.Length];
            int LCounter = 0, RCounter = 0;
            for(int counter = 0; counter < sorted_numbers.Length; counter++)
            {
                if(LCounter < left.Length && RCounter < right.Length)
                {
                    if(left[LCounter] < right[RCounter])
                    {
                        sorted_numbers[counter] = left[LCounter];
                        LCounter++;
                    }
                    else
                    {
                        sorted_numbers[counter] = right[RCounter];
                        RCounter++;
                    }
                }
                else
                {
                    if(RCounter == right.Length)
                    {
                        sorted_numbers[counter] = left[LCounter];
                        LCounter++;
                    }
                    else
                    {
                        sorted_numbers[counter] = right[RCounter];
                        RCounter++;
                    }
                }
            }
            return sorted_numbers;
        }
        #endregion

        #region Quick sorting
        //Quick sorting
        public int[] QuickSort(int[] numbers , int left , int right)
        {
            int pivot = numbers[(left + right) / 2];
            int i = left;
            int j = right;
            int swap = 0;

            while(i <= j)
            {
                while(numbers[i] < pivot) 
                {
                    i++;
                }
                while(numbers[j] > pivot)
                { 
                    j--; 
                }

                if(i <= j)
                {
                    swap = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = swap;
                    i++;
                    j--;
                }
                if(left < j)
                {
                    numbers = QuickSort(numbers, left, j);
                }
                if(right > i)
                {
                    numbers = QuickSort(numbers, i, right);
                }
            }
            return numbers;
        }
        #endregion
    }
}

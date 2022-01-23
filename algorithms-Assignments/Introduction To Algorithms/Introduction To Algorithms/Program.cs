using System;

namespace Introduction_To_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Fields and declarations
            //object of Sorting Class
            Sorting sorting = new Sorting();
            #endregion

            #region Selection Sort way
            //Selection Sort way
            int[] numbers1 = new int[] { 10, 8, 2, 6, 12, 26, 2 };
            sorting.SelectionSort(numbers1);
            //Printing Array
            foreach (var item in numbers1)
            {
                Console.Write(item +"  ");
            }
            Console.WriteLine(" ");
            #endregion

            #region Insertion Sort way
            //Insertion Sort 
            int[] number2 = new int[] { 1, 4, 0, 2, 3, 64, 234 };
            sorting.InsertionSorting(number2);
            //Printing Array
            foreach (var item in number2)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine(" ");
            #endregion

            #region Sell sorting way
            //Sell sorting 
            int[] number3 = new int[] { 191, 4, 0, 2, 3, 64, 234, 192 , 5 , 12 , 8};
            sorting.SellSorting(number3);
            //Printing Array
            foreach (var item in number3)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine(" ");
            #endregion

            #region Bubble Sort
            //Bubble Sort
            int[] number4 = new int[] { 10, 4,190, 0, 2, 3, 9, 7};
            sorting.BubbleSorting(number4);
            //Printing Array
            foreach (var item in number4)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine("  ");
            #endregion

            #region Merge Sorting way
            //Merge Sorting way
            int[] numbers5 = new int[] { 100, 32 , 76 , 22, 4 , 0 , 12 , 98 , 102, 1000};
            int[] arrange = sorting.Divide(numbers5);
            foreach (var item in arrange)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine(" ");
            #endregion

            #region Quick sorting way 
            //Quick sorting way 
            int[] numbers6 = new int[] { 0,150, 342, 716, 22, 114, 20, 12, 981, 102, 1020 };
            numbers6 = sorting.QuickSort(numbers6, 0 , numbers6.Length - 1);
            //Printing Array
            foreach (var item in numbers6)
            {
                Console.Write(item + "  ");
            }
            Console.ReadLine();
            #endregion
        }
    }
}

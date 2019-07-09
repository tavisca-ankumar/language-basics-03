using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int[] solution = new int[dietPlans.Length];
            int[] calculateCalories = CalculateCalories(carbs, protein, fat);
            for (int i=0;i < dietPlans.Length; i++)
            {
                if (dietPlans.Length == 0)
                    continue;
                /*Storing those dishes having smallest value of either protein
                 *or fat or carbs or calories
                 */
                List<int> dishIndexWithLessValue = new List<int>();
                /*when two dish with same value is selected then to 
                *consider only those two dish "indexValue" is created
                */
                List<int> indexValue = addingAllIndexToList(protein.Length);
                for (int j= 0; j < dietPlans[i].Length; j++)
                {
                    int max = int.MinValue;
                    int min = int.MaxValue;
                    //i=fetch string in array("tFc")
                    //j =char in string('t','f',c'')
                    switch (dietPlans[i][j]) 
                    {
                        case 'P':
                            max = AddMaximumDishValueToList(indexValue, max, protein, dishIndexWithLessValue);
                            break;
                        case 'C':
                            max = AddMaximumDishValueToList(indexValue, max, carbs, dishIndexWithLessValue);
                            break;
                        case 'F':
                            max = AddMaximumDishValueToList(indexValue, max, fat, dishIndexWithLessValue);
                            break;
                        case 'T':
                            max = AddMaximumDishValueToList(indexValue, max, calculateCalories, dishIndexWithLessValue);
                            break;
                        case 'p':
                            min = AddMinimumDishValueToList(indexValue, min, protein, dishIndexWithLessValue);
                            break;
                        case 'c':
                            min = AddMinimumDishValueToList(indexValue, min, carbs, dishIndexWithLessValue);
                            break;
                        case 'f':
                            min = AddMinimumDishValueToList(indexValue, min, fat, dishIndexWithLessValue);
                            break;
                        case 't':
                            min = AddMinimumDishValueToList(indexValue, min, calculateCalories, dishIndexWithLessValue);
                            break;
                    }
                    indexValue = dishIndexWithLessValue;
                    dishIndexWithLessValue = new List<int>();
                    if (dishIndexWithLessValue.Count == 1)
                    {
                        break;
                    }
                    
                }
                /*if  two dishes with same value is in list then
                * the dish with lower index value is the answer
                */
                solution[i] = indexValue[0];
            }
            return solution;
        }

        // calculating calories for the particular dish
        private static int[] CalculateCalories(int[] carbs, int[] protein, int[] fat)
        {
            int[] calculatedCalories = new int[fat.Length];
            for (int i = 0; i < fat.Length; i++)
            {
                calculatedCalories[i] = carbs[i] * 5 + protein[i] * 5 + fat[i] * 9;
            }
            return calculatedCalories;
        }
        private static List<int> addingAllIndexToList(int length)
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < length; i++)
            {
                temp.Add(i);
            }
            return temp;
        }
        /*if two dish with maximum value of carbs is selected
         * then both dish will be added to the list so that for the 
         * next priority only these dishes are considered
         */
        private static int AddMaximumDishValueToList(List<int> index, int max, int[] dishValue, List<int> dishWithLessValue)
        {
            foreach (int k in index)
            {
                max = Math.Max(max, dishValue[k]);
            }
            foreach (int k in index)
            {
                if (max == dishValue[k])
                    dishWithLessValue.Add(k);
            }
            return max;
        }
        /*if two dish with minimum value of carbs is selected
         * then both dish will be added to the list so that for
         * the next priority these dishes are considered
         */
        private static int AddMinimumDishValueToList(List<int> index,int min, int[] dishValue, List<int> dishWithLessValue)
        {
            foreach (int k in index)
            {
                min = Math.Min(min, dishValue[k]);
            }
            foreach (int k in index)
            {
                if (min == dishValue[k])
                    dishWithLessValue.Add(k);
            }
            return min;
        }
    }
}

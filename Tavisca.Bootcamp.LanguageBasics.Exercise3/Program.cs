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
                List<int> dishIndexWithLessValue = new List<int>();
                List<int> indexValue = addingAllIndexToList(protein.Length);
                for (int j= 0; j < dietPlans[i].Length; j++)
                {
                    int max = int.MinValue;
                    int min = int.MaxValue;
                    switch (dietPlans[i][j])
                    {
                        case 'P':
                            max = AddMaximumValueToList(indexValue, max, protein, dishIndexWithLessValue);
                            break;
                        case 'C':
                            max = AddMaximumValueToList(indexValue, max, carbs, dishIndexWithLessValue);
                            break;
                        case 'F':
                            max = AddMaximumValueToList(indexValue, max, fat, dishIndexWithLessValue);
                            break;
                        case 'T':
                            max = AddMaximumValueToList(indexValue, max, calculateCalories, dishIndexWithLessValue);
                            break;
                        case 'p':
                            min = AddMinimumValueToList(indexValue, min, protein, dishIndexWithLessValue);
                            break;
                        case 'c':
                            min = AddMinimumValueToList(indexValue, min, carbs, dishIndexWithLessValue);
                            break;
                        case 'f':
                            min = AddMinimumValueToList(indexValue, min, fat, dishIndexWithLessValue);
                            break;
                        case 't':
                            min = AddMinimumValueToList(indexValue, min, calculateCalories, dishIndexWithLessValue);
                            break;
                    }
                    indexValue = dishIndexWithLessValue;
                    dishIndexWithLessValue = new List<int>();
                    if (dishIndexWithLessValue.Count == 1)
                    {
                        break;
                    }
                    
                }
                solution[i] = indexValue[0];
            }
            return solution;
        }
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
        private static int AddMaximumValueToList(List<int> index, int max, int[] dishValue, List<int> dishWithLessValue)
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
        private static int AddMinimumValueToList(List<int> index,int min, int[] dishValue, List<int> dishWithLessValue)
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

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
            int[] calculateCalories = new int[fat.Length];
            for (int i = 0; i < fat.Length; i++)
            {
                calculateCalories[i] = carbs[i] * 5 + protein[i] * 5 + fat[i] * 9;
            }
            for(int i=0;i < dietPlans.Length; i++)
            {
                if (dietPlans.Length == 0)
                    continue;
                List<int> calculateDishWithLessValueList = new List<int>();
                List<int> calculateDishWithLessValueListTemp = new List<int>();
                for (int j = 0; j < protein.Length; j++)
                    calculateDishWithLessValueListTemp.Add(j);
                for (int j= 0; j < dietPlans[i].Length; j++)
                {
                    int max = int.MinValue;
                    int min = int.MaxValue;
                    switch (dietPlans[i][j])
                    {
                        case 'P':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                max = Math.Max(max, protein[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (max == protein[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 'C':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                max = Math.Max(max, carbs[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (max == carbs[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 'F':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                max = Math.Max(max, fat[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (max == fat[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 'T':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                max = Math.Max(max, calculateCalories[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (max == calculateCalories[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 'p':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                min = Math.Min(min, protein[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (min == protein[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 'c':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                min = Math.Min(min, carbs[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (min == carbs[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 'f':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                min = Math.Min(min, fat[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (min == fat[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                        case 't':
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                min = Math.Min(min, calculateCalories[k]);
                            }
                            foreach (int k in calculateDishWithLessValueListTemp)
                            {
                                if (min == calculateCalories[k])
                                    calculateDishWithLessValueList.Add(k);
                            }
                            break;
                    }
                    calculateDishWithLessValueListTemp = calculateDishWithLessValueList;
                    calculateDishWithLessValueList = new List<int>();
                    if (calculateDishWithLessValueList.Count == 1)
                    {
                        break;
                    }
                    
                }
                solution[i] = calculateDishWithLessValueListTemp[0];
            }
            return solution;
            throw new NotImplementedException();
        }
    }
}

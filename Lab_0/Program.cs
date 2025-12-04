using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp111
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\nВведите n (целое положительное): ");
            int n = Int32.Parse(Console.ReadLine());

            if (n < 3) 
            {
                Console.WriteLine("\nПожалуйста, введите n >= 3!");
            }
            else 
            {
                Random R = new Random();// Заполняем массив рандомными числами 
                float[] Arr1 = new float[n]; 
                Console.WriteLine("\nЦелочисленный массив:\n"); 
                for (int i = 0; i < n; i++)
                {
                    Arr1[i] = R.Next(1, 6);
                    Console.Write($"{Arr1[i]}\t");
                }

                Console.Write("\n\nСумма 5/(а[i-1] * a[i] * a[i+1]) = "); 

               
                float Rec(int i) 
                                 
                {
                    if (i == (Arr1.Length) - 1) return 0; 
                                                          

                    float res1; 
                    res1 = (5 / (Arr1[i - 1] * Arr1[i] * Arr1[i + 1]));

                    return res1 + Rec(i + 1);
                }

                Console.WriteLine(Rec(1));
            }
        }
    }
}

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
            Console.Write("\nВведите n (целое положительное): ");//Ввод чисел в массиве
            int n = Int32.Parse(Console.ReadLine());

            if (n < 3) // Проверка, что n меньше 3. Если условие выполняется, то выводится сообщение об ошибке,
                       // так как для вычисления суммы нам нужно как минимум 3 элемента.
            {
                Console.WriteLine("\nПожалуйста введите n >= 3!");// Вывод сообщения, что n должно быть не меньше 3.
            }
            else //Если n >= 3, то выполняется блок кода после else
            {
                Random R = new Random(); //Создание объекта Random для генерации случайных чисел.
                float[] Arr1 = new float[n]; // Объявление массива Arr1 из n элементов типа float.
                                             // Заполнение и вывод массива
                Console.WriteLine("\nЦелочисленный массив:\n"); // Заполнение и вывод массива
                for (int i = 0; i < n; i++)
                {
                    Arr1[i] = R.Next(1, 6);
                    Console.Write($"{Arr1[i]}\t");
                }

                Console.Write("\n\nСумма 1/(а[i-1] * a[i] * a[i+1]) = "); // Вывод сообщения о том, что далее будет выведена сумма

                // Создание рекурсивной функции
                float Rec(int i) //Объявление локальной рекурсивной функции Rec,
                                 //которая принимает целый аргумент i и возвращает float.
                {
                    if (i == (Arr1.Length) - 1) return 0; //Условие выхода из рекурсии: если i равно индексу последнего элемента (Arr1.Length - 1), то возвращается 0.
                                                          //Это потому, что мы не можем брать a[i+1] для последнего элемент

                    float f; //
                    f = (1 / (Arr1[i - 1] * Arr1[i] * Arr1[i + 1]));

                    return f + Rec(i + 1);//Возвращается значение f плюс результат рекурсивного вызова Rec(i+1).
                }

                Console.WriteLine(Rec(1)); //Вывод значения расчитанного в функции  Rec
            }
        }
    }
}

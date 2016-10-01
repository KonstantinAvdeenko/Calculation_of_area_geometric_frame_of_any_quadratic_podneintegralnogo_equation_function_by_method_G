using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrirovanieMetodomGaussa
{
    class Program
    {// члены квадратного уравнения, вводятся пользователем 1 раз
        public static double t1;
        public static double t2;
        public static double t3;
        // знаки между членами квадратного уравнения, вводятся пользователем 2 раза, истина +, ложь -, истина-истина *, ложь-ложь /
        public static bool z1;
        public static bool z2;
        public static bool z3;
        public static bool z4;

        static void Main(string[] args)
        {

            Console.WriteLine("\n Результат интегрирования методом Гаусса:\n");
            double a, b, result, k;
            // a,b - пределы интегрирования
            Console.WriteLine("Введите нижний предел интеграла");
            a = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите верхний предел интеграла");
            b = double.Parse(Console.ReadLine());

            if (a > b)
            {
                Console.WriteLine("Должно быть a < b");
                Console.ReadKey();
                return; // окончание работы программы из-за несоответствия правилу
            }
            else
            {
                k = (b - a);
                // n - степень полинома Лежандра
                for (int n = 1; n < k; n++)
                { // f2 - интегрируемая функция
                    result = GaussLegendre(f2, a, b, n);
                    Console.WriteLine(" n = {0}, result = {1}", n, result);
                    Console.ReadKey();
                }
            }
        }

        public static double GaussLegendre(Function f, double a, double b, int n)
        {
            // Формула Гаусса с расчетом функции уравнения интерполяционным полиномом
            double[] x, w;
            LegendreNodesWeights(n, out x, out w);
            double sum = 0.0;
            for (int i = 0; i < n; i++)
            {
                sum += 0.5 * (b - a) * w[i] * f(0.5 * (a + b) + 0.5 * (b - a) * x[i]);
            }
            return sum;
        }
            
        public static double Legendre(double x, int deg)
        { // полином Лагранжа степени n в точке х
            double P0 = 1.0;
            double P1 = x;
            double P2 = f2(1.0 / 2.0); 
            int n = 1;
            if (deg < 0)
                throw new Exception("Bad Hermite polynomial: deg < 0");
            if (deg == 0)
                return P0;
            else if (deg == 1)
                return P1;
            else
            {
                while (n < deg)
                {
                    P2 = 2.0 * x * P1 - P0 - (x * P1 - P0) / (deg + 1);
                    P0 = P1;
                    P1 = P2;
                    n++;
                }
                return P2;
            }
        }

        public static void LegendreNodesWeights(int n, out double[] x, out double[] w)
        {
            // веса полиномов Лагранжа выбор самого подходящего так, как через 2 любые точки можно провести лишь одну линию
            double c, d, p1, p2, p3, dp;
 
            x = new double[n];
            w = new double[n];
 
            for (int i = 0; i < (n + 1) / 2; i++)
            {
                c = Math.Cos(Math.PI * (4 * i + 3) / (4 * n + 2)); // выбор нужной осевой четверти по абсциссе
                do
                {
                    p2 = 0;
                    p3 = 1;
                    for (int j = 0; j < n; j++)
                    {
                        p1 = p2;
                        p2 = p3;
                        p3 = ((2 * j + 1) * c * p2 - j * p1) / (j + 1); // подбор нужной точки между пределами интеграла
                    }
                    dp = n * (c * p3 - p2) / (c * c - 1); // подбор геометрического вида искомой линии 
                    d = c;
                    c -= p3 / dp;
                }
                while (Math.Abs(c - d) > 1e-12); // подбор подходящих для искомой линии полиномов
                x[i] = c;
                x[n - 1 - i] = -c;
                w[i] = 2 * (1 - x[i] * x[i]) / (n + 1) / (n + 1) / Legendre(x[i], n + 1) / Legendre(x[i], n + 1);
                w[n - 1 - i] = w[i];
            }
        }
        public delegate double Function(double x); // передача данных для переменной х между методами

        static double f2(double x) // функция вводимого пользователем квадратичного уравнения
        {
            if (t1 != 0 && t2 != 0 && t3 != 0)
            {
                if (z1 == true && z2 == true)
                {
                    if (z3 == false && z4 == false)
                    {
                        return t1 + t2 * x + t3 * x * x; // передача функции данного метода методу GaussLegendre
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 * t2 * x * t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 * t2 * x + t3 * x * x;
                    }
                    else
                    {
                        return t1 + t2 * x * t3 * x * x;
                    }
                }
                if (z1 == false && z2 == false)
                {
                    if (z3 == false && z4 == false)
                    {
                        return t1 / t2 * x / t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 - t2 * x - t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 - t2 * x / t3 * x * x;
                    }
                    else
                    {
                        return t1 / t2 * x - t3 * x * x;
                    }
                }
                if (z1 == true && z2 == false)
                {
                    if (z3 == false && z4 == false)
                    {
                        return t1 + t2 * x / t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 * t2 * x - t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 * t2 * x / t3 * x * x;
                    }
                    else
                    {
                        return t1 + t2 * x - t3 * x * x;
                    }
                }
                if (z1 == false && z2 == true)
                {
                    Console.WriteLine("Введите знак между t1 и t2 true = * false = + ");
                    z3 = bool.Parse(Console.ReadLine());
                    Console.WriteLine("Введите знак между t2 и t3 true = - false = / ");
                    z4 = bool.Parse(Console.ReadLine());
                    if (z3 == false && z4 == false)
                    {
                        return t1 / t2 * x + t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 - t2 * x * t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 - t2 * x + t3 * x * x;
                    }
                    else 
                    {
                        return t1 / t2 * x * t3 * x * x;
                    }
                }
            }
            // когда квадратное уравнение не полное и какие-то члены уравнения = 0, сделано чтоб меньше нагружать ЦП лишними вычислениями с 0 членами уравнения
            if (t1 != 0 && t2 != 0)
            {
                if (z1 == true)
                {
                    if (z3 == false)
                    {
                        return t1 + t2 * x;
                    }
                    if (z3 == true)
                    {
                        return t1 * t2 * x;
                    }
                }
                if (z1 == false)
                {
                    if (z3 == false)
                    {
                        return t1 / t2 * x;
                    }
                    if (z3 == true)
                    {
                        return t1 - t2 * x;
                    }
                }
            }
            if (t2 != 0 && t3 != 0)
            {
                if (z2 == true)
                {
                    if (z4 == false)
                    {
                        return t2 * x + t3 * x * x;
                    }
                    if (z4 == true)
                    {
                        return t2 * x * t3 * x * x;
                    }
                }
                if (z2 == false)
                {
                    if (z4 == false)
                    {
                        return t2 * x / t3 * x * x;
                    }
                    if (z4 == true)
                    {
                        return t2 * x - t3 * x * x;
                    }
                }
            }
            if (t1 != 0 && t3 != 0)
            {
                if (z1 == true)
                {
                    if (z3 == false)
                    {
                        return t1 + t3 * x * x;
                    }
                    if (z3 == true)
                    {
                        return t1 * t3 * x * x;
                    }
                }
                if (z1 == false)
                {
                    if (z3 == false)
                    {
                        return t1 / t3 * x * x;
                    }
                    if (z3 == true)
                    {
                        return t1 - t3 * x * x;
                    }
                }
            }
            if (t1 != 0 && t2 == 0 && t3 == 0)
            {
                return t1;
            }
            if (t1 == 0 && t2 != 0 && t3 == 0)
            {
                return t2;
            }
            if (t1 == 0 && t2 == 0 && t3 != 0)
            {
                return t3;
            }
            Console.WriteLine("Введите свободный член квадратного уравнения подинтегральной функции t1 вида t1+t2*х-t3*х*х или 0 при его отсутствии");
            t1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите зависимое 1-го уровня квадратного уравнения подинтегральной функции t2 вида t1+t2*х-t3*х*х или 0 при его отсутствии");
            t2 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите зависимое 2-го уровня квадратного уравнения подинтегральной функции t3 вида t1+t2*х-t3*х*х или 0 при его отсутствии");
            t3 = double.Parse(Console.ReadLine());
            if (t1 != 0 && t2 != 0 && t3 != 0) // ввод знаков только после ввода всех членов уравнения
            {
                Console.WriteLine("Введите знак между t1 и t2 true = +, false = -, true && true = *, false && false = /"); // знак вводится только true или false
                z1 = bool.Parse(Console.ReadLine());
                Console.WriteLine("Введите знак между t2 и t3 true = +, false = -, true && true = *, false && false = /");
                z2 = bool.Parse(Console.ReadLine());
                if (z1 == true && z2 == true)
                {
                    Console.WriteLine("Введите знак между t1 и t2 true = * false = + ");
                    z3 = bool.Parse(Console.ReadLine());
                    Console.WriteLine("Введите знак между t2 и t3 true = * false = + ");
                    z4 = bool.Parse(Console.ReadLine());
                    if (z3 == false && z4 == false)
                    {
                        return t1 + t2 * x + t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 * t2 * x * t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 * t2 * x + t3 * x * x;
                    }
                    else
                    {
                        return t1 + t2 * x * t3 * x * x;
                    }
                }
                if (z1 == false && z2 == false)
                {
                    Console.WriteLine("Введите знак между t1 и t2 true = - false = / ");
                    z3 = bool.Parse(Console.ReadLine());
                    Console.WriteLine("Введите знак между t2 и t3 true = - false = / ");
                    z4 = bool.Parse(Console.ReadLine());
                    if (z3 == false && z4 == false)
                    {
                        return t1 / t2 * x / t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 - t2 * x - t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 - t2 * x / t3 * x * x;
                    }
                    else
                    {
                        return t1 / t2 * x - t3 * x * x;
                    }
                }
                if (z1 == true && z2 == false)
                {
                    Console.WriteLine("Введите знак между t1 и t2 true = * false = + ");
                    z3 = bool.Parse(Console.ReadLine());
                    Console.WriteLine("Введите знак между t2 и t3 true = - false = / ");
                    z4 = bool.Parse(Console.ReadLine());
                    if (z3 == false && z4 == false)
                    {
                        return t1 + t2 * x / t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 * t2 * x - t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 * t2 * x / t3 * x * x;
                    }
                    else
                    {
                        return t1 + t2 * x - t3 * x * x;
                    }
                }
                else 
                {
                    Console.WriteLine("Введите знак между t1 и t2 true = - false = / ");
                    z3 = bool.Parse(Console.ReadLine());
                    Console.WriteLine("Введите знак между t2 и t3 true = * false = + ");
                    z4 = bool.Parse(Console.ReadLine());
                    if (z3 == false && z4 == false)
                    {
                        return t1 / t2 * x + t3 * x * x;
                    }
                    if (z3 == true && z4 == true)
                    {
                        return t1 - t2 * x * t3 * x * x;
                    }
                    if (z3 == true && z4 == false)
                    {
                        return t1 - t2 * x + t3 * x * x;
                    }
                    else 
                    {
                        return t1 / t2 * x * t3 * x * x;
                    }
                }
            }
            else
            {
                if (t1 != 0 && t2 != 0)
                {
                    Console.WriteLine(" Введите знак между t1 и t2 true = +, false = -, true && true = *, false && false = / "); // знак вводится только true или false
                    z1 = bool.Parse(Console.ReadLine());
                    if (z1 == true)
                    {
                        Console.WriteLine("Введите знак между t1 и t2 true = * false = + ");
                        z3 = bool.Parse(Console.ReadLine());

                        if (z3 == false )
                        {
                            return t1 + t2 * x;
                        }
                        if (z3 == true )
                        {
                            return t1 * t2 * x;
                        }
                    }
                    if (z1 == false)
                    {
                        Console.WriteLine("Введите знак между t1 и t2 true = - false = / ");
                        z3 = bool.Parse(Console.ReadLine());
                        if (z3 == false)
                        {
                            return t1 / t2 * x;
                        }
                        if (z3 == true)
                        {
                            return t1 - t2 * x;
                        }
                    }
                }
                if (t2 != 0 && t3 != 0)
                {
                    Console.WriteLine(" Введите знак между t1 и t2 true = +, false = -, true && true = *, false && false = / "); // знак вводится только true или false
                    z2 = bool.Parse(Console.ReadLine());
                    if (z2 == true)
                    {
                        Console.WriteLine("Введите знак между t2 и t3 true = * false = + ");
                        z4 = bool.Parse(Console.ReadLine());
                        if (z4 == false)
                        {
                            return t2 * x + t3 * x * x;
                        }
                        if (z4 == true)
                        {
                            return t2 * x * t3 * x * x;
                        }
                    }
                    if (z2 == false)
                    {
                        Console.WriteLine("Введите знак между t2 и t3 true = - false = / ");
                        z4 = bool.Parse(Console.ReadLine());
                        if (z4 == false)
                        {
                            return t2 * x / t3 * x * x;
                        }
                        if (z4 == true)
                        {
                            return t2 * x - t3 * x * x;
                        }
                    }
                }
                if (t1 != 0 && t3 != 0)
                {
                    Console.WriteLine(" Введите знак между t1 и t2 true = +, false = -, true && true = *, false && false = / "); // знак вводится только true или false
                    z1 = bool.Parse(Console.ReadLine());
                    if (z1 == true)
                    {
                        Console.WriteLine("Введите знак между t1 и t3 true = * false = + ");
                        z3 = bool.Parse(Console.ReadLine());
                        if (z3 == false)
                        {
                            return t1 + t3 * x * x;
                        }
                        if (z3 == true)
                        {
                            return t1 * t3 * x * x;
                        }
                    }
                    if (z1 == false)
                    {
                        Console.WriteLine("Введите знак между t1 и t3 true = - false = / ");
                        z3 = bool.Parse(Console.ReadLine());
                        if (z3 == false)
                        {
                            return t1 / t3 * x * x;
                        }
                        if (z3 == true)
                        {
                            return t1 - t3 * x * x;
                        }
                    }
                }
                if (t1 != 0 && t2 == 0 && t3 == 0)
                {
                    return t1;
                }
                if (t1 == 0 && t2 != 0 && t3 == 0)
                {
                    return t2;
                }
                if (t1 == 0 && t2 == 0 && t3 != 0)
                {
                    return t3;
                }
                else
                {
                    Console.WriteLine("Вы не ввели уравнение");
                    Console.ReadKey();
                    return 0;
                }
            }
        }
    }
  }
    
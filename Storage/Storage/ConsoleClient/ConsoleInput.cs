using System;

namespace Storage.ConsoleClient
{
    class ConsoleInput
    {
        public static double InputDouble()
        {
            bool flag = double.TryParse(Console.ReadLine(), out double val);
            while (!flag)
            {
                Console.Write("Wrong format. Please, enter the value again: ");
                flag = double.TryParse(Console.ReadLine(), out val);
            }
            return val;
        }

        public static int InputInt()
        {
            bool flag = int.TryParse(Console.ReadLine(), out int val);
            while (!flag)
            {
                Console.Write("Wrong format. Please, enter the value again: ");
                flag = int.TryParse(Console.ReadLine(), out val);
            }
            return val;
        }

        public static DateTime InputDate()
        {
            bool flag = DateTime.TryParse(Console.ReadLine(), out DateTime val);
            while (!flag)
            {
                Console.Write("Wrong format. Please, enter the value again: ");
                flag = DateTime.TryParse(Console.ReadLine(), out val);
            }
            return val;
        }
    }
}

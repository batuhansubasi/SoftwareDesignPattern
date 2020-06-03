using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    class BasariliMesajlar
    {
        public static void BaslangicMesajı()
        {
            Console.WriteLine("Program basarili sekilde basladi!");
        }

        public static void BitisMesajı()
        {
            Console.Write("Program basarili sekilde sonlandı!");
            Console.ReadLine();
        }

    }
}

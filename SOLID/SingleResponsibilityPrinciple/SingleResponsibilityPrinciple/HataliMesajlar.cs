using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    class HataliMesajlar
    {
        public static void hataliVKN(int VKN)
        {
            if (VKN != 111111111)
            {
                Console.WriteLine($"Girmiş oldugunuz VKN ( { VKN } )bilgisi yanlış! Yine de fatura kesilecektir.");
            }
        }

        public static void hataliTCKN(int TCKN)
        {
            if (TCKN != 111111111)
            {
                Console.WriteLine($"Girmiş oldugunuz VKN ( { TCKN } )bilgisi yanlış! Yine de fatura kesilecektir.");
            }
        }
    }
}

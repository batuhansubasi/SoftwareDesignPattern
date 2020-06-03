using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    public class FaturaBitir
    {
        public static void FaturaYarat(Fatura fatura)
        {
            Console.WriteLine($"Fatura yaratıldı. Gönderen VKN: { fatura.gonderenVKN } - Alıcı TCKN: { fatura.aliciTCKN } - Tutar: { fatura.tutar }");
        }
    }
}

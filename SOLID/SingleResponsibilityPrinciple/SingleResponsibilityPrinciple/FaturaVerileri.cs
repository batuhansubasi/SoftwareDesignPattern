using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    public class FaturaVerileri
    {
        public static Fatura KullanıcıdanAl()
        {
            Fatura fatura = new Fatura();

            Console.Write("Gönderen VKN giriniz: ");
            fatura.gonderenVKN = Convert.ToInt32(Console.ReadLine());

            Console.Write("Alıcı TCKN giriniz:");
            fatura.aliciTCKN = Convert.ToInt32(Console.ReadLine());

            return fatura;
        }

        public static Fatura VeritabanındanCek(Fatura fatura)
        {
            if (fatura == null)
            {
                fatura = new Fatura();
            }

            //Veritabanından gittik, tutar bilgisini çektik.
            fatura.tutar = 100.00;

            return fatura;
        }
    }
}



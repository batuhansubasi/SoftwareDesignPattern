using System;
using System.Collections.Generic;
using System.Text;

namespace SingleResponsibilityPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            BasariliMesajlar.BaslangicMesajı();

            Fatura eFatura = FaturaVerileri.KullanıcıdanAl();

            eFatura = FaturaVerileri.VeritabanındanCek(eFatura);

            HataliMesajlar.hataliVKN(eFatura.gonderenVKN);
            HataliMesajlar.hataliTCKN(eFatura.aliciTCKN);

            FaturaBitir.FaturaYarat(eFatura);

            BasariliMesajlar.BitisMesajı();
        }
    }
}

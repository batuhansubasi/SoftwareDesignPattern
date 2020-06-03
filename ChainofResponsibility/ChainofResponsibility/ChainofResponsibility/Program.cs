using System;

namespace ChainofResponsibility
{
    //160202091 - Batuhan Subasi
    //ChainofResponsibility Behavioral Pattern'i Prototype Creational Pattern'i ile birlestirildi.
    class İzinDurumu
    {
        //Yolculuk yapacak kişinin KM bilgisiyle beraber izin istemesi => Mesaj
        //public int gidilecekYol { get; set; }

        public int gidilecekYol { get; set; }

        public Kisi kisi;

        public İzinDurumu(int gidilecekYol, string isim, string soyisim)
        {
            this.gidilecekYol = gidilecekYol;
            kisi = new Kisi(isim, soyisim);
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public İzinDurumu DeepCopy()
        {
            İzinDurumu deepCopyIzinDurumu = new İzinDurumu(this.gidilecekYol, kisi.isim, kisi.soyisim);
            return deepCopyIzinDurumu;
        }
    }

    class Kisi
    {
        public string isim;
        public string soyisim;
        public Kisi(string isim, string soyisim)
        {
            this.isim = isim;
            this.soyisim = soyisim;
        }
    }

    abstract class İzinDegerlendir
    {
        //Handler sınıfımız, mesajı ele alır, soyut.
        İzinDegerlendir successor; 

        //Soyut metot, bütün miras alanlar kendi kriterlerine göre izin durumunu değerlendirecek.
        public abstract void talepDegerlendir(İzinDurumu izinDurumu);

        //Zincirdeki halkaları belirliyor
        public void ustOrganizasyonaGit(İzinDegerlendir successor)
        {
            this.successor = successor;
        }
        
        //Eğer zincirdeki halka boş değilse bir sonraki kurumun degerlendir metoduna git.
        public void next(İzinDurumu izinDurumu)
        {
            if (this.successor != null)
            {
                this.successor.talepDegerlendir(izinDurumu);
            } else
            {
                Console.WriteLine("İzin alamadınız, kriterleriniz uymuyor!");
            }
        }
    }

    class Aile : İzinDegerlendir
    {
        public override void talepDegerlendir(İzinDurumu izinDurumu)
        {
            //Kendine özgü bir kriter belirledik, farklılık olsun diye sadece.
            //Aile eğer şüpheli korona sayisi bugün 50' den düşükse izin verebiliyor, yoksa kaymakamdan izin almasını söylüyor
            int şüpheliKoronaliSayi = 50;

            if (izinDurumu.gidilecekYol < 20 && şüpheliKoronaliSayi < 75)
            {
                Console.WriteLine("Sayın " + izinDurumu.kisi.isim + " "+ izinDurumu.kisi.soyisim + " - İzin talebi aileniz tarafindan onaylandi");
            } else
            {
                this.next(izinDurumu);
            }
        }
 
    }

    class Kaymakam : İzinDegerlendir
    {
        public override void talepDegerlendir(İzinDurumu izinDurumu)
        {
            int bugünİzinVerilenKisiSayisi = 10;
            if (izinDurumu.gidilecekYol < 120 && bugünİzinVerilenKisiSayisi < 15)
            {
                Console.WriteLine("Sayın " + izinDurumu.kisi.isim + " " + izinDurumu.kisi.soyisim + " - İzin talebi kaymakam tarafindan onaylandi");
            }
            else
            {
                this.next(izinDurumu);
            }
        }

    }

    class Vali : İzinDegerlendir
    {

        public override void talepDegerlendir(İzinDurumu izinDurumu)
        {
            if (izinDurumu.gidilecekYol < 400)
            {
                Console.WriteLine("Sayın " + izinDurumu.kisi.isim + " " + izinDurumu.kisi.soyisim + " - İzin talebi vali tarafindan onaylandi");
            }
            else
            {
                this.next(izinDurumu);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Concrete Handlerlarımızı tanımladık.
            Aile aile = new Aile();
            Kaymakam kaymakam = new Kaymakam();
            Vali vali = new Vali();

            //Mesaja göre nesne sıralaması yaptık.
            aile.ustOrganizasyonaGit(kaymakam);
            kaymakam.ustOrganizasyonaGit(vali);

            //Mesajlarımızı oluşturduk ve girilen km sayisini verdik, bir tanesini shallowcopy ile kopyaladık.
            İzinDurumu izinDurumu = new İzinDurumu(100, "Batuhan", "Subasi");
            İzinDurumu yeniIzinDurumu = (İzinDurumu)izinDurumu.ShallowCopy();

            Console.WriteLine("İzin Durumları - Shallow Copy Öncesi");
            //Mesajlarımızı halkanın ilk zincirine gönderdik.
            aile.talepDegerlendir(izinDurumu);
            aile.talepDegerlendir(yeniIzinDurumu);

            Console.WriteLine("\nİzin Durumları - Shallow Copy Sonrasi");
            yeniIzinDurumu.gidilecekYol = 10;
            yeniIzinDurumu.kisi.isim = "Aslihan";
            aile.talepDegerlendir(izinDurumu);
            aile.talepDegerlendir(yeniIzinDurumu);

            Console.WriteLine("\n\n");

            //Mesajlarımızı oluşturduk ve girilen km sayisini verdik, bir tanesini deepcpoy ile kopyaladık.
            İzinDurumu izinDurumuCopy = new İzinDurumu(100, "Batuhan", "Subasi");
            İzinDurumu yeniIzinDurumuCopy = (İzinDurumu)izinDurumuCopy.DeepCopy();

            Console.WriteLine("İzin Durumları - Deep Copy Öncesi");
            //Mesajlarımızı halkanın ilk zincirine gönderdik.
            aile.talepDegerlendir(izinDurumuCopy);
            aile.talepDegerlendir(yeniIzinDurumuCopy);

            Console.WriteLine("\nİzin Durumları - Deep Copy Sonrasi");
            yeniIzinDurumuCopy.gidilecekYol = 10;
            yeniIzinDurumuCopy.kisi.isim = "Aslihan";
            aile.talepDegerlendir(izinDurumuCopy);
            aile.talepDegerlendir(yeniIzinDurumuCopy);
        }
    }
}

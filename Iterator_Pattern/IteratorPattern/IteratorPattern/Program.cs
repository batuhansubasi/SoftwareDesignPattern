using System;
using System.Collections;
using System.Collections.Generic;

// 160202091 - Batuhan Subaşı

// Bu projede kanallar arasında gezinme işlemini soyutlaştırmak icin kullanıldı.
// IKanalIterator: Koleksiyon elemanlarını almaya yarayan islemler yapıyor.
// IKanalAggregate: Iterator tipinden nesne alıyor.
// KanalConcreteAggregate:  KanalConcreteIterator ismindeki gerçek iterator nesnesi oluşturuyor, kanal koleksiyonuna sahip.
// KanalConcreteIterator: Koleksiyon elemanlarını elde ediyor.


namespace IteratorPattern
{
    //Tvkanal sınıfı
    public class TVKnali
    {
        public static int ObjectCounter = 0;
        public string KanalAdi { get; set; }

    }

    //İnterface Kanal İterator
    public interface IKanalIterator
    {
        TVKnali İlkKanal();
        TVKnali SonrakiKanal();
        TVKnali GecerliKanal();
        bool bittiMi();
    }

    //Interface Kanal Aggregate
    public interface IKanalAggregate
    {
        IKanalIterator getIterator();
    }

    //Kanal Concreate Aggregate Sınıfı
    public class KanalConcreteAggregate : IKanalAggregate
    {
        private List<TVKnali> _kanalListesi = new List<TVKnali>();
        public int kanalSayisi
        {
            get { return _kanalListesi.Count; }
        }
        public void Ekle(TVKnali t)
        {
            _kanalListesi.Add(t);
        }
        public TVKnali GetItem(int deger)
        {
            return _kanalListesi[deger];
        }
        public IKanalIterator getIterator()
        {
            return new KanalConcreteIterator(this);
        }
    }

    //Kanal Concrete Iterator 
    public class KanalConcreteIterator : IKanalIterator
    {
        private KanalConcreteAggregate kanallar;
        private int deger;
        public KanalConcreteIterator(KanalConcreteAggregate kanal)
        {
            kanallar = kanal;
        }
        public TVKnali İlkKanal()
        {
            deger = 0;
            return kanallar.GetItem(deger);
        }
        public bool bittiMi()
        {
            return deger < kanallar.kanalSayisi;
        }
        public TVKnali GecerliKanal()
        {
            return kanallar.GetItem(deger);
        }
        public TVKnali SonrakiKanal()
        {
            deger++;
            if (bittiMi())
            {
                return kanallar.GetItem(deger);
            }
            else
                return null;
        }

        class Program
        {
            static void Main(string[] args)
            {
                Factory factory = new Factory();
                KanalConcreteAggregate kca = new KanalConcreteAggregate();
                kca.Ekle(factory.getTVKnali("Kanal D"));
                kca.Ekle(factory.getTVKnali("ATV"));
                kca.Ekle(factory.getTVKnali("Show TV"));
                kca.Ekle(factory.getTVKnali("A Haber"));
                kca.Ekle(factory.getTVKnali("Kanal7"));
                IKanalIterator kanalI = kca.getIterator();
                string kanallar = "";
                kanalI.İlkKanal();
                while (kanalI.bittiMi())
                {
                    kanallar += kanalI.GecerliKanal().KanalAdi + Environment.NewLine;
                    kanalI.SonrakiKanal();
                }
                Console.WriteLine(kanallar);
                Console.ReadKey();
            }
        }

        class Factory
        {
            private static int havuzKapasite = 2;
            private static readonly Queue objPool = new Queue(havuzKapasite);

            public TVKnali getTVKnali(String kanaladi)
            {
                TVKnali oTVKnali;
                if (TVKnali.ObjectCounter >= havuzKapasite && objPool.Count > 0)
                {
                    oTVKnali = HavuzdanGetir(kanaladi);
                }
                else
                {
                    oTVKnali = GetNewTvKnali(kanaladi);
                }
                return oTVKnali;
            }

            private TVKnali GetNewTvKnali(String kanaladi)
            {
                //yeni kanal üret
                TVKnali kanal = new TVKnali { KanalAdi = kanaladi };
                objPool.Enqueue(kanal);
                return kanal;
            }

            protected TVKnali HavuzdanGetir(String kanaladi)
            {
                TVKnali kanal;

                if (objPool.Count > 0)
                {
                    kanal = (TVKnali)objPool.Dequeue();
                    TVKnali.ObjectCounter--;
                }
                else
                {
                    kanal = new TVKnali { KanalAdi = kanaladi };
                }
                return kanal;
            }

        }

    }
}
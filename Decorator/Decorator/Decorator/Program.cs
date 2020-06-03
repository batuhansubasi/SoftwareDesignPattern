using System;

namespace Decorator
{
    //160202091 - Batuhan Subaşı
    //Decorator Structural Pattern'i, Thread Safe Singleton Creational Pattern'i ile birleştirildi.

    //Component ve Decorator classın türediği interface
    public interface IKoronaOnlemler
    {
        void maskeTak();
        bool sosyalMesafeKoru(double uzaklik);
    }

    //Varsayılan önlemlerin tanımlanması, Component
    public class KoronaOnlemler : IKoronaOnlemler
    {
        //instance
        private static KoronaOnlemler koronaOnlemler = null;
        private static readonly object padlock = new object();

        private KoronaOnlemler()
        {
            Console.WriteLine("Sadece bir kez korona önlem sınıfından nesne oluşturulmasına izin verdik\n");
        }

        public static KoronaOnlemler getKoronaOnlemler
        {
            get
            {
                lock (padlock)
                {
                    if (koronaOnlemler == null)
                    {
                        koronaOnlemler = new KoronaOnlemler();
                    }
                    return koronaOnlemler;
                }
            }
        }

        public void maskeTak()
        {
            Console.WriteLine("Tam korumalı maske takildi.");
        }

        public bool sosyalMesafeKoru(double uzaklik)
        {
            if (uzaklik > 2)
            {
                return true;
            }
            else return false;
        }
    }

    //Decorator => Dinamik olarak özelliğin ve davranışın eklendiği soyut class.
    public abstract class DecoratorKoronaOnlemler : IKoronaOnlemler
    {
        private IKoronaOnlemler iKoronaOnlemler;

        public DecoratorKoronaOnlemler(IKoronaOnlemler iKoronaOnlemler)
        {
            this.iKoronaOnlemler = iKoronaOnlemler;
        }

        public void maskeTak()
        {
            iKoronaOnlemler.maskeTak();
        }

        public bool sosyalMesafeKoru(double uzaklik)
        {
            if (iKoronaOnlemler.sosyalMesafeKoru(uzaklik))
                return true;
            else return false;
        }
    }

    //ConcreteDecorator => Dinamik olarak özelliğin ve davranışın eklendiği somut class.
    class KoronaYeniOnlem : DecoratorKoronaOnlemler
    {
        public KoronaYeniOnlem(IKoronaOnlemler iKoronaOnlemler) : base(iKoronaOnlemler) //base ile taban sınıfın üyelerine erişmek için kullanılır
        {
        }
        //Sonradan eklenilen metot, özellik
        public void sokagaCikma(int gün)
        {
            //Eğer haftasonu ise
            if(gün == 6 || gün == 7)
            {
                Console.WriteLine("Bugün haftasonu, dışarı çıkmayın!");
            } else
            {
                Console.WriteLine("Haftaici, dısarı cikabilirsiniz!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Component
            KoronaOnlemler koronaOnlemler = KoronaOnlemler.getKoronaOnlemler;
            
            //Somut decorator nesnesi olusturduk, componenti içeri verdik.
            KoronaYeniOnlem koronaYeniOnlemler = new KoronaYeniOnlem(koronaOnlemler);

            //Default gelen metotlar, classın kendisinde olanlar
            koronaYeniOnlemler.maskeTak();
            bool uzaklikKontrol = koronaYeniOnlemler.sosyalMesafeKoru(6);
            if (uzaklikKontrol)            
                Console.WriteLine("Sosyal mesafeyi korudugunuz icin tesekkurler");    
            else
                Console.WriteLine("Sosyal mesafe kuralına uymadınız !!!");
            
            //Sonradan eklenilen metot.
            //Classa sokagaCikma metodu eklemeden, nesneye metot kazandırdık.
            koronaYeniOnlemler.sokagaCikma(6); //Haftasonu: 6,7

            Console.WriteLine("\n");

            KoronaOnlemler koronaOnlemlerv2 = KoronaOnlemler.getKoronaOnlemler;
            KoronaYeniOnlem koronaYeniOnlemlerv2 = new KoronaYeniOnlem(koronaOnlemlerv2);
            koronaYeniOnlemlerv2.maskeTak();
            if (koronaYeniOnlemlerv2.sosyalMesafeKoru(2.5)) Console.WriteLine("Sosyal mesafeyi korudugunuz icin tesekkurler");
            koronaYeniOnlemlerv2.sokagaCikma(5); //Haftasonu: 6,7

        }
    }
}

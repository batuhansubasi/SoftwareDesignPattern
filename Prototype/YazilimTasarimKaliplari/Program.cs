using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace Main
{
    class Program
    {
        [Serializable]
        public class Araba
        {
            public int Id { get; set; }
            public string Marka { get; set; }
            public Parcalar Parcalar { get; set; }

            public Araba ShallowCopy()
            {
                return (Araba)this.MemberwiseClone();
            }

            public Araba DeepCopy()
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, this);
                    ms.Position = 0;

                    return (Araba)formatter.Deserialize(ms);
                }
            }
        }
        [Serializable]
        public class Parcalar 
        {
            public int kapiSayisi { get; set; }
            public int tekerlekSayisi { get; set; }
        }

        static void Main(string[] args)
        {
            var araba1 = new Araba();
            araba1.Id = 1;
            araba1.Marka = "BMW";

            Parcalar pc1 = new Parcalar();
            pc1.kapiSayisi = 2;
            pc1.tekerlekSayisi = 4;
            araba1.Parcalar = pc1;

            Console.WriteLine("Shallow Copy Öncesi");
            Console.WriteLine("İlk Araba:\nID: " + araba1.Id + ", Marka: " + araba1.Marka + ", Kapi Sayisi: " + araba1.Parcalar.kapiSayisi + ", Tekerlek Sayisi: " + araba1.Parcalar.tekerlekSayisi);

            var araba2 = araba1.ShallowCopy();

            Console.WriteLine("\nShallow Copy Sonrası");
            Console.WriteLine("İlk Araba:\nID: " + araba1.Id + ", Marka: " + araba1.Marka + ", Kapi Sayisi: " + araba1.Parcalar.kapiSayisi + ", Tekerlek Sayisi: " + araba1.Parcalar.tekerlekSayisi);
            Console.WriteLine("İkinci Araba:\nID: " + araba2.Id + ", Marka: " + araba2.Marka + ", Kapi Sayisi: " + araba2.Parcalar.kapiSayisi + ", Tekerlek Sayisi: " + araba2.Parcalar.tekerlekSayisi);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var araba3 = new Araba();
            araba3.Id = 2;
            araba3.Marka = "Mercedes";

            Parcalar pc2 = new Parcalar();
            pc2.kapiSayisi = 4;
            pc2.tekerlekSayisi = 6;
            araba3.Parcalar = pc2;

            Console.WriteLine("\n\nDeep Copy Öncesi");
            Console.WriteLine("Ucuncu Araba:\nID: " + araba3.Id + ", Marka: " + araba3.Marka + ", Kapi Sayisi: " + araba3.Parcalar.kapiSayisi + ", Tekerlek Sayisi: " + araba3.Parcalar.tekerlekSayisi);

            var araba4 = araba3.DeepCopy();

            Console.WriteLine("\nDeep Copy Sonrası");
            Console.WriteLine("Ucuncu Araba:\nID: " + araba3.Id + ", Marka: " + araba3.Marka + ", Kapi Sayisi: " + araba3.Parcalar.kapiSayisi + ", Tekerlek Sayisi: " + araba3.Parcalar.tekerlekSayisi);
            Console.WriteLine("Dorduncu Araba:\nID: " + araba4.Id + ", Marka: " + araba4.Marka + ", Kapi Sayisi: " + araba4.Parcalar.kapiSayisi + ", Tekerlek Sayisi: " + araba4.Parcalar.tekerlekSayisi);

            Console.Read();
        }
    }
}
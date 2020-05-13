using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 160202091 - Batuhan Subasi

// Bu çalışmadaki patternin amacı: Vize ve Final bilgi sistemi açıldığında, sadece bu konu ile ilgilenen öğretmen, öğrenci ve dekan nesnelerine bildirim atılması. Abone olmayanların kayıt dışı olması.
// Concrete Subscribers: Öğretmen, Öğrenci, Dekan
// Publisher: VizeFinal
// Observer: Öğretmen, öğrenci ve dekanların soyut gözlemcisi.


namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            VizeFinal not = new VizeFinal();

            Ögretmen ogretmen = new Ögretmen();
            Ögrenci öğrenci = new Ögrenci();
            Dekan dekan = new Dekan();

            not.AboneEkle(ogretmen);
            not.AboneEkle(öğrenci);
            not.AboneEkle(dekan);

            not.DersAdi = "Bulanık Mantık";
            not.HocaAdi = "Batuhan Subasi";
            not.Kredisi = 5;
            not.NotGirildimi = true;

            not.AboneCikar(ogretmen);

            not.DersAdi = "Bitirme Projesi";
            not.HocaAdi = "Alev Mutlu";
            not.Kredisi = 4;
            not.NotGirildimi = true;

            Console.ReadKey(true);
        }
    }
    //Observer arayüzü bir çok tipin abone olması için
    abstract public class Observer
    {
        public abstract void Güncelle();
    }

    //Concrete sınıflar Takip edecekler
    public class Ögretmen : Observer
    {
        public override void Güncelle()
        {
            Console.WriteLine("Öğretmenin Eriştiği Yer");
        }
    }
    public class Ögrenci : Observer
    {
        public override void Güncelle()
        {
            Console.WriteLine("Öğrencinin Eriştiği Yer");
        }
    }
    public class Dekan : Observer
    {
        public override void Güncelle()
        {
            Console.WriteLine("Dekanın Eriştiği Yer");
        }
    }
    public class VizeFinal
    {
        public string DersAdi { get; set; }
        public string HocaAdi { get; set; }
        public int Kredisi { get; set; }
        bool notGirildimi;
        public bool NotGirildimi
        {
            get { return notGirildimi; }
            set
            {
                if (value == true)
                {
                    HaberVer();
                    notGirildimi = value;
                }
                else
                    notGirildimi = value;
            }
        }
        //Subject nesnesi kendisine abone olan gözlemcileri bu koleksiyonda tutacaktır.
        List<Observer> Gozlemciler;
        public VizeFinal()
        {
            this.Gozlemciler = new List<Observer>();
        }
        //Gözlemci ekle
        public void AboneEkle(Observer observer)
        {
            Gozlemciler.Add(observer);
        }
        //Gözlemci çıkar
        public void AboneCikar(Observer observer)
        {
            Gozlemciler.Remove(observer);
        }
        //Herhangi bir güncelleme olursa ilgili gözlemcilere haber verecek metodumuzdur.
        public void HaberVer()
        {
            Gozlemciler.ForEach(g =>
            {
                g.Güncelle();
            });
        }

    }

}
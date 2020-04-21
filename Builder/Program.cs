using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace YazilimTasarimKaliplari_Builder
{
    class CookDirector
    {
        public void constructPizza(PizzaBuilder pizzaBuilder)
        {
            pizzaBuilder.SetHamur();
            pizzaBuilder.SetSos();
            pizzaBuilder.SetMalzeme();
        }
    }

    abstract class PizzaBuilder
    {
        protected Pizza pizza;
        public Pizza Pizza
        {
            get { return pizza; }
        }
        public abstract void SetHamur();
        public abstract void SetSos();
        public abstract void SetMalzeme();
    }

    class Vegetarian : PizzaBuilder
    {
        public Vegetarian()
        {
            pizza = new Pizza();
        }
        public override void SetHamur() => pizza.Hamur = "İnce";
        public override void SetSos() => pizza.Sos = "Az";
        public override void SetMalzeme() => pizza.Malzeme = "Mozarella, biber, mantar";
    }

    class Classic : PizzaBuilder
    {
        public Classic()
        {
            pizza = new Pizza();
        }
        public override void SetHamur() => pizza.Hamur = "Kalın";
        public override void SetSos() => pizza.Sos = "Bol";
        public override void SetMalzeme() => pizza.Malzeme = "Sucuk, mozarella, biber";
    }

    class Pizza
    {
        public string Hamur { get; set; }
        public string Sos { get; set; }
        public string Malzeme { get; set; }

        public override string ToString()
        {
            return $"Hamur Seçiminiz: {Hamur} , Sos Seçiminiz: {Sos} ve Malzemeler: {Malzeme} olarak siparişiniz verilmiştir.";
        }
    }

    class Client
    {
        static void Main(string[] args)
        {
            PizzaBuilder pizzaBuilder = new Vegetarian();
            CookDirector pizzaDirector = new CookDirector();
            pizzaDirector.constructPizza(pizzaBuilder);

            Console.WriteLine(pizzaBuilder.Pizza.ToString());

            pizzaBuilder = new Classic();
            pizzaDirector.constructPizza(pizzaBuilder);
            Console.WriteLine(pizzaBuilder.Pizza.ToString());

            Console.Read();
        }
    }

}
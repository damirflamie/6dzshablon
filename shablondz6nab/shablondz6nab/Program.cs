using System;
using System.Collections.Generic;

namespace ObserverExample
{
    interface IObserver
    {
        void Update(string currency, decimal rate);
    }

    interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers(string currency, decimal rate);
    }

    class CurrencyExchange : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(string currency, decimal rate)
        {
            foreach (var obs in observers)
            {
                obs.Update(currency, rate);
            }
        }

        public void SetRate(string currency, decimal rate)
        {
            Console.WriteLine($"\n>>> Новый курс {currency}: {rate}");
            NotifyObservers(currency, rate);
        }
    }

    class MobileApp : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Мобильное приложение] {currency}: {rate}");
        }
    }

    class WebSite : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Сайт] Обновлён курс {currency}: {rate}");
        }
    }

    class BankSystem : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Банк] Обновили данные по {currency}: {rate}");
        }
    }

    class Program
    {
        static void Main()
        {
            CurrencyExchange exchange = new CurrencyExchange();

            IObserver mobile = new MobileApp();
            IObserver web = new WebSite();
            IObserver bank = new BankSystem();

            exchange.AddObserver(mobile);
            exchange.AddObserver(web);
            exchange.AddObserver(bank);

            exchange.SetRate("USD", 481.50m);
            exchange.SetRate("EUR", 505.30m);

            Console.WriteLine("\nУдаляем сайт из наблюдателей...");
            exchange.RemoveObserver(web);

            exchange.SetRate("USD", 484.10m);

            Console.WriteLine("\nНаблюдение завершено!");
        }
    }
}

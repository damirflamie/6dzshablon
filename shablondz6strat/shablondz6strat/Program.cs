using System;

namespace StrategyExample
{
    interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }

    class CardPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"[Карта] Оплачено {amount} тенге.");
        }
    }

    class PayPalPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"[PayPal] Оплачено {amount} тенге.");
        }
    }

    class CryptoPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"[Крипта] Отправлено {amount} тенге в эквиваленте BTC.");
        }
    }

    class PaymentContext
    {
        private IPaymentStrategy _payment;

        public void SetPaymentMethod(IPaymentStrategy payment)
        {
            _payment = payment;
        }

        public void PayNow(decimal amount)
        {
            if (_payment != null)
                _payment.Pay(amount);
            else
                Console.WriteLine("Способ оплаты не выбран!");
        }
    }

    class Program
    {
        static void Main()
        {
            PaymentContext context = new PaymentContext();

            Console.WriteLine("Выберите способ оплаты:");
            Console.WriteLine("1 - Банковская карта");
            Console.WriteLine("2 - PayPal");
            Console.WriteLine("3 - Криптовалюта");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    context.SetPaymentMethod(new CardPayment());
                    break;
                case "2":
                    context.SetPaymentMethod(new PayPalPayment());
                    break;
                case "3":
                    context.SetPaymentMethod(new CryptoPayment());
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            Console.Write("Введите сумму: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            context.PayNow(amount);

            Console.WriteLine("Оплата завершена!");
        }
    }
}

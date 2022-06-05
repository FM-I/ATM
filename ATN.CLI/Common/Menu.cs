using ATM.Application.Interfaces;
using ATM.Domain.Models;

namespace ATN.CLI.Common
{
    public class Menu : IMenu, IDisposable
    {
        private readonly IAuthenticate _authenticate;
        private readonly IBalance _balance;
        private Account _account;
        private AutomatedTellerMachine _atm;

        public Menu(IBalance balance, IAuthenticate authenticate, AutomatedTellerMachine atm)
        {
            _balance = balance ?? throw new ArgumentNullException($"{nameof(balance)} is null");
            _authenticate = authenticate ?? throw new ArgumentNullException($"{nameof(balance)} is null");
            _atm = atm ?? throw new ArgumentNullException($"{nameof(atm)} is null");


            _authenticate.OnAuth += (string mess) =>
            {
                Console.WriteLine(mess);
            };

            _balance.OnAddBalace += ActionOnBalace;
        }

        private void ActionOnBalace(BalanceResult obj)
        {
            if (obj == null)
                return;

            if (obj.Succeeded)
            {
                Console.WriteLine(obj.SucceededMessage);
            }
            else
            {
                foreach (var error in obj.Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }

        public void Authenticate()
        {
            Account account = null;

            do
            {
                Console.Clear();

                Console.Write("Введіть номер карти: ");
                var cardNumber = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(cardNumber))
                {
                    Console.Clear();
                    Console.WriteLine("Номер карти не може бути порожнім!");
                    continue;
                }

                Console.Write("Введіть пароль від карти: ");
                var password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.Clear();
                    Console.WriteLine("Пароль карти не може бути порожнім!");
                    continue;
                }

                account = _authenticate.Auth(cardNumber, password);

                if (account == null)
                {
                    Console.WriteLine("Спробувати ще раз введіть [Y], для виходу натисніть будь-яку клавішу.");
                    var key = Console.ReadKey();

                    if (key.Key != ConsoleKey.Y)
                        return;
                }

                
            } while (account == null);

            _account = account;

            Show();
        }

        public void BalanceAction(Func<AutomatedTellerMachine ,Account, decimal, BalanceResult> func)
        {
            if (_account == null)
                return;

            var summ = InputBalance();

            var result = func(_atm, _account, summ);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine(error);

                return;
            }

            Console.WriteLine(result.SucceededMessage);
        }

        public void TransferBalance()
        {
            if (_account == null)
                return;

            string cardNumber = "";

            do
            {
                Console.Write("Введіть номер карти: ");
                cardNumber = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(cardNumber))
                {
                    Console.Clear();
                    Console.WriteLine("Номер карти не може бути порожнім!");

                    Console.WriteLine("Спробувати ще раз введіть [Y], для виходу натисніть будь-яку клавішу.");
                    var key = Console.ReadKey();

                    if (key.Key != ConsoleKey.Y)
                        return;

                }

            } while (string.IsNullOrWhiteSpace(cardNumber));

            var summ = InputBalance();

            var result = _balance.TransferBalance(_atm, cardNumber, summ);
                
            if(result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine(error);

                return;
            }

            Console.WriteLine(result.SucceededMessage);
        }

        public void Close()
        {
            _account = null;
        }

        public void Show()
        {
            while (true)
            {
                
                Console.WriteLine("1.Поповнити баланс");
                Console.WriteLine("2.Зняти з балансу");
                Console.WriteLine("3.Показати баланс");
                Console.WriteLine("4.Переказ на іншу картку");
                Console.WriteLine("5.Баланс в банкоматі");
                Console.WriteLine("0.Вихід");
                Console.Write("Оберіть пункт: ");
                var command = Console.ReadKey();

                Console.Clear();

                switch (command.Key)
                {
                    case ConsoleKey.D1:
                         BalanceAction(_balance.AddBalance);
                        break;
                    case ConsoleKey.D2:
                        BalanceAction(_balance.WithDrawal);
                        break;
                    case ConsoleKey.D3:
                        ShowBalance();
                        break;
                    case ConsoleKey.D4:
                        TransferBalance();
                        break;
                    case ConsoleKey.D5:
                        ShowAtmBalance();
                        break;
                    case ConsoleKey.D0:
                        Close();
                        return;
                    default:
                        Console.WriteLine("Обрана невірна команда!");
                        break;
                }

            }
        }

        private void ShowAtmBalance()
        {
            if (_atm == null)
                return;

            Console.WriteLine($"Баланс банкомата: {_atm.Cash}");
        }

        public void ShowBalance()
        {
            if (_account == null)
                return;

            Console.WriteLine($"Баланс: {_account.Balance}");
        }


        public decimal InputBalance()
        {
            decimal summ = 0;

            do
            {
                Console.Write("Введіть суму: ");
                var parseResult = decimal.TryParse(Console.ReadLine(), out summ);

                if (parseResult == false)
                {
                    Console.WriteLine("Помилка вводу!");
                    summ = 0;
                }
                else if (summ < 0)
                {
                    Console.WriteLine("Некоректна сумма.\nВведіть суму більшу за 0!");
                    summ = 0;
                }

            } while (summ <= 0);

            return summ;
        }

        public void Dispose()
        {
            if (_account != null)
                _account = null;
        }
    }
}

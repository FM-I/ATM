using ATM.Application.Data;
using ATM.Application.Interfaces;
using ATN.CLI.Common;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var menuBuilder = new MenuBuilder();

while (true)
{
    for (int i = 0; i < Repository.Banks.Count; ++i)
    {
        Console.WriteLine($"{i + 1}. {Repository.Banks[i].Name}");
    }

    Console.WriteLine("0. Вихід");
    Console.Write("Оберіть банк: ");
    var number = Console.ReadLine();

    if (int.TryParse(number, out int bankNumber))
    {
        if (bankNumber == 0) break;

        bankNumber--;

        if (bankNumber >= 0 && bankNumber  < Repository.Banks.Count)
        {
            while (true)
            {
                for (int i = 0; i < Repository.Banks[bankNumber].AutomatedTellerMachines.Count; ++i)
                {
                    Console.WriteLine($"{i + 1}. {Repository.Banks[bankNumber].AutomatedTellerMachines[i].Name}");
                }

                Console.WriteLine("0. Назад");
                Console.Write("Оберіть банкомат: ");
                number = Console.ReadLine();

                if (int.TryParse(number, out int atmNumber))
                {
                    if (atmNumber == 0) break;

                    atmNumber--;

                    if (atmNumber >= 0 && atmNumber < Repository.Banks.Count)
                    {
                        var atm = Repository.Banks[bankNumber].AutomatedTellerMachines[atmNumber];

                        IMenu menu = menuBuilder.Build(atm);

                        while (true)
                        {
                            Console.WriteLine("1.Вхід");
                            Console.WriteLine("0.Завершити роботу");

                            Console.Write("Оберіть пункт: ");
                            var command = Console.ReadKey();

                            Console.Clear();

                            if(ConsoleKey.D0 == command.Key)
                                break;

                            switch (command.Key)
                            {
                                case ConsoleKey.D1:
                                    menu.Authenticate();
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}

Console.WriteLine("Допобачення.");

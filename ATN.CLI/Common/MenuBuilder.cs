using ATM.Application.Common;
using ATM.Domain.Models;

namespace ATN.CLI.Common
{
    public class MenuBuilder
    {
        public Menu Build(AutomatedTellerMachine atm)
        {
            var balanceController = new BalanceController();
            var authenticate = new Authenticate();
            var menu = new Menu(balanceController, authenticate, atm);

            return menu;
        }
    }
}

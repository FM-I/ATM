using ATM.Application.Data;
using ATM.Application.Interfaces;
using ATM.Domain.Models;

namespace ATM.Application.Common
{
    public class BalanceController : IBalance
    {
        public event Action<BalanceResult> OnAddBalace;
        public event Action<BalanceResult> OnWithDrawalBalace;

        public BalanceResult AddBalance(AutomatedTellerMachine atm, Account account, decimal summ)
        {
            account.Balance += summ;
            atm.Cash += summ;

            var result = new BalanceResult { Succeeded = true, SucceededMessage = "Баланс успішно поповнено." };

            OnAddBalace?.Invoke(result);

            return result;
        }

        public BalanceResult TransferBalance(AutomatedTellerMachine atm, string cardTo, decimal summ)
        {
            var account = Repository.Cards.Find(c => c.Number == cardTo);

            if(account == null)
               return new() { Succeeded = false, Errors = new() { "Введений невірний номер карти отримувача." } };

            var resultAddBalance = AddBalance(atm, account, summ);

            if (resultAddBalance.Succeeded == false)
            {
                return resultAddBalance;
            }

            return new() { Succeeded = true, SucceededMessage = "Транзакція пройшла успішно." };
        }

        public BalanceResult WithDrawal(AutomatedTellerMachine atm, Account account, decimal summ)
        {
            var result = new BalanceResult { Succeeded = true, SucceededMessage = "Зняття з балансу пройшло успішно." };

            if (account.Balance < summ)
                result = new BalanceResult() { Succeeded = false, Errors = new() { "На балансі недостатньо коштів для зняття." } };

            if (atm.Cash < summ)
                result = new BalanceResult() { Succeeded = false, Errors = new() { "В банкоматі недостатньо коштів для зняття." } };

            if (result.Succeeded)
            {
                account.Balance -= summ;
                atm.Cash -= summ;
            }

            OnWithDrawalBalace?.Invoke(result);

            return result;
        }
    }
}

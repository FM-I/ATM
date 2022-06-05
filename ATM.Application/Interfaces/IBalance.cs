using ATM.Domain.Models;

namespace ATM.Application.Interfaces
{
    public interface IBalance
    {
        event Action<BalanceResult> OnAddBalace;
        event Action<BalanceResult> OnWithDrawalBalace;
        BalanceResult AddBalance(AutomatedTellerMachine atm, Account card, decimal summ);
        BalanceResult WithDrawal(AutomatedTellerMachine atm, Account card, decimal summ);
        BalanceResult TransferBalance(AutomatedTellerMachine atm, string cardTo, decimal summ);
    }
}

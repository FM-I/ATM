namespace ATM.Domain.Models
{
    public class BalanceResult
    {
        public bool Succeeded { get; set; }
        public string SucceededMessage { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}

namespace CustomERPSolution_Invoices_Currency.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; } // Example: USD, EUR, GBP
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}

namespace CustomERPSolution_Invoices_Currency.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<QuotationItem> QuotationItems { get; set; }
        public decimal TotalAmount { get; set; }
    }

}

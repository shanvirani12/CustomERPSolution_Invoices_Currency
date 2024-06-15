namespace CustomERPSolution_Invoices_Currency.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }

}

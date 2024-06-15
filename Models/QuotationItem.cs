namespace CustomERPSolution_Invoices_Currency.Models
{
    public class QuotationItem
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

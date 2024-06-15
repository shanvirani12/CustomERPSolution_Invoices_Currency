namespace CustomERPSolution_Invoices_Currency.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

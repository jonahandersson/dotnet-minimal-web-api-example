namespace DotNetMinimalAPIDemo.EntityClasses
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public Product ProductId { get; set; }
       

    }
}

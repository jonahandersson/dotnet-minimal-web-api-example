namespace DotNetMinimalAPIDemo.EntityClasses
{
    public class Order
    {
        public int OrderID { get; set; }
        public List<Order> OrderItems { get; set; }
      
    }
}

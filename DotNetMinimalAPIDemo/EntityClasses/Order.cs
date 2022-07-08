using AdventureWorksMinimalAPIDemo.EntityClasses;

namespace DotNetMinimalAPIDemo.EntityClasses
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public Guid OrderDetailsId { get; set; }

        public int CustomerId { get; set; }
    
    }
}

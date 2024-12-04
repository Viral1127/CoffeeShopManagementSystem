using System.ComponentModel.DataAnnotations;

namespace CoffeeShopManagementSystem.Models
{
    public class OrderModel
    {
        
        public int? OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]

        public int CustomerID { get; set; }
        [Required]

        public string PaymentMode { get; set; }
        [Required]

        public int? TotalAmount { get; set; }
        [Required]

        public string ShippingAddress { get; set; }
        [Required]

        public int UserID { get; set; }

    }
    public class OrderDropdownModel
    {
        public int? OrderID { get; set; }
    }
}


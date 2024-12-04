using System.ComponentModel.DataAnnotations;

namespace CoffeeShopManagementSystem.Models
{
    public class ProductModel
    {

        public int? ProductID { get; set; }


        [Required(ErrorMessage = "Please enter Product Name")]
        

        public string ProductName { get; set; }


        [Required(ErrorMessage = "Please enter Product Price")]

        public int ProductPrice { get; set; }


        [Required(ErrorMessage = "Please Enter product code")]
        public string ProductCode { get; set; }


        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        public int UserID { get; set; }

    }

    public class ProductDropdownModel
    {
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
    }


}


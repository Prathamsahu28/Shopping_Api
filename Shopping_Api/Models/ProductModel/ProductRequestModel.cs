using System.ComponentModel.DataAnnotations;


namespace Zero.Shopping_Api.Models
{
    public class ProductRequestModel
    {


        [Required(ErrorMessage = "Item Name Is Required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Type Is Required.")]
        public int Quantity { get; set; }
       
        [Required(ErrorMessage = "Type Is Required.")]
        public int Price { get; set; }


    }
}

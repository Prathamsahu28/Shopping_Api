﻿using System.ComponentModel.DataAnnotations;


namespace Zero.AspNetCoreServiceProjectExample.Models
{
    public class ItemRequestModel
    {
       

        [Required(ErrorMessage = "Item Name Is Required.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Type Is Required.")]
        public int Quantity { get; set; }
       
        [Required(ErrorMessage = "Type Is Required.")]
        public int Price { get; set; }


    }
}

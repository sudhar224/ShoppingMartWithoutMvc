using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingMartWithoutMVC.Models;

namespace ShoppingMartWithoutMVC.Models
{
    public class CartItem
    {
        public AddProduct Product { get; set; } 
        public int Quantity { get; set; } 
    }
}
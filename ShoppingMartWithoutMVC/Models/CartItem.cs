using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingMartWithoutMVC.Models;

namespace ShoppingMartWithoutMVC.Models
{
    public class CartItem
    {
        public AddProduct Product { get; set; } // Details of the product
        public int Quantity { get; set; } // Quantity of the product in the cart
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingMartWithoutMVC.Models
{
    public class AddProduct
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string price { get; set; }
        public string img_path { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string user_name { get; set; }
    }
}
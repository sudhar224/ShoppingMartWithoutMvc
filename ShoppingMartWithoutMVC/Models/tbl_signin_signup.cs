using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingMartWithoutMVC.Models
{
    public class tbl_signin_signup
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string mobile_number { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }

    }
}
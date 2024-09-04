using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingMartWithoutMVC.Models;

namespace ShoppingMartWithoutMVC.Controllers
{
    public class CartController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        private static List<CartItem> Cart = new List<CartItem>();

        // GET: Cart
        public ActionResult Index()
        {
            return View(Cart);
        }

        // POST: Cart/Add
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = GetProductById(id); // Implement this method to fetch product details by ID
            if (product != null)
            {
                var cartItem = Cart.FirstOrDefault(c => c.Product.id == id);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    Cart.Add(new CartItem
                    {
                        Product = product,
                        Quantity = quantity
                    });
                }
            }
            return RedirectToAction("Index");
        }

        private AddProduct GetProductById(int id)
        {
            AddProduct addProduct_obj = new AddProduct();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_product_details " + id, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    addProduct_obj = new AddProduct
                    {
                        img_path = sdr["img_path"].ToString(),
                        product_id = Convert.ToInt32(sdr["product_id"].ToString()),
                        product_name = sdr["product_name"].ToString(),
                        price = sdr["price"].ToString()
                    };
                }
                sdr.Close();
                con.Close();
            }
           
            return (addProduct_obj);
         
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var itemToRemove = Cart.FirstOrDefault(c => c.Product.id == id);
            if (itemToRemove != null)
            {
                Cart.Remove(itemToRemove);
            }
            return RedirectToAction("Index");
        }
    }
}
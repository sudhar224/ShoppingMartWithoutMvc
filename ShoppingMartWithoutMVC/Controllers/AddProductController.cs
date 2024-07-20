using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using ShoppingMartWithoutMVC.Models;
using System.IO;

namespace ShoppingMartWithoutMVC.Controllers
{
    public class AddProductController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        // GET: AddProduct
        public ActionResult Index()
        {
            List<AddProduct> addProducts_obj = new List<AddProduct>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_display_product",con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    addProducts_obj.Add(new AddProduct
                    {
                        id = Convert.ToInt32( sdr["id"].ToString()),
                        product_id = Convert.ToInt32(sdr["product_id"].ToString()),
                        product_name = sdr["product_name"].ToString(),
                        price = (sdr["price"].ToString()),
                        img_path = sdr["img_path"].ToString()

                    });
                }
                sdr.Close();
                con.Close();
            }

            return View(addProducts_obj);
        }

        // GET: AddProduct/Details/5
        public ActionResult Details(int id)
        {
            AddProduct addProduct_obj = new AddProduct();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_product_details "+id, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    addProduct_obj = new AddProduct
                    {
                        id = Convert.ToInt32( sdr["id"].ToString()),
                        img_path = sdr["img_path"].ToString(),
                        product_id = Convert.ToInt32(sdr["product_id"].ToString()),
                        product_name = sdr["product_name"].ToString(),
                        price = sdr["price"].ToString()
                    };
                }
                sdr.Close();
                con.Close();
            }
            return View(addProduct_obj);
        }

        // GET: AddProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddProduct/Create
        [HttpPost]
        public ActionResult Create(AddProduct AddProduct_obj)
        {
            try
            {
                if (AddProduct_obj.ImageFile != null && AddProduct_obj.ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(AddProduct_obj.ImageFile.FileName);
                    string extension = Path.GetExtension(AddProduct_obj.ImageFile.FileName);
                    fileName = fileName + extension;
                    AddProduct_obj.img_path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    AddProduct_obj.ImageFile.SaveAs(fileName);
                }
           

                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_add_product " + AddProduct_obj.product_id + ",'" + AddProduct_obj.product_name + "'," + AddProduct_obj.price + ",'" +AddProduct_obj.img_path +"'", con);
                    int a = cmd.ExecuteNonQuery();
                    if(a > 0)
                    {
                        Response.Write("Product Added");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.Write("Not added");
                    }
                    con.Close();
                }

                //return RedirectToAction("AddProduct");
                return View();
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());
                return View();
            }
        }

        // GET: AddProduct/Edit/5
        public ActionResult Edit(int id)
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
            return View(addProduct_obj);
        }

        // POST: AddProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AddProduct AddProduct_obj)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(constr))
                {
                    string query = "sp_edit_product " +id+ "," +AddProduct_obj.product_id+ ", '" +AddProduct_obj.product_name+ "',"+ AddProduct_obj.price;
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddProduct/Delete/5
        public ActionResult Delete(int id)
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
            return View(addProduct_obj);
        }

        // POST: AddProduct/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AddProduct AddProduct_obj)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_delete_product " + id, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

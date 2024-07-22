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
    public class tbl_signin_signupController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        // GET: tbl_signin_signup
        public ActionResult Index()
        {
            return View();
        }

        // GET: tbl_signin_signup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: tbl_signin_signup/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: tbl_signin_signup/Create
        [HttpPost]
        public ActionResult SignUp(tbl_signin_signup signup_Obj)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string query = "sp_signUp '"+ signup_Obj.full_name + "','" + signup_Obj.mobile_number + "','" + signup_Obj.Gender + "','" + signup_Obj.email + "','" + signup_Obj.user_name + "','" + signup_Obj.password + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: tbl_signin_signup/Create
        public ActionResult SignIn()
        {
            return View();
        }

        // POST: tbl_signin_signup/Create
        [HttpPost]
        public ActionResult SignIn(tbl_signin_signup signin_Obj)
        {
            try
            {
                string username = "";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string query = "sp_sigIn '" + signin_Obj.user_name + "','" + signin_Obj.password + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while(sdr.Read())
                    {
                        username = sdr["user_name"].ToString();
                        Session["uname"] = username;
                    }
                    if(username != null)
                    {
                        return RedirectToAction("UserProductView", "AddProduct");
                    }
                    else
                    {
                        ViewBag.message = "Loginfail";
                        return View();
                    }
                    con.Close();
                }

                //return RedirectToAction("Index");
               
            }
            catch
            {
                return View();
            }
        }

        // GET: tbl_signin_signup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: tbl_signin_signup/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tbl_signin_signup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: tbl_signin_signup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

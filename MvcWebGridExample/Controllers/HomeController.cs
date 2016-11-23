using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebGridExample.Models;

namespace MvcWebGridExample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            Dictionary<string, string> lstCategories = new Dictionary<string, string>();
            lstCategories.Add("1", "Beverages");
            lstCategories.Add("2", "Condiments");
            lstCategories.Add("3", "Confections");
            lstCategories.Add("4", "Dairy Products");
            lstCategories.Add("5", "Grains/Cereals");
            lstCategories.Add("6", "Meat/Poultry");
            lstCategories.Add("7", "Produce");
            lstCategories.Add("8", "Seafood");
            return Json(lstCategories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoriesWithModel()
        {
            List<Category> lstCateogories = new List<Category>();
            lstCateogories.Add(new Category() { CategoryID = "1", CategoryName = "Beverages" });
            lstCateogories.Add(new Category() { CategoryID = "2", CategoryName = "Condiments" });
            lstCateogories.Add(new Category() { CategoryID = "3", CategoryName = "Confections" });
            lstCateogories.Add(new Category() { CategoryID = "4", CategoryName = "Dairy Products" });
            lstCateogories.Add(new Category() { CategoryID = "5", CategoryName = "Grains/Cereals" });
            lstCateogories.Add(new Category() { CategoryID = "6", CategoryName = "Meat/Poultry" });
            lstCateogories.Add(new Category() { CategoryID = "7", CategoryName = "Produce" });
            lstCateogories.Add(new Category() { CategoryID = "8", CategoryName = "Seafood" });

            return Json(lstCateogories, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ShowProducts(string categoryID)
        {
            IEnumerable<Product> products = GetProducts(categoryID);

            return PartialView("_showProducts", products);
        }

        private IEnumerable<Product> GetProducts(string categoryID)
        {
            List<Product> lstProducts = new List<Product>();            

            string query = string.Format("SELECT  [ProductID], [ProductName], [QuantityPerUnit],[UnitPrice],[UnitsInStock],[ReorderLevel] " +
                    " FROM [Northwind].[dbo].[Products] WHERE CategoryID = {0}",categoryID);

            using (SqlConnection con = new SqlConnection("Data Source=AADITYA-PC;Initial Catalog=Northwind;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open(); 
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lstProducts.Add(
                            new Product 
                            { 
                                ProductID = reader.GetValue(0).ToString(),
                                ProductName = reader.GetString(1),
                                QuantityPerUnit = reader.GetValue(2).ToString(),
                                UnitPrice = reader.GetValue(3).ToString().ToString(),
                                UnitsInStock = reader.GetValue(4).ToString(), 
                                ReorderLevel = reader.GetValue(5).ToString()  
                            }
                        );    
                    }
                }
            }

            return lstProducts.AsEnumerable();
        }
    }
}

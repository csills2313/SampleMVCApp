using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Index()
        {
            return "Navigate to a URL to show an example";
        }
        public ViewResult AutoProperty()
        {
            // create a new Product object
            Product myProduct = new Product();
            // set the property value
            myProduct.Name = "Kayak";
            // get the property
            string productName = myProduct.Name;
            // generate the view
            return View("Result",
            (object)String.Format("Product name: {0}", productName));
        }
        public ViewResult CreateProduct()
        {
            // create a new Product object
            Product myProduct = new Product();
            // set the property values
            myProduct.ProductID = 100;
            myProduct.Name = "Kayak";
            myProduct.Description = "A boat for one person";
            myProduct.Price = 275M;
            myProduct.Category = "Watersports";
            return View("Result",
            (object)String.Format("Category: {0}", myProduct.Category));
        }
        public ViewResult SumProducts()
        {
            Product[] products = {
new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
};
            var results = products.Sum(e => e.Price);
            products[2] = new Product { Name = "Stadium", Price = 79500M };
            return View("Result",
            (object)String.Format("Sum: {0:c}", results));
        }
        public ViewResult FindProducts()
        {
            Product[] products = {
new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
};
            // define the array to hold the results
            Product[] foundProducts = new Product[3];
            // sort the contents of the array
            Array.Sort(products, (item1, item2) =>
            {
                return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            });
            // get the first three items in the array as the results
            Array.Copy(products, foundProducts, 3);
            // create the result
            StringBuilder result = new StringBuilder();
            foreach (Product p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);                 
                result.AppendFormat("Name: {0} ", p.Name);
                result.AppendFormat("Category: {0} ", p.Name);                
            }
            return View("Result", (object)result.ToString());
        }
    }
}
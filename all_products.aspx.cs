using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class all_products : System.Web.UI.Page
    {
        protected void displayUserInformation()
        {
            //Display user information
            if (Session["login"].ToString() == "1")
            {
                string username = Session["username"].ToString();

                login_status_desktop.InnerHtml = "<li>Hi " + username + "</li>" +
                                                 "<span>|</span>" +
                                                 "<li><a href='signOut.aspx'>Sign Out</a></li>";

                login_status_mobile.InnerHtml = "<li>Hi " + username + "</li>" +
                                                "<li class='signOut-mobile'><a href='signOut.aspx'><img src='./Images/Icons/LogOut.svg' alt=''></a></li>";
            }
        }
        protected void displayCartNumber()
        {
            //Display cart number
            if (Request.Cookies["cart"] != null)
            {
                string[] cartProductsID = Request.Cookies["cart"].Value.Split(',');
                // -1 empty string after last ,
                Cart_Total_Products.InnerText = (cartProductsID.Length - 1).ToString();
                Cart_Total_Products_Mobile.InnerText = (cartProductsID.Length - 1).ToString();
            }
            else
            {
                Cart_Total_Products.InnerText = "0";
                Cart_Total_Products_Mobile.InnerText = "0";
            }   
        }

        protected void getProductsListByTypeAndFilter(string type, int begin, int end, List<Product> productsListsByTypeAndFilter, List<Product> productsLists)
        {
            foreach (Product product in productsLists)
            {
                if (type == product.type && Int32.Parse(product.price) >= begin && Int32.Parse(product.price) <= end)
                {
                    productsListsByTypeAndFilter.Add(product);
                }
            }
            ListViewAllProducts.DataSource = productsListsByTypeAndFilter;
            ListViewAllProducts.DataBind();
        }

        protected void getProductsListBySearchAndFilter(string search, int begin, int end, List<Product> productsListsBySearchAndFilter, List<Product> productsLists)
        {
            foreach (Product product in productsLists)
            {
                if (product.name.ToLower().IndexOf(search) != - 1 && Int32.Parse(product.price) >= begin && Int32.Parse(product.price) <= end)
                {
                    productsListsBySearchAndFilter.Add(product);
                }
            }
            ListViewAllProducts.DataSource = productsListsBySearchAndFilter;
            ListViewAllProducts.DataBind();
        }

        protected string totalProducts(List<Product> productsListsByTypeAndFilter)
        {
            int total = 0;
            foreach (Product product in productsListsByTypeAndFilter)
                total++;
            return total.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString.Get("type");
            displayUserInformation();
            displayCartNumber();
            List<Product> productsLists = (List<Product>)Application["productsList"];

            type = type.ToLower();

            if (type == "nike" || type == "adidas" || type == "puma")
                    {
                        //Change page title
                        Page.Title = type.ToUpper();
                        //Create Products List
                        List<Product> productsListByTypeAndFilter = new List<Product>();
                        getProductsListByTypeAndFilter(type, 0, 999999999, productsListByTypeAndFilter, productsLists);
                        all_products_brand_name.InnerText = $"{type} ({totalProducts(productsListByTypeAndFilter)})";
                    }                  
                }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
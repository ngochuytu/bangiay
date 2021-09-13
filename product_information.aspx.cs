using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class product_information : System.Web.UI.Page
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

        
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString.Get("id");
  
            if (id != null)
            {
                displayUserInformation();
                displayCartNumber();
                

                //Dislay page content                
                string pageTitle = "";

                    //Display product information
                List<Product> productsList = (List<Product>)Application["productsList"];
                List<Product> productInformation = new List<Product>();
                //List<Product> relateProducts = new List<Product>(); Thi
                foreach (Product product in productsList)
                {
                    

                    if (id == product.id)
                    {
                        productInformation.Add(product);
                        pageTitle = product.name;
                    }

                    /*Thi
                    string[] idTruocCham = id.Split('.');
                    string[] idTruocChamTrongList = product.id.Split('.');
                    if (idTruocCham[0] == idTruocChamTrongList[0])
                    {
                        relateProducts.Add(product);
                    }
                    */
                }

                ListViewProductInformation1.DataSource = productInformation;
                ListViewProductInformation1.DataBind();
                ListViewProductInformation2.DataSource = productInformation;
                ListViewProductInformation2.DataBind();

                /*Thi
                ListViewRelateProducts.DataSource = relateProducts;
                ListViewRelateProducts.DataBind();
                */

                
                //Display product colors
                List<Product> productColors = new List<Product>();
                //Get productID without color part, ex: "1.1" -> "1", "12.1" -> "12"
                string currentProductIDBeforeDot = id.Substring(0, id.Length - 2);
                foreach(Product product in productsList)
                {
                    //Compare with productID without color from list
                    string productIDBeforeDot = product.id.Substring(0, product.id.Length - 2);
                    if (currentProductIDBeforeDot == productIDBeforeDot)
                    {
                        productColors.Add(product);
                    }
                }

                ListViewProductColors.DataSource = productColors;
                ListViewProductColors.DataBind();
                
                //Change Page Title
                Page.Title = pageTitle;
            }
            else
                Response.Redirect("index.aspx");   
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            
        }

        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString.Get("id");

            //Store cart to cookies
            if(Request.Cookies["cart"] == null)
            {
                Response.Cookies["cart"].Value = $"{id},";
                Response.Cookies["cart"].Expires = DateTime.Now.AddDays(14);
            }
            else
            {
                //Store cookies by productID, example: 1,2,3,40,50,... 
                Response.Cookies["cart"].Value = Request.Cookies["cart"].Value + $"{id},";
                Response.Cookies["cart"].Expires = DateTime.Now.AddDays(14);
            }

            //Refresh to update cart number
            Response.Redirect(Request.Url.ToString());
        }
    }
}
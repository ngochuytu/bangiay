using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class signIn : System.Web.UI.Page
    {
        protected void redirectIfLoggedIn()
        {
            if (Session["login"].ToString() == "1")
                Response.Redirect("index.aspx");
        }
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

        /*Thi
        protected void LoginFail5Times()
        {
            if (Session["wrong"] == null)
            {
                Session["wrong"] = 1;
                sign_in_status.InnerHtml = "Đăng nhập sai lần thứ: " + Session["wrong"].ToString();
            }
            else
            {
                int wrong = Int32.Parse(Session["wrong"].ToString());
                wrong++;
                Session["wrong"] = wrong;
                sign_in_status.InnerHtml = "Đăng nhập sai lần thứ: " + Session["wrong"].ToString();
            }

            if (Session["wrong"].ToString() == "5")
            {
                Response.Redirect("index.aspx");
            }
        }
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            redirectIfLoggedIn();
            displayUserInformation();
            displayCartNumber();
            /*Thi
            if(Session["wrong"] != null)
            {
                if (Session["wrong"].ToString() == "5")
                {
                    Response.Redirect("index.aspx");
                }
            }*/
            

            if (IsPostBack)
            {
                string username = Request.Form.Get("input-username");
                string password = Request.Form.Get("input-password");
                List<User> arr = (List<User>)Application["users"];

                if (arr.Count == 0)
                {
                    sign_in_status.InnerHtml = "Tài khoản không tồn tại";


                    //LoginFail5Times(); Thi


                }
                else
                {
                    foreach (User user in arr)
                    {
                        if (user.username == username && user.password == password)
                        {
                            Session["login"] = 1;
                            Session["username"] = username;
                            //Thi
                            sign_in_status.InnerHtml = "Đăng nhập thành công" + DateTime.Now;
                            break;
                        }
                        else
                        {
                            //LoginFail5Times(); Thi



                            if (user.username != username && user.password == password)
                                username_message.InnerHtml = "Sai tài khoản";
                            if (user.username == username && user.password != password)
                                password_message.InnerHtml = "Sai mật khẩu";
                            if (user.username != username && user.password != password)
                            {
                                username_message.InnerHtml = "Sai tài khoản";
                                password_message.InnerHtml = "Sai mật khẩu";
                            }
                        }
                    }

                    if ((int)Session["login"] == 1)
                    {
                        //Response.Redirect("index.aspx");
                    }
                }
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
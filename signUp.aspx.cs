using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class signUp : System.Web.UI.Page
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
            displayUserInformation();
            displayCartNumber();

            if (IsPostBack)
            {
                string fullname = Request.Form.Get("input-fullname");
                string username = Request.Form.Get("input-username");
                string password = Request.Form.Get("input-password");
                string repassword = Request.Form.Get("input-repassword");
                List<User> arr = (List<User>)Application["users"];
                bool check = false;
                int pass = 0;


                if (!check)
                {
                    if (pass != 4)
                    {
                        //Fullname check
                        if (fullname == "")
                            fullname_message.InnerHtml = "Hãy nhập họ và tên";
                        else
                        {

                            fullname_message.InnerHtml = "";
                            pass++;
                        }

                        //Username check
                        if (username == "")
                            username_message.InnerHtml = "Hãy nhập Username";
                        else
                        {
                            bool newUsername = true;
                            foreach (User user in arr)
                            {
                                if (user.username == username)
                                {
                                    sign_up_status.InnerHtml = "";
                                    username_message.InnerHtml = "Tên tài khoản này đã có người đặt";
                                    newUsername = false;
                                    break;
                                }
                            }

                            if (newUsername)
                            {
                                username_message.InnerHtml = "";
                                pass++;
                            }
                        }

                        

                        //Password check
                        if (password == "")
                            password_message.InnerHtml = "Hãy nhập mật khẩu";
                        else if (password.Length <= 5)
                            password_message.InnerHtml = "Mật khẩu phải lớn hơn 5 ký tự";
                        else
                        {
                            password_message.InnerHtml = "";
                            pass++;
                        }

                        //Repassword check
                        if (repassword == "")
                            repassword_message.InnerHtml = "Hãy nhập lại mật khẩu";
                        else if (repassword != password)
                            repassword_message.InnerHtml = "Mật khẩu xác nhận không chính xác";
                        else
                        {
                            repassword_message.InnerHtml = "";
                            pass++;
                        }
                    }

                    if (pass == 4)
                    {
                        check = true;
                    }
                }

                if (check)
                {
                    sign_up_status.InnerHtml = "Đăng kí thành công";
                    User user = new User(fullname, username, password, repassword);
                    arr.Add(user);
                    Application["users"] = arr;
                    Session["username"] = username;
                }
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
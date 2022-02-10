using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MensaBestellung
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        string connStrg = WebConfigurationManager.ConnectionStrings["AppDbInt"].ConnectionString;
        //string connStrg = WebConfigurationManager.ConnectionStrings["AppDbExt"].ConnectionString;

        DataBase db;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DataBase(connStrg);
                db.Open();
                db.Close();
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return;
            }
        }

        protected void btn_signInUser_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lbl_Info.Text = "";
                bool isUserRegistrated = CheckUser();
                int permission = GetPermission();
                string username = GetName();
                int userID = GetUserId();
                if ((isUserRegistrated == true && permission == 1) || (isUserRegistrated == true && permission == 2))
                {
                    Session["Email"] = txt_userEmail.Text.ToString();
                    Session["Lastname"] = txt_lastName.Text.ToString();
                    Session["Permission"] = permission;
                    Session["UserID"] = userID;

                    FormsAuthentication.RedirectFromLoginPage(username, false);
                    Response.Redirect("UserPage.aspx");
                }
                else
                {
                    lbl_Info.Text = "Sie sind nicht in der Mensa angemeldet. Bitte wenden Sie sich an das Sekretariat.";
                }
            }
            else
            {
                lbl_Info.Text = "Bitte überprüfen Sie Ihre Eingaben.";
            }
            
        }
        private string GetName()
        {
            string username = "";
            try
            {
                username = (string)db.RunQueryScalar($"SELECT Concat(user.firstName,' ',user.lastName ) " +
                    $"AS UserName FROM user WHERE email='{txt_userEmail.Text}'");
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
            }
            return username;
        }

        private int GetPermission()
        {
            int permissionId = 0;
            try
            {
                permissionId = Convert.ToInt32(db.RunQueryScalar($"SELECT permission_id FROM user " +
                    $"WHERE lastName = '{txt_lastName.Text}' " +
                    $"AND email = '{txt_userEmail.Text}'"));
            }
            catch (Exception)
            {
                throw new Exception("Bitte überprüfen Sie Ihre Eingaben.");
            }
            return permissionId;
        }

        private int GetUserId()
        {
            try
            {
                int userId = Convert.ToInt32(db.RunQueryScalar($"SELECT user_id FROM user WHERE email = '{txt_userEmail.Text}';"));
                return userId;
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
            }
            return -1; // überarbeiten
        }

        private bool CheckUser()
        {
            bool isUserRegistrated = false;
            try
            {
                int countUser = Convert.ToInt32(db.RunQueryScalar($"SELECT COUNT(user_id) FROM user WHERE " +
                    $"lastName = '{txt_lastName.Text}' AND email = '{txt_userEmail.Text}'"));

                if (countUser == 1)
                {
                    isUserRegistrated = true;
                }
            }
            catch (Exception)
            {
                lbl_Info.Text = "Bitte überprüfen Sie Ihre Eingaben";
            }
            return isUserRegistrated;
        }
    }
}
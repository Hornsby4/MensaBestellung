using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MensaBestellung
{
    public partial class UserPage : System.Web.UI.Page
    {
        string connStrg = WebConfigurationManager.ConnectionStrings["AppDbInt"].ConnectionString;
        //string connStrg = WebConfigurationManager.ConnectionStrings["AppDbExt"].ConnectionString;

        DataBase db;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    btn_goToAdminPage.Visible = false;
                }
                string username = DesignName(User.Identity.Name);
                lbl_name.Text = username;

                db = new DataBase(connStrg);
                db.Open();
                db.Close();

                AllowAdminPage(username);

                DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                //DataTable dt = db.RunQuery("SELECT * FROM menu WHERE dateOfDay BETWEEN '2021-12-13' AND '2021-12-17'");

                //currentWeekMonday.ToLongDateString()
                //currentWeekMonday.ToShortDateString()

                for (int i = 0; i < 5; i++)
                {
                }

                lbl_sideDishMonday.Text = "Suppe";
                lbl_menu1Monday.Text = "Schnitzel";
                lbl_menu2Monday.Text = "Topfenknödel";
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return;
            }

        }


        private string DesignName(string name)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in name.Split(' '))
            {
                if (s.Length > 0)
                {
                    sb.Append(s.Substring(0, 1).ToUpper() + s.Substring(1) + " ");
                }
            }
            return sb.ToString();
        }

        private void AllowAdminPage(string username)
        {
            int permission = Convert.ToInt32(Session["Permission"]);
            if(permission == 2)
            {
                btn_goToAdminPage.Visible = true;
            }
        }

        protected void btn_buyMoreFood_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserPageFoodExchange.aspx");
        }

        protected void btn_goToAdminPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

        protected void btn_saveOrder_Click(object sender, EventArgs e)
        {
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("SignIn.aspx");
        }

    }
}
using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
                db = new DataBase(connStrg);
                db.Open();
                db.Close();

                DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                //DataTable dt = db.RunQuery("SELECT * FROM menu WHERE dateOfDay BETWEEN '2021-12-13' AND '2021-12-17'");

                //currentWeekMonday.ToLongDateString()
                //currentWeekMonday.ToShortDateString()

                for (int i = 0; i < 5; i++)
                {
                }

                lbl_sideDishMonday.Text = "haojdo";
                lbl_menu1Monday.Text = "Schnitzel";
                lbl_menu2Monday.Text = "aasdas";
            }
            catch (Exception ex)
            {
                //lblInfo.Text = ex.Message; //TODO info lable
                return;
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
    }
}
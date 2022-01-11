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
    public partial class UserPageFoodExchange : System.Web.UI.Page
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
                DataTable dt = db.RunQuery($"SELECT * FROM menu WHERE dateOfDay BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");

                gv_foodExchange.DataSource = dt;
                gv_foodExchange.DataBind();

            }
            catch (Exception ex)
            {
                //lblInfo.Text = ex.Message; //TODO info lable
                return;
            }
        }

        protected void btn_goBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }

        
    }
}
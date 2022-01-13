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

                lbl_dateMonday.Text = "2021-12-13";
                lbl_dateTuesday.Text = "2021-12-16";
                lbl_dateWendesday.Text = "2021-12-17";


                db = new DataBase(connStrg);
                db.Open();
                db.Close();

                AllowAdminPage(username);
                DataTable dt;
                if (GetExistingOrders(out dt))
                {
                    AllowFoodExchange(dt);
                }

                DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                SelectDates(currentWeekMonday);
                
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

        private void SelectDates(DateTime currentWeekMonday)
        {
            DataTable dt;
            List<string> dates = new List<string>();
            dt = db.RunQuery($"SELECT menuDate FROM menu WHERE menuDate BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");
            foreach(DataRow dr in dt.Rows)
            {
                dates.Add(((DateTime)dr[0]).ToString("yyyy-MM-dd"));
            }
            lbl_dateMonday.Text = dates[0];
            lbl_dateTuesday.Text = dates[1];
            lbl_dateWendesday.Text = dates[2];
            lbl_dateThursday.Text = dates[3];
            lbl_dateFriday.Text = dates[4];
        }

        private void AllowFoodExchange(DataTable dt)
        {
            int foodExchange = 0;

            foreach (DataRow dr in dt.Rows)
            {
                string date = Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd");
                foodExchange = Convert.ToInt32(dr[2]);
                FindAndWorkWithDay(date, foodExchange);
            }
        }

        private bool GetExistingOrders(out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                int userId = GetUserId();
                dt = db.RunQuery($"SELECT * FROM user_orders_menu WHERE user_id={userId}");
                return true;
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return false;
            }
        }


        private void FindAndWorkWithDay(string date, int foodExchange)
        {
            if(lbl_dateMonday.Text == date)
            {
                SetCheckBoxes(chkBox_foodMonday, chkBox_foodExchangeMonday, foodExchange);
            }
            if (lbl_dateTuesday.Text == date)
            {
                SetCheckBoxes(chkBox_foodTuesday, chkBox_foodExchangeTuesday, foodExchange);
            }
            if (lbl_dateWendesday.Text == date)
            {
                SetCheckBoxes(chkBox_foodWendesday, chkBox_foodExchangeWendesday, foodExchange);
            }
            if (lbl_dateThursday.Text == date)
            {
                SetCheckBoxes(chkBox_foodThursday, chkBox_foodExchangeThursday, foodExchange);
            }
            if (lbl_dateFriday.Text == date)
            {
                SetCheckBoxes(chkBox_foodFriday, chkBox_foodExchangeFriday, foodExchange);
            }
        }

        private void SetCheckBoxes(CheckBox chkBox_food, CheckBox chkBox_foodExchange, int foodExchange)
        {
            chkBox_food.Checked = true;
            chkBox_food.Enabled = false;
            if (foodExchange == 1)
            {
                chkBox_foodExchange.Checked = true;
            }
            if(foodExchange == 0)
            {
                chkBox_foodExchange.Enabled = true;
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
            SaveOrderIntoDb(lbl_dateMonday.Text, chkBox_foodExchangeMonday, chkBox_foodMonday);
            SaveOrderIntoDb(lbl_dateTuesday.Text, chkBox_foodExchangeTuesday, chkBox_foodTuesday);
            SaveOrderIntoDb(lbl_dateWendesday.Text, chkBox_foodExchangeWendesday, chkBox_foodWendesday);
            SaveOrderIntoDb(lbl_dateThursday.Text, chkBox_foodExchangeThursday, chkBox_foodThursday);
            SaveOrderIntoDb(lbl_dateFriday.Text, chkBox_foodExchangeFriday, chkBox_foodFriday);
        }

        private void SaveOrderIntoDb(string date, CheckBox chkBox_foodExchange, CheckBox chkBox_menuOfTheDay)
        {
            try
            {
                if(chkBox_menuOfTheDay.Checked == true)
                {
                    bool isFoodExchangeEnabled = false;
                    if(chkBox_foodExchange.Checked == true)
                    {
                        isFoodExchangeEnabled = true;
                    }
                    int userId = GetUserId();
                    bool updateOrInsert = UpdateOrInsert(date, chkBox_foodExchange, chkBox_menuOfTheDay);

                    if(updateOrInsert == false && isFoodExchangeEnabled == true)
                    {
                        db.RunNonQuery($"UPDATE user_orders_menu SET foodExchange = 1 WHERE menuDate = '{date}' AND user_Id = {userId};");
                        chkBox_foodExchange.Enabled = false;
                    }
                    else if(chkBox_menuOfTheDay.Enabled == true)
                    {
                        db.RunNonQuery($"INSERT INTO user_orders_menu(`menuDate`, `user_id`, `foodExchange`) VALUES('{date}', {userId}, {isFoodExchangeEnabled});");
                        chkBox_foodExchange.Enabled = true;
                        lbl_Info.Text = "Die Bestellungen wurden erfolgreich gespeichert.";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
            }
        }

        private bool UpdateOrInsert(string date, CheckBox chkBox_foodExchange, CheckBox chkBox_menuOfTheDay)
        {
            DataTable dt;
            if (GetExistingOrders(out dt))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string dateDb = Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd");
                    int foodExchangeDb = Convert.ToInt32(dr[2]);
                    if (dateDb == date && foodExchangeDb == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int GetUserId()
        {
            try
            {
                string email = Session["Email"].ToString();
                int userId = Convert.ToInt32(db.RunQueryScalar($"SELECT user_id FROM user WHERE email = '{email}';"));
                return userId;
            }
            catch(Exception ex)
            {
                lbl_Info.Text = ex.Message;
            }
            return -1; // überarbeiten
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("SignIn.aspx");
        }

    }
}
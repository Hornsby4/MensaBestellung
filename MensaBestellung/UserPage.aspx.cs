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

                db = new DataBase(connStrg);
                db.Open();
                db.Close();

                if (!Page.IsPostBack)
                {
                    btn_goToAdminPage.Visible = false;
                    string username = DesignName(User.Identity.Name);
                    lbl_name.Text = username;

                    AllowAdminPage(username);

                    DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    SelectDates(currentWeekMonday);
                    SelectMenus(currentWeekMonday);
                    EnableCheckBoxes();

                    DataTable dt;
                    if (GetExistingOrders(out dt))
                    {
                        AllowFoodExchange(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return;
            }
        }

        private void SelectMenus(DateTime currentWeekMonday)
        {
            List<string> sidedish = new List<string>();
            List<string> maindish1 = new List<string>();
            List<string> maindish2 = new List<string>();
            DataTable dt = db.RunQuery($"SELECT sidedish.description, m1.description, m2.description " +
                $"FROM menu LEFT JOIN sidedish ON menu.sideDish = sidedish.dish_id " +
                $"LEFT JOIN maindish m1 ON menu.mainDish1 = m1.dish_id " +
                $"LEFT JOIN maindish m2 ON menu.mainDish2 = m2.dish_id " +
                $"WHERE menuDate >= '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND menuDate <= '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'; ");
            
            foreach(DataRow dr in dt.Rows)
            {
                sidedish.Add(dr[0].ToString());
                maindish1.Add(dr[1].ToString());
                maindish2.Add(dr[2].ToString());
            }

            lbl_sideDishMonday.Text = sidedish[0];
            lbl_sideDishTuesday.Text = sidedish[1];
            lbl_sideDishWendesday.Text = sidedish[2];
            lbl_sideDishThursday.Text = sidedish[3];
            lbl_sideDishFriday.Text = sidedish[4];

            lbl_menu1Monday.Text = maindish1[0];
            lbl_menu1Tuesday.Text = maindish1[1];
            lbl_menu1Wendesday.Text = maindish1[2];
            lbl_menu1Thursday.Text = maindish1[3];
            lbl_menu1Friday.Text = maindish1[4];

            lbl_menu2Monday.Text = maindish2[0];
            lbl_menu2Tuesday.Text = maindish2[1];
            lbl_menu2Wendesday.Text = maindish2[2];
            lbl_menu2Thursday.Text = maindish2[3];
            lbl_menu2Friday.Text = maindish2[4];
        }

        private void SelectDates(DateTime currentWeekMonday)
        {
            DataTable dt;
            List<string> dates = new List<string>();
            dt = db.RunQuery($"SELECT menuDate FROM menu WHERE menuDate " +
                $"BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' " +
                $"AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");
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
        private void EnableCheckBoxes()
        {
            chkBox_foodMonday.Enabled = false;
            chkBox_foodTuesday.Enabled = false;
            chkBox_foodWendesday.Enabled = false;
            chkBox_foodThursday.Enabled = false;
            chkBox_foodFriday.Enabled = false;
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
                chkBox_foodExchange.Enabled = true;
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

                    if(updateOrInsert == true && isFoodExchangeEnabled == true || updateOrInsert == false && isFoodExchangeEnabled == false)
                    {
                        db.RunNonQuery($"UPDATE user_orders_menu SET foodExchange = {isFoodExchangeEnabled} WHERE menuDate = '{date}' AND user_Id = {userId};");
                    }
                    else
                    {
                        lbl_Info.Text = "Sie können nur die Essensbörse verändern.";
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
                        return true;
                    }
                }
            }
            return false;
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
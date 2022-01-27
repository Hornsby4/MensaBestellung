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
        DateTime currentWeekMonday;
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
                    currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    ViewState["currentWeek"] = currentWeekMonday;
                    btn_goToAdminPage.Visible = false;
                    string username = DesignName(User.Identity.Name);
                    lbl_name.Text = username;

                    AllowAdminPage(username);
                    lbl_Info.Text = "";

                    int dates = SelectDates(currentWeekMonday);
                    int menu = SelectMenus(currentWeekMonday);
                    if(menu == -1 || dates == -1)
                    {
                        lbl_Info.Text = "Es wurde noch kein Datum und keine Menüs festgelegt.";
                    }
                    else
                    {
                        DisableCheckBoxesFood();

                        DataTable dt;
                        if (GetExistingOrders(currentWeekMonday, out dt))
                        {
                            AllowFoodExchange(dt);
                        }

                        SetAllFoodExchangeLabels();
                    }

                }
                currentWeekMonday = (DateTime)ViewState["currentWeek"];
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return;
            }
        }

        private void SetAllFoodExchangeLabels()
        {
            SetFoodExchangeLabel(lbl_closeFoodExchangeMonday, lbl_dateMonday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeTuesday, lbl_dateTuesday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeWendesday, lbl_dateWendesday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeThursday, lbl_dateThursday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeFriday, lbl_dateFriday);
        }

        private void SetFoodExchangeLabel(Label lbl_closeFoodExchange, Label lbl_date)
        {
            if(lbl_date.Text == "")
            {
                lbl_closeFoodExchange.Text = "Essensbörse geschlossen.";
                return;
            }
            DateTime today = DateTime.Now.Date;
            DateTime date = Convert.ToDateTime(lbl_date.Text);
            int time = DateTime.Compare(date, today);
            if (time == -1)
            {
                lbl_closeFoodExchange.Text = "Essensbörse geschlossen.";
            }
            else
            {
                lbl_closeFoodExchange.Text = $"Bestellende Fr, {currentWeekMonday.AddDays(4).ToString("dd.MMM")} 10:30";
            }
        }

        private int SelectMenus(DateTime currentWeekMonday)
        {
            try
            {
                List<string> sidedish = new List<string>();
                List<string> maindish1 = new List<string>();
                List<string> maindish2 = new List<string>();
                DataTable dt = db.RunQuery($"SELECT sidedish.description, m1.description, m2.description " +
                    $"FROM menu LEFT JOIN sidedish ON menu.sideDish = sidedish.dish_id " +
                    $"LEFT JOIN maindish m1 ON menu.mainDish1 = m1.dish_id " +
                    $"LEFT JOIN maindish m2 ON menu.mainDish2 = m2.dish_id " +
                    $"WHERE menuDate >= '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND menuDate <= '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'; ");
                if(dt.Rows.Count < 5)
                {
                    lbl_Info.Text = "Es wurden noch keine Menüs festgelegt";

                    lbl_sideDishMonday.Text = "";
                    lbl_sideDishTuesday.Text = "";
                    lbl_sideDishWendesday.Text = "";
                    lbl_sideDishThursday.Text = "";
                    lbl_sideDishFriday.Text = "";

                    lbl_menu1Monday.Text = "";
                    lbl_menu1Tuesday.Text = "";
                    lbl_menu1Wendesday.Text = "";
                    lbl_menu1Thursday.Text = "";
                    lbl_menu1Friday.Text = "";

                    lbl_menu2Monday.Text = "";
                    lbl_menu2Tuesday.Text = "";
                    lbl_menu2Wendesday.Text = "";
                    lbl_menu2Thursday.Text = "";
                    lbl_menu2Friday.Text = "";

                    DisableCheckBoxesFood();
                    DisableCheckBoxFoodExchange();
                    return -1;
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
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
                    return 0;
                }
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return -1;
            }
        }

        private int SelectDates(DateTime currentWeekMonday)
        {
            try
            {
                DataTable dt;
                List<string> dates = new List<string>();
                dt = db.RunQuery($"SELECT menuDate FROM menu WHERE menuDate " +
                    $"BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' " +
                    $"AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");
                if(dt.Rows.Count < 5)
                {
                    lbl_Info.Text = "Es wurden noch kein Datum festgelegt";

                    lbl_dateMonday.Text = "";
                    lbl_dateTuesday.Text = "";
                    lbl_dateWendesday.Text = "";
                    lbl_dateThursday.Text = "";
                    lbl_dateFriday.Text = "";

                    DisableCheckBoxesFood();
                    DisableCheckBoxFoodExchange();
                    return -1;
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dates.Add(((DateTime)dr[0]).ToString("yyyy-MM-dd"));
                    }
                    lbl_dateMonday.Text = dates[0];
                    lbl_dateTuesday.Text = dates[1];
                    lbl_dateWendesday.Text = dates[2];
                    lbl_dateThursday.Text = dates[3];
                    lbl_dateFriday.Text = dates[4];
                    return 0;
                }
            }
            catch(Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return -1;
            }
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

        private bool GetExistingOrders(DateTime whisedWeek,out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                int userId = GetUserId();
                dt = db.RunQuery($"SELECT * FROM user_orders_menu WHERE user_id={userId} " +
                    $"AND menuDate BETWEEN '{whisedWeek.ToString("yyyy-MM-dd")}' " +
                    $"AND '{whisedWeek.AddDays(4).ToString("yyyy-MM-dd")}';");
                return true;
            }
            catch (Exception ex)
            {
                lbl_Info.Text = ex.Message;
                return false;
            }
        }
        private void DisableCheckBoxesFood()
        {
            chkBox_foodMonday.Enabled = false;
            chkBox_foodTuesday.Enabled = false;
            chkBox_foodWendesday.Enabled = false;
            chkBox_foodThursday.Enabled = false;
            chkBox_foodFriday.Enabled = false;
        }
        private void EnableCheckBoxesFood()
        {
            chkBox_foodMonday.Enabled = true;
            chkBox_foodTuesday.Enabled = true;
            chkBox_foodWendesday.Enabled = true;
            chkBox_foodThursday.Enabled = true;
            chkBox_foodFriday.Enabled = true;
        }

        private void FindAndWorkWithDay(string date, int foodExchange)
        {
            if(lbl_dateMonday.Text == date)
            {
                SetCheckBoxes(chkBox_foodMonday, chkBox_foodExchangeMonday, foodExchange, date);
            }
            if (lbl_dateTuesday.Text == date)
            {
                SetCheckBoxes(chkBox_foodTuesday, chkBox_foodExchangeTuesday, foodExchange, date);
            }
            if (lbl_dateWendesday.Text == date)
            {
                SetCheckBoxes(chkBox_foodWendesday, chkBox_foodExchangeWendesday, foodExchange, date);
            }
            if (lbl_dateThursday.Text == date)
            {
                SetCheckBoxes(chkBox_foodThursday, chkBox_foodExchangeThursday, foodExchange, date);
            }
            if (lbl_dateFriday.Text == date)
            {
                SetCheckBoxes(chkBox_foodFriday, chkBox_foodExchangeFriday, foodExchange, date);
            }

        }

        private void SetCheckBoxes(CheckBox chkBox_food, CheckBox chkBox_foodExchange, int foodExchange, string date)
        {
            chkBox_food.Checked = true;
            if (foodExchange == 1)
            {
                chkBox_foodExchange.Checked = true;
            }
            DateTime today = DateTime.Now.Date;
            int time = DateTime.Compare(Convert.ToDateTime(date), today);
            if (time == -1)
            {
                chkBox_foodExchange.Enabled = false;
            }
            else
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
                bool isFoodExchangeEnabled = false;
                if(chkBox_foodExchange.Checked == true)
                {
                    isFoodExchangeEnabled = true;
                }
                int userId = GetUserId();
                bool updateOrInsert = UpdateOrInsert(date, chkBox_foodExchange, chkBox_menuOfTheDay);

                if(updateOrInsert == true && isFoodExchangeEnabled == true || updateOrInsert == false && isFoodExchangeEnabled == false)
                {
                    db.RunNonQuery($"UPDATE user_orders_menu SET foodExchange = {isFoodExchangeEnabled} " +
                        $"WHERE menuDate = '{date}' AND user_Id = {userId};");
                    lbl_Info.Text = "Ihre Bestellungen wurden geupdated";
                }
                if (updateOrInsert == false && chkBox_menuOfTheDay.Enabled == true && chkBox_menuOfTheDay.Checked == true)
                {
                    db.RunNonQuery($"INSERT INTO user_orders_menu VALUES " +
                        $"('{Convert.ToDateTime(date).ToString("yyyy-MM-dd")}', '{userId}', {isFoodExchangeEnabled})");
                    lbl_Info.Text = "Ihr Bestellungen wurden gespeichert.";
                }
                if(updateOrInsert == true && chkBox_menuOfTheDay.Checked == false)
               {
                    DataTable dt = db.RunQuery($"SELECT * FROM user_orders_menu WHERE user_id = {userId} AND menuDate = '{date}'");
                    if(dt.Rows.Count > 0)
                    {
                        db.RunNonQuery($"DELETE FROM user_orders_menu WHERE user_id = {userId} AND menuDate = '{date}'");
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
            if (GetExistingOrders(currentWeekMonday,out dt))
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

        private void DeleteAllCheckBoxSettings()
        {
            chkBox_foodMonday.Checked = false;
            chkBox_foodTuesday.Checked = false;
            chkBox_foodWendesday.Checked = false;
            chkBox_foodThursday.Checked = false;
            chkBox_foodFriday.Checked = false;

            chkBox_foodExchangeMonday.Checked = false;
            chkBox_foodExchangeTuesday.Checked = false;
            chkBox_foodExchangeWendesday.Checked = false;
            chkBox_foodExchangeThursday.Checked = false;
            chkBox_foodExchangeFriday.Checked = false;
        }

        protected void btn_nextWeek_Click(object sender, EventArgs e)
        {
            currentWeekMonday = currentWeekMonday.AddDays(7);
            ViewState["currentWeek"] = currentWeekMonday;

            int dates = SelectDates(currentWeekMonday);
            int menu = SelectMenus(currentWeekMonday);
            DeleteAllCheckBoxSettings();
            SetAllFoodExchangeLabels();
            lbl_Info.Text = "";

            if (menu == -1 || dates == -1)
            {
                lbl_Info.Text = "Es wurde noch kein Datum und keine Menüs festgelegt";
            }
            else
            {
                DataTable dt;
                if (GetExistingOrders(currentWeekMonday, out dt))
                {
                    AllowFoodExchange(dt);
                }
                IsWeekBeforeOrAfter(dt);
            }
        }

        private void IsWeekBeforeOrAfter(DataTable dt)
        {
            DateTime currentWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            int isWeekBeforeOrAfter = DateTime.Compare(currentWeek, currentWeekMonday);
            if (isWeekBeforeOrAfter == -1)
            {
                // Woche ist in der Zukunft
                EnableCheckBoxesFood();
                DisableCheckBoxFoodExchange();
                AllowFoodExchange(dt);
            }
            else if (isWeekBeforeOrAfter == 1)
            {
                // Woche ist in der Vergangenheit
                DisableCheckBoxesFood();
                DisableCheckBoxFoodExchange();
            }
            else if (isWeekBeforeOrAfter == 0)
            {
                DisableCheckBoxesFood();
                DisableCheckBoxFoodExchange();
                AllowFoodExchange(dt);
            }
            else
            {
                throw new ArgumentException("Es ist noch kein Datum festgelegt.");
            }
        }

        protected void btn_lastWeek_Click(object sender, EventArgs e)
        {
            currentWeekMonday = currentWeekMonday.AddDays(-7);
            ViewState["currentWeek"] = currentWeekMonday;
            
            int dates = SelectDates(currentWeekMonday);
            int menu = SelectMenus(currentWeekMonday);
            DeleteAllCheckBoxSettings();
            SetAllFoodExchangeLabels();
            lbl_Info.Text = "";

            if (menu == -1 || dates == -1)
            {
                lbl_Info.Text = "Es wurde noch kein Datum und keine Menüs festgelegt";
            }
            else
            {
                DataTable dt;
                if (GetExistingOrders(currentWeekMonday, out dt))
                {
                    AllowFoodExchange(dt);
                }
                IsWeekBeforeOrAfter(dt);
            }
        }

        private void DisableCheckBoxFoodExchange()
        {
            chkBox_foodExchangeMonday.Enabled = false;
            chkBox_foodExchangeTuesday.Enabled = false;
            chkBox_foodExchangeWendesday.Enabled = false;
            chkBox_foodExchangeThursday.Enabled = false;
            chkBox_foodExchangeFriday.Enabled = false;
        }
    }
}
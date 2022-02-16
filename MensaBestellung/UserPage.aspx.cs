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

                    int dates = SelectDatesAndMenu(currentWeekMonday);
                    if(dates == -1)
                    {
                        lbl_Info.Text = "Es wurde noch kein Datum und keine Menüs festgelegt.";
                    }
                    else
                    {
                        DisableCheckBoxesFood();
                        DisableCheckBoxFoodExchange();

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
            SetFoodExchangeLabel(lbl_closeFoodExchangeMonday, currentWeekMonday, chkBox_foodExchangeMonday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeTuesday, currentWeekMonday.AddDays(1), chkBox_foodExchangeTuesday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeWendesday, currentWeekMonday.AddDays(2), chkBox_foodExchangeWendesday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeThursday, currentWeekMonday.AddDays(3), chkBox_foodExchangeThursday);
            SetFoodExchangeLabel(lbl_closeFoodExchangeFriday, currentWeekMonday.AddDays(4), chkBox_foodExchangeFriday);
        }

        private void SetFoodExchangeLabel(Label lbl_closeFoodExchange, DateTime date, CheckBox chkBox_foodExchange)
        {
            
            DateTime today = DateTime.Now;
            //DateTime today = Convert.ToDateTime("05.02.2022 09:00:00");
            int time = DateTime.Compare(Convert.ToDateTime($"{date.ToString("dd.MM.yyyy")} 13:30:00"), today);

            DateTime currentMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            int isWeekBeforeOrAfter = DateTime.Compare(currentMonday, currentWeekMonday);

            if(isWeekBeforeOrAfter == 0)
            {
                if (time == -1)
                {
                    lbl_closeFoodExchange.Text = "Essensbörse geschlossen.";
                    chkBox_foodExchange.Enabled = false;
                }
                else if (time == 0)
                {
                    lbl_closeFoodExchange.Text = "Börsenschluss Heute 13:30";
                }
                else
                {
                    string dateForLabel = GetDateAndDay(date, out string day);
                    lbl_closeFoodExchange.Text = $"Börsenschluss {day}, {dateForLabel} 13:30";
                }
            }
            else if(isWeekBeforeOrAfter == -1)
            {
                lbl_closeFoodExchange.Text = $"Bestellende Fr, {currentWeekMonday.AddDays(-3).ToString("dd.MMM")} 10:30";
            }
            else
            {
                lbl_closeFoodExchange.Text = "Essensbörse geschlossen.";
                chkBox_foodExchange.Enabled = false;
            }
        }

        private string GetDateAndDay(DateTime date, out string day)
        {
            if (date.ToString("yyyy-MM-dd") == currentWeekMonday.ToString("yyyy-MM-dd"))
            {
                day = "Mo";
                return Convert.ToDateTime(lbl_dateMonday.Text).ToString("dd.MM.yyyy");
            }
            if (date.ToString("yyyy-MM-dd") == currentWeekMonday.AddDays(1).ToString("yyyy-MM-dd"))
            {
                day = "Di";
                return Convert.ToDateTime(lbl_dateTuesday.Text).ToString("dd.MM.yyyy");
            }
            if (date.ToString("yyyy-MM-dd") == currentWeekMonday.AddDays(2).ToString("yyyy-MM-dd"))
            {
                day = "Mi";
                return Convert.ToDateTime(lbl_dateWendesday.Text).ToString("dd.MM.yyyy");
            }
            if (date.ToString("yyyy-MM-dd") == currentWeekMonday.AddDays(3).ToString("yyyy-MM-dd"))
            {
                day = "Do";
                return Convert.ToDateTime(lbl_dateThursday.Text).ToString("dd.MM.yyyy");
            }
            if (date.ToString("yyyy-MM-dd") == currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd"))
            {
                day = "Fr";
                return Convert.ToDateTime(lbl_dateFriday.Text).ToString("dd.MM.yyyy");
            }
            day = "";
            return "";
        }

        private void SetMenusZero()
        {
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
        }

        private int SelectDatesAndMenu(DateTime currentWeekMonday)
        {
            try
            {
                SetMenusZero();
                DisableCheckBoxesFood();

                lbl_dateMonday.Text = currentWeekMonday.ToString("yyyy-MM-dd");
                lbl_dateTuesday.Text = currentWeekMonday.AddDays(1).ToString("yyyy-MM-dd");
                lbl_dateWendesday.Text = currentWeekMonday.AddDays(2).ToString("yyyy-MM-dd");
                lbl_dateThursday.Text = currentWeekMonday.AddDays(3).ToString("yyyy-MM-dd");
                lbl_dateFriday.Text = currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd");

                DataTable dt;
                dt = db.RunQuery($"SELECT menuDate, mensaOpen, sidedish.description, m1.description, m2.description FROM menu " +
                    $"LEFT JOIN sidedish ON menu.sideDish = sidedish.dish_id " +
                    $"LEFT JOIN maindish m1 ON menu.mainDish1 = m1.dish_id " +
                    $"LEFT JOIN maindish m2 ON menu.mainDish2 = m2.dish_id " +
                    $"WHERE menuDate >= '{lbl_dateMonday.Text}' AND menuDate <= '{lbl_dateFriday.Text}'");

                foreach (DataRow dr in dt.Rows)
                {
                    if (lbl_dateMonday.Text == Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                    {
                        lbl_sideDishMonday.Text = dr[2].ToString();
                        lbl_menu1Monday.Text = dr[3].ToString();
                        lbl_menu2Monday.Text = dr[4].ToString();
                        if((bool)dr[1] == true)
                        {
                            chkBox_foodMonday.Enabled = true;
                        }
                    }
                    if (lbl_dateTuesday.Text == Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                    {
                        lbl_sideDishTuesday.Text = dr[2].ToString();
                        lbl_menu1Tuesday.Text = dr[3].ToString();
                        lbl_menu2Tuesday.Text = dr[4].ToString();
                        if ((bool)dr[1] == true)
                        {
                            chkBox_foodTuesday.Enabled = true;
                        }
                    }
                    if (lbl_dateWendesday.Text == Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                    {
                        lbl_sideDishWendesday.Text = dr[2].ToString();
                        lbl_menu1Wendesday.Text = dr[3].ToString();
                        lbl_menu2Wendesday.Text = dr[4].ToString();
                        if ((bool)dr[1] == true)
                        {
                            chkBox_foodWendesday.Enabled = true;
                        }
                    }
                    if (lbl_dateThursday.Text == Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                    {
                        lbl_sideDishThursday.Text = dr[2].ToString();
                        lbl_menu1Thursday.Text = dr[3].ToString();
                        lbl_menu2Thursday.Text = dr[4].ToString();
                        if ((bool)dr[1] == true)
                        {
                            chkBox_foodThursday.Enabled = true;
                        }
                    }
                    if (lbl_dateFriday.Text == Convert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                    {
                        lbl_sideDishFriday.Text = dr[2].ToString();
                        lbl_menu1Friday.Text = dr[3].ToString();
                        lbl_menu2Friday.Text = dr[4].ToString();
                        if ((bool)dr[1] == true)
                        {
                            chkBox_foodFriday.Enabled = true;
                        }
                    }
                }

                return 0;
            }
            catch (Exception ex)
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

            DateTime wantedDate = Convert.ToDateTime(date);
            DateTime today = DateTime.Now.Date;
            //DateTime today = Convert.ToDateTime("05.02.2022 09:00:00");
            int compareDates = DateTime.Compare(wantedDate, today);

            DateTime foodExchangeOpen = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(7);
            //int compareFriday = DateTime.Compare(wantedDate,Convert.ToDateTime($"{foodExchangeOpen.ToString("dd.MM.yyyy")} 10:30:00"));
            //int compareDatesFoodExchange = DateTime.Compare(today, Convert.ToDateTime($"{foodExchangeOpen.ToString("dd.MM.yyyy")} 13:30:00"));

            if (foodExchange == 1)
            {
                chkBox_foodExchange.Checked = true;
            }
            if (compareDates == -1)
            {
                chkBox_foodExchange.Enabled = false;
            }
            if(compareDates == 1)
            {
                chkBox_foodExchange.Enabled = true;
            }
            if(compareDates == 0)
            {
                int compareTime = DateTime.Compare(DateTime.Now, Convert.ToDateTime($"{today.ToString("dd.MM.yyyy")} 13:30:00"));
                if(compareTime == -1)
                {
                    chkBox_food.Enabled = false;
                    chkBox_foodExchange.Enabled = true;
                }
                else
                {
                    chkBox_food.Enabled = true;
                    chkBox_foodExchange.Enabled = false;
                }
            }
        }
        private bool GetExistingOrders(DateTime whisedWeek, out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                int userId = (int)Session["UserID"];
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
        private void DisableCheckBoxFoodExchange()
        {
            chkBox_foodExchangeMonday.Enabled = false;
            chkBox_foodExchangeTuesday.Enabled = false;
            chkBox_foodExchangeWendesday.Enabled = false;
            chkBox_foodExchangeThursday.Enabled = false;
            chkBox_foodExchangeFriday.Enabled = false;
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
            Response.Redirect("AdminPage_Overview.aspx");
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
                int userId = (int)Session["UserID"];
                bool updateOrInsert = UpdateOrInsert(date, chkBox_foodExchange, chkBox_menuOfTheDay);

                if(chkBox_menuOfTheDay.Checked == true)
                {
                    if(updateOrInsert == true && isFoodExchangeEnabled == true || updateOrInsert == false && isFoodExchangeEnabled == false)
                    {
                        db.RunNonQuery($"UPDATE user_orders_menu SET foodExchange = {isFoodExchangeEnabled} " +
                            $"WHERE menuDate = '{date}' AND user_Id = {userId};");
                        lbl_Info.Text = "Ihre Bestellungen wurden geupdated";
                    }
                }
                if (updateOrInsert == false && chkBox_menuOfTheDay.Enabled == true && chkBox_menuOfTheDay.Checked == true)
                {
                    db.RunNonQuery($"INSERT INTO user_orders_menu VALUES " +
                        $"('{Convert.ToDateTime(date).ToString("yyyy-MM-dd")}', '{userId}', {isFoodExchangeEnabled})");
                    lbl_Info.Text = "Ihre Bestellungen wurden gespeichert.";
                }
                if(updateOrInsert == true && chkBox_menuOfTheDay.Checked == false)
                {
                    DataTable dt = db.RunQuery($"SELECT * FROM user_orders_menu WHERE user_id = {userId} AND menuDate = '{date}'");
                    if(dt.Rows.Count > 0)
                    {
                        db.RunNonQuery($"DELETE FROM user_orders_menu WHERE user_id = {userId} AND menuDate = '{date}'");
                    }
                    lbl_Info.Text = "Ihre Bestellung wurde gelöscht";
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

            int dates = SelectDatesAndMenu(currentWeekMonday);
            DeleteAllCheckBoxSettings();
            SetAllFoodExchangeLabels();
            lbl_Info.Text = "";

            if (dates == -1)
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
                //DateTime today = Convert.ToDateTime("05.02.2022 10:31:00");
                DateTime today = DateTime.Now;
                DateTime orderEnding = currentWeekMonday.AddDays(-3);
                int compareDates = DateTime.Compare(today, Convert.ToDateTime($"{orderEnding.ToString("dd.MM.yyyy")} 13:30:00"));

                if (compareDates == 1)
                {
                    DisableCheckBoxFoodExchange();
                    DisableCheckBoxesFood();
                    AllowFoodExchange(dt);
                    SetAllFoodExchangeLabels();
                }
                else
                {
                    DisableCheckBoxFoodExchange();
                    SetAllFoodExchangeLabels();
                }
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
            
            int dates = SelectDatesAndMenu(currentWeekMonday);
            DeleteAllCheckBoxSettings();
            SetAllFoodExchangeLabels();
            lbl_Info.Text = "";

            if (dates == -1)
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

        
    }
}
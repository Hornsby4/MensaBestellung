using AjaxControlToolkit;
using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
namespace MensaBestellung
{
    public partial class AdminPage : System.Web.UI.Page
    {
        DataBase db;
        string connStrg = WebConfigurationManager.ConnectionStrings["AppDbInt"].ConnectionString;
        //string connStrg = WebConfigurationManager.ConnectionStrings["AppDbExt"].ConnectionString;
        DateTime selectedWeekMonday;
        protected void Page_Load(object sender, EventArgs e)
        {
            //selectedWeekMonday = default;
            try
            {
                if (!IsPostBack)
                {
                    FillAdminDDls();
                    if (selectedWeekMonday == default) selectedWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    ViewState["selectedWeek"] = selectedWeekMonday;
                    FillAdminOverviewTable(selectedWeekMonday);

                }
                selectedWeekMonday = (DateTime)ViewState["selectedWeek"];
            }
            catch (Exception aex)
            {
                lbl_infoLabel.Text = lbl_infoLabel.Text + "Error: " + aex.Message + '\n';
            }

            

            
        }

        private void FillAdminDDls()
        {
            db = new DataBase(connStrg);
            
            DataTable dt= db.RunQuery("SELECT description FROM maindish");                
               
            comboB_maindish1.DataSource = dt;
            comboB_maindish1.DataTextField="description";
            comboB_maindish1.DataBind();
            comboB_maindish1.Items.Insert(0, new ListItem("--Hauptgericht 1--", "0"));

            comboB_maindish2.DataSource = dt;
            comboB_maindish2.DataTextField = "description";
            comboB_maindish2.DataBind();
            comboB_maindish2.Items.Insert(0, new ListItem("--Hauptgericht 2--", "0"));

            dt = db.RunQuery("SELECT description FROM sidedish");
            comboB_sidedish.DataSource = dt;
            comboB_sidedish.DataTextField = "description";
            comboB_sidedish.DataBind();
            comboB_sidedish.Items.Insert(0, new ListItem("--Vorspeise--", "0"));



            comboB_sidedish.SelectedIndex = 0;
            comboB_maindish1.SelectedIndex = 0;
            comboB_maindish2.SelectedIndex = 0;



        }

        private void FillAdminOverviewTable(DateTime selectedMonday)
        {
            db = new DataBase(connStrg);
            DataTable dt = db.RunQuery($"SELECT * FROM menu WHERE menuDate BETWEEN '{selectedMonday.ToString("yyyy-MM-dd")}' AND '{selectedMonday.AddDays(4).ToString("yyyy-MM-dd")}'");
            FlushMenus();
            FlushCheckBoxes();
            SetDates(selectedMonday);
            SetMenuSums(selectedMonday);
            SetDishesForWeek(selectedMonday);
            SetExchangeEndDates();


        }

        private void FlushCheckBoxes()
        {
            List<CheckBox> checkBoxList=new List<CheckBox>();
            checkBoxList.Add(CheckBox_monday);
            checkBoxList.Add(CheckBox_tuesday);
            checkBoxList.Add(CheckBox_wednesday);
            checkBoxList.Add(CheckBox_thursday);
            checkBoxList.Add(CheckBox_friday);

            foreach(CheckBox checkBox in checkBoxList)
            {
                checkBox.Checked = false;
            }
        }

        private void SetDates(DateTime selectedMonday)
        {
            //Fills dates
            lbl_monday_date.Text = "Mo " + selectedMonday.ToString("dd.MM.yy");
            lbl_tuesday_date.Text = "Di " + selectedMonday.AddDays(1).ToString("dd.MM.yy");
            lbl_wednesday_date.Text = "Mi " + selectedMonday.AddDays(2).ToString("dd.MM.yy");
            lbl_thursday_date.Text = "Do " + selectedMonday.AddDays(3).ToString("dd.MM.yy");
            lbl_friday_date.Text = "Fr " + selectedMonday.AddDays(4).ToString("dd.MM.yy");
        }

        private void SetMenuSums(DateTime selectedMonday)
        {
            //Fills menu Sums
            lbl_monday_menuSum.Text = Convert.ToString(db.RunQueryScalar($"SELECT count(menuDate)" +
                $" FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.ToString("yyyy-MM-dd")}'"));

            lbl_tuesday_menuSum.Text = Convert.ToString(db.RunQueryScalar($"SELECT count(menuDate)" +
                $" FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(1).ToString("yyyy-MM-dd")}'"));

            lbl_wednesday_menuSum.Text = Convert.ToString(db.RunQueryScalar($"SELECT count(menuDate)" +
                $" FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(2).ToString("yyyy-MM-dd")}'"));

            lbl_thursday_menuSum.Text = Convert.ToString(db.RunQueryScalar($"SELECT count(menuDate)" +
                $" FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(3).ToString("yyyy-MM-dd")}'"));

            lbl_friday_menuSum.Text = Convert.ToString(db.RunQueryScalar($"SELECT count(menuDate)" +
                $" FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(4).ToString("yyyy-MM-dd")}'"));


            //Fills menu sums for foodexchange
            lbl_monday_menuSumX.Text = Convert.ToString(db.RunQueryScalar($"SELECT COUNT(menuDate) " +
                $"FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.ToString("yyyy-MM-dd")}' " +
                $"AND FoodExchange = 1"));

            lbl_tuesday_menuSumX.Text = Convert.ToString(db.RunQueryScalar($"SELECT COUNT(menuDate) " +
                $"FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(1).ToString("yyyy-MM-dd")}' " +
                $"AND FoodExchange = 1"));

            lbl_wednesday_menuSumX.Text = Convert.ToString(db.RunQueryScalar($"SELECT COUNT(menuDate) " +
                $"FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(2).ToString("yyyy-MM-dd")}' " +
                $"AND FoodExchange = 1"));

            lbl_thursday_menuSumX.Text = Convert.ToString(db.RunQueryScalar($"SELECT COUNT(menuDate) " +
                $"FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(3).ToString("yyyy-MM-dd")}' " +
                $"AND FoodExchange = 1"));

            lbl_friday_menuSumX.Text = Convert.ToString(db.RunQueryScalar($"SELECT COUNT(menuDate) " +
                $"FROM user_orders_menu " +
                $"WHERE menuDate = '{selectedMonday.AddDays(4).ToString("yyyy-MM-dd")}' " +
                $"AND FoodExchange = 1"));
        }

        private void SetDishesForWeek(DateTime selectedMonday)
        {
            DataTable menuDishes = db.RunQuery("SELECT main1.description AS maindish1, main2.description AS maindish2, side.description AS sidedish " +
                            "FROM menu " +
                            "right JOIN maindish main1 " +
                            "ON menu.mainDish1 = main1.dish_id " +
                            "right JOIN maindish main2 " +
                            "ON menu.mainDish2 = main2.dish_id " +
                            "right JOIN sidedish side " +
                            "ON menu.sidedish = side.dish_id " +
                            $"WHERE menuDate = '{selectedMonday.ToString("yyyy-MM-dd")}'");
            if (menuDishes.Rows.Count != 0)
            {
                lbl_dish1Monday.Text = menuDishes.Rows[0]["maindish1"].ToString();
                lbl_dish2Monday.Text = menuDishes.Rows[0]["maindish2"].ToString();
                lbl_sideDishMonday.Text = menuDishes.Rows[0]["sidedish"].ToString();
            }


            menuDishes = db.RunQuery("SELECT main1.description AS maindish1, main2.description AS maindish2, side.description AS sidedish " +
                "FROM menu " +
                "right JOIN maindish main1 " +
                "ON menu.mainDish1 = main1.dish_id " +
                "right JOIN maindish main2 " +
                "ON menu.mainDish2 = main2.dish_id " +
                "right JOIN sidedish side " +
                "ON menu.sidedish = side.dish_id " +
                $"WHERE menuDate = '{selectedMonday.AddDays(1).ToString("yyyy-MM-dd")}'");
            if (menuDishes.Rows.Count != 0)
            {
                lbl_dish1Tuesday.Text = menuDishes.Rows[0]["maindish1"].ToString();
                lbl_dish2Tuesday.Text = menuDishes.Rows[0]["maindish2"].ToString();
                lbl_sideDishTuesday.Text = menuDishes.Rows[0]["sidedish"].ToString();
            }

            menuDishes = db.RunQuery("SELECT main1.description AS maindish1, main2.description AS maindish2, side.description AS sidedish " +
                "FROM menu " +
                "right JOIN maindish main1 " +
                "ON menu.mainDish1 = main1.dish_id " +
                "right JOIN maindish main2 " +
                "ON menu.mainDish2 = main2.dish_id " +
                "right JOIN sidedish side " +
                "ON menu.sidedish = side.dish_id " +
                $"WHERE menuDate = '{selectedMonday.AddDays(2).ToString("yyyy-MM-dd")}'");
            if (menuDishes.Rows.Count != 0)
            {
                lbl_dish1Wednesday.Text = menuDishes.Rows[0]["maindish1"].ToString();
                lbl_dish2Wednesday.Text = menuDishes.Rows[0]["maindish2"].ToString();
                lbl_sideDishWednesday.Text = menuDishes.Rows[0]["sidedish"].ToString();
            }

            menuDishes = db.RunQuery("SELECT main1.description AS maindish1, main2.description AS maindish2, side.description AS sidedish " +
                "FROM menu " +
                "right JOIN maindish main1 " +
                "ON menu.mainDish1 = main1.dish_id " +
                "right JOIN maindish main2 " +
                "ON menu.mainDish2 = main2.dish_id " +
                "right JOIN sidedish side " +
                "ON menu.sidedish = side.dish_id " +
                $"WHERE menuDate = '{selectedMonday.AddDays(3).ToString("yyyy-MM-dd")}'");
            if (menuDishes.Rows.Count != 0)
            {
                lbl_dish1Thursday.Text = menuDishes.Rows[0]["maindish1"].ToString();
                lbl_dish2Thursday.Text = menuDishes.Rows[0]["maindish2"].ToString();
                lbl_sideDishThursday.Text = menuDishes.Rows[0]["sidedish"].ToString();
            }

            menuDishes = db.RunQuery("SELECT main1.description AS maindish1, main2.description AS maindish2, side.description AS sidedish " +
                "FROM menu " +
                "right JOIN maindish main1 " +
                "ON menu.mainDish1 = main1.dish_id " +
                "right JOIN maindish main2 " +
                "ON menu.mainDish2 = main2.dish_id " +
                "right JOIN sidedish side " +
                "ON menu.sidedish = side.dish_id " +
                $"WHERE menuDate = '{selectedMonday.AddDays(4).ToString("yyyy-MM-dd")}'");
            if (menuDishes.Rows.Count != 0)
            {
                lbl_dish1Friday.Text = menuDishes.Rows[0]["maindish1"].ToString();
                lbl_dish2Friday.Text = menuDishes.Rows[0]["maindish2"].ToString();
                lbl_sideDishFriday.Text = menuDishes.Rows[0]["sidedish"].ToString();
            }
        }

        private void SetExchangeEndDates()
        {
            int compareDates = DateTime.Compare(DateTime.Now, Convert.ToDateTime($"{lbl_monday_date.Text} 13:30:00").AddDays(-7));
            if (compareDates != 1) lbl_MonExchangeEndDate.Text = Convert.ToDateTime($"{lbl_monday_date.Text} 13:30:00").AddDays(-7).ToString("dd.MM.yyyy  HH:mm");
            else lbl_MonExchangeEndDate.Text = "Essensbörse geschlossen";

            compareDates = DateTime.Compare(DateTime.Now, Convert.ToDateTime($"{lbl_tuesday_date.Text} 13:30:00").AddDays(-7));
            if (compareDates != 1) lbl_TueExchangeEndDate.Text = Convert.ToDateTime($"{lbl_tuesday_date.Text} 13:30:00").AddDays(-7).ToString("dd.MM.yyyy  HH:mm");
            else lbl_TueExchangeEndDate.Text = "Essensbörse geschlossen";

            compareDates = DateTime.Compare(DateTime.Now, Convert.ToDateTime($"{lbl_wednesday_date.Text} 13:30:00").AddDays(-7));
            if (compareDates != 1) lbl_WedExchangeEndDate.Text = Convert.ToDateTime($"{lbl_wednesday_date.Text} 13:30:00").AddDays(-7).ToString("dd.MM.yyyy  HH:mm");
            else lbl_WedExchangeEndDate.Text = "Essensbörse geschlossen";

            compareDates = DateTime.Compare(DateTime.Now, Convert.ToDateTime($"{lbl_thursday_date.Text} 13:30:00").AddDays(-7));
            if (compareDates != 1) lbl_ThuExchangeEndDate.Text = Convert.ToDateTime($"{lbl_thursday_date.Text} 13:30:00").AddDays(-7).ToString("dd.MM.yyyy  HH:mm");
            else lbl_ThuExchangeEndDate.Text = "Essensbörse geschlossen";

            compareDates = DateTime.Compare(DateTime.Now, Convert.ToDateTime($"{lbl_friday_date.Text} 13:30:00").AddDays(-7));
            if (compareDates != 1) lbl_FriExchangeEndDate.Text = Convert.ToDateTime($"{lbl_friday_date.Text} 13:30:00").AddDays(-7).ToString("dd.MM.yyyy  HH:mm");
            else lbl_FriExchangeEndDate.Text = "Essensbörse geschlossen";
        }

        protected void btn_goToUserPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }

        protected void btn_throwAwayChanges_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage_Overview.aspx");
        }

        protected void btn_saveNewMenu_Click(object sender, EventArgs e)
        {
            db = new DataBase(connStrg);
            try
            {
                CheckIfDateIsOk(txt_datePicker.Text);
                AddNewDishes();
                SaveMenu();
                FillAdminOverviewTable(selectedWeekMonday);

            }
            catch (Exception ex)
            {
                lbl_infoLabel.Text=ex.Message;
            }



        }
        /// <summary>
        /// Gets the matching DishIds from the DB and Inserts the new menu.
        /// </summary>
        private void SaveMenu()
        {
            //get dish ids from DB
            string sidedishIdstring = "null";
            string maindish1IDstring = "null";
            string maindish2IDstring = "null";

            if (HasValue(comboB_sidedish))
            {
                sidedishIdstring = db.RunQueryScalar($"select dish_id from sidedish where description='{comboB_sidedish.Text}'").ToString();
                sidedishIdstring.Insert(0, "'");
                sidedishIdstring.Insert(sidedishIdstring.Length, "'");
            }
            if (HasValue(comboB_maindish1))
            {
                maindish1IDstring = db.RunQueryScalar($"select dish_id from maindish where description='{comboB_maindish1.Text}'").ToString();
                maindish1IDstring.Insert(0, "'");
                maindish1IDstring.Insert(maindish1IDstring.Length, "'");
            }
            if (HasValue(comboB_maindish2))
            {
                maindish2IDstring = db.RunQueryScalar($"select dish_id from maindish where description='{comboB_maindish2.Text}'").ToString();
                maindish2IDstring.Insert(0, "'");
                maindish2IDstring.Insert(maindish2IDstring.Length, "'");
            }


            if (db.RunNonQuery($"insert into menu values('{txt_datePicker.Text}',{sidedishIdstring},{maindish1IDstring},{maindish2IDstring},1)") != 0)
            {
                lbl_infoLabel.Text = "Speichern des Menütages erfolgreich!";
            }
            else lbl_infoLabel.Text = "Speichern des Menütages fehlgeschlagen.";

        }

        /// <summary>

        /// Creates the chosen dishes if they don't exist in the database
        /// </summary>
        private void AddNewDishes()
        {
            List<ComboBox> comboBoxList = new List<ComboBox>();
            comboBoxList.Add(comboB_maindish1);
            comboBoxList.Add(comboB_maindish2);
            comboBoxList.Add(comboB_sidedish);
            bool objectExists;
            foreach (ComboBox currentComboBox in comboBoxList)
            {
                //Bei dem Nebengericht muss auf eine andere Tabelle zugegriffen werden
                if (currentComboBox.ID == comboB_sidedish.ID)
                {
                    //schaut nach ob das Nebengericht schon existiert, wenn nicht fügt es es in sidedish ein
                    objectExists = ObjectExistsInTableOfDB(currentComboBox.Text, "sidedish", "description", db);
                    if (!objectExists && HasValue(currentComboBox))
                    {
                        int maxId = (int)db.RunQueryScalar("SELECT MAX(dish_id) FROM sidedish");
                        if (db.RunNonQuery($"insert into sidedish values({maxId + 1},'{currentComboBox.Text}')") != 0)
                        {
                            lbl_infoLabel.Text = $"Speichern von neuem Nebengericht '{currentComboBox.Text}' erfolgreich.";

                        }
                        else lbl_infoLabel.Text = $"Speichern von neuem Nebengericht '{currentComboBox.Text}' fehlgeschlagen.";
                    }
                }
                else
                {
                    objectExists = ObjectExistsInTableOfDB(currentComboBox.Text, "maindish", "description", db);
                    if (!objectExists && HasValue(currentComboBox))
                    {
                        int maxId = (int)db.RunQueryScalar("SELECT MAX(dish_id) FROM maindish");
                        if (db.RunNonQuery($"insert into maindish values({maxId + 1},'{currentComboBox.Text}')") != 0)
                        {
                            lbl_infoLabel.Text = $"Speichern von neuem Hauptgericht '{currentComboBox.Text}' erfolgreich.";

                        }
                        else lbl_infoLabel.Text = $"Speichern von neuem Hauptgericht '{currentComboBox.Text}' fehlgeschlagen.";
                    }
                }
            }
        }

        /// <summary>
        /// returns true if the comboboxes text has a usable entry
        /// if selectedIndex==0 it also returns false
        /// </summary>
        /// <param name="comboBox"></param>
        /// <returns></returns>
        private bool HasValue(ComboBox comboBox)
        {
            if(comboBox.Text == null|| comboBox.Text == ""||comboBox.SelectedIndex==0) return false;
            return true;
        }

        /// <summary>
        /// Checks if the format of the string is in the 'yyyy-mm-dd' date format;
        /// checks if date is available in database;
        /// if not throws exception
        /// </summary>
        /// <param name="text"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckIfDateIsOk(string dateToCheck)
        {
            if (!DateTime.TryParseExact(dateToCheck, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime menuDate))
            {
                throw new ApplicationException("Datum hat das falsche Format");
            }
            if (ObjectExistsInTableOfDB(dateToCheck, "menu", "menuDate", db))
            {
                txt_datePicker.BorderColor = System.Drawing.Color.Crimson;
                throw new ApplicationException("Es gibt schon ein Menü für das Datum " + dateToCheck);
            }
            txt_datePicker.BorderColor = default;
        }

        private bool ObjectExistsInTableOfDB(object objectToCheckFor, string tableName,string columnName, DataBase db)
        {
            if((db.RunQueryScalar($"SELECT {columnName} FROM {tableName} where {columnName} = '{objectToCheckFor.ToString()}'")!=null))
            {
                return true;
            }

            return false;
        }

        protected void btn_nextWeek_Click(object sender, EventArgs e)
        {
            //if (selectedWeekMonday == default) selectedWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            selectedWeekMonday= selectedWeekMonday.AddDays(7);
            ViewState["selectedWeek"] = selectedWeekMonday;          
            FillAdminOverviewTable(selectedWeekMonday);
            //Server.TransferRequest(Request.Url.AbsolutePath, false);

        }

        private void FlushMenus()
        {
            List<Label> menuLabels = new List<Label>();

            menuLabels.Add(lbl_dish1Monday);
            menuLabels.Add(lbl_dish2Monday);
            menuLabels.Add(lbl_sideDishMonday);

            menuLabels.Add(lbl_dish1Tuesday);
            menuLabels.Add(lbl_dish2Tuesday);
            menuLabels.Add(lbl_sideDishTuesday);

            menuLabels.Add(lbl_dish1Wednesday);
            menuLabels.Add(lbl_dish2Wednesday);
            menuLabels.Add(lbl_sideDishWednesday);

            menuLabels.Add(lbl_dish1Thursday);
            menuLabels.Add(lbl_dish2Thursday);
            menuLabels.Add(lbl_sideDishThursday);

            menuLabels.Add(lbl_dish1Friday);
            menuLabels.Add(lbl_dish2Friday);
            menuLabels.Add(lbl_sideDishFriday);


            foreach(Label lable in menuLabels)
            {
                lable.Text = "";
            }
        }

        protected void btn_lastWeek_Click(object sender, EventArgs e)
        {
            selectedWeekMonday = selectedWeekMonday.AddDays(-7);
            ViewState["selectedWeek"] = selectedWeekMonday;
            FillAdminOverviewTable(selectedWeekMonday);
        }

        protected void btn_deleteSelected_Click(object sender, EventArgs e)
        {
            db = new DataBase(connStrg);
            int deletedDates = 0;
            if(CheckBox_monday.Checked)
            {
                deletedDates+= DeleteDate(selectedWeekMonday);

            }
            if(CheckBox_tuesday.Checked)
            {
                deletedDates += DeleteDate(selectedWeekMonday.AddDays(1));

            }
            if(CheckBox_wednesday.Checked)
            {
                deletedDates += DeleteDate(selectedWeekMonday.AddDays(2));
            }
            if(CheckBox_thursday.Checked)
            {
                deletedDates += DeleteDate(selectedWeekMonday.AddDays(3));

            }
            if(CheckBox_friday.Checked)
            {
                deletedDates += DeleteDate(selectedWeekMonday.AddDays(4));
            }

            lbl_infoLabel.Text = $"{deletedDates} Menütage wurden gelöscht.";

            FillAdminOverviewTable(selectedWeekMonday);
            
        }


        /// <summary>
        /// Deletes a date from the DB
        /// Returns the affected Rows...
        /// 1=successful delete
        /// 0=unsuccessful
        /// </summary>
        /// <param name="dateToDelete"></param>
        /// <returns></returns>
        private int DeleteDate(DateTime dateToDelete)
        {
            int affectedRows=db.RunNonQuery($"DELETE FROM menu WHERE menuDate = '{dateToDelete.ToString("yyyy-MM-dd")}'");
            return affectedRows;
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("SignIn.aspx");
        }
    }

    


}
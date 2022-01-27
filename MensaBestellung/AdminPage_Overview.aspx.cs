using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
namespace MensaBestellung
{
    public partial class AdminPage : System.Web.UI.Page
    {
        DataBase db;
        string connStrg = WebConfigurationManager.ConnectionStrings["AppDbInt"].ConnectionString;
        //string connStrg = WebConfigurationManager.ConnectionStrings["AppDbExt"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            try 
            { 
                if(!IsPostBack) FillAdminDDls();
                
            }
            catch(Exception aex)
            {
                lbl_infoLabel.Text = lbl_infoLabel.Text  + "Error: "+aex.Message+ '\n';
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

        private bool FillAdminOverviewTable()
        {
            //db = new DataBase(connStrg);
            //db.Open();
            //db.Close();

            //DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            //DataTable dt = db.RunQuery($"SELECT * FROM menu WHERE dateOfDay BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");

            
            //table.DataSource = dt;
            //gv_foodExchange.DataBind();


            return true;
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
    }

    


}
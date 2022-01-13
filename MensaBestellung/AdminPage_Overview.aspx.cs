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

namespace MensaBestellung
{
    public partial class AdminPage : System.Web.UI.Page
    {
        DataBase db;
        string connStrg = WebConfigurationManager.ConnectionStrings["AppDbInt"].ConnectionString;
        //string connStrg = WebConfigurationManager.ConnectionStrings["AppDbExt"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            //if (!FillAdminOverviewTable()) lbl_infoLabel.Text = lbl_infoLabel.Text+ '\n' + "Error: Konnte Datenbank nicht füllen";

            try 
            { 
                FillAdminDDls(); 
                
            }
            catch(ApplicationException aex)
            {
                lbl_infoLabel.Text = lbl_infoLabel.Text  + "Error: "+aex.Message+ '\n';
            }

            

            
        }

        private void FillAdminDDls()
        {
            db = new DataBase(connStrg);
            
            DataTable dt= db.RunQuery("SELECT description FROM maindish");                
               
            ddl_dish1.DataSource = dt;
            ddl_dish1.DataTextField = "description";               
                
            ddl_dish1.DataBind();
            ddl_dish1.Items.Insert(0, new ListItem("--Hauptgericht 1--", "0"));


            ddl_dish2.DataSource = dt;
            ddl_dish2.DataTextField = "description";

            ddl_dish2.DataBind();
            ddl_dish2.Items.Insert(0, new ListItem("--Hauptgericht 2--", "0"));


            dt = db.RunQuery("SELECT description FROM sidedish");
            ddl_sidedish.DataSource = dt;
            ddl_sidedish.DataTextField = "description";

            ddl_sidedish.DataBind();
            ddl_sidedish.Items.Insert(0, new ListItem("--Vorspeise--", "0"));
            




            
        }

        private bool FillAdminOverviewTable()
        {
            db = new DataBase(connStrg);
            db.Open();
            db.Close();

            DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DataTable dt = db.RunQuery($"SELECT * FROM menu WHERE dateOfDay BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");

            
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
            
            if(!DateTime.TryParseExact(txt_datePicker.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime menuDate))
            {
                throw new ApplicationException("Datum hat das falsche Format");
            }
            
            db = new DataBase(connStrg);

            //DataTable dt = db.RunQuery($"SELECT dish_id FROM maindish where description='{ddl_dish1.Text}'");
            //var maindish1ID= db.RunQueryScalar("SELECT dish_id FROM maindish where description='Putencurry mit Reis'");
            //string cmd = $"SELECT dish_id FROM maindish where description='{ddl_dish1.Text}'";
            string cmd = $"SELECT dish_id FROM maindish where description='{ddl_dish1.SelectedValue}'";
            //var maindish1ID = db.RunQueryScalar("SELECT dish_id FROM maindish where description='@ddl_dish1.Text'");
            var maindish1ID = db.RunQueryScalar(cmd);

            int i = 0;
            //string sqlCmd = $"INSERT INTO menu VALUES('{txt_datePicker.Text}', 0, 0, 1, 1)";
            

                        
           
        }
    }

    


}
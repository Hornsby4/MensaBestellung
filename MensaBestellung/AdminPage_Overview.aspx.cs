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
    public partial class AdminPage : System.Web.UI.Page
    {
        DataBase db;
        string connStrg = WebConfigurationManager.ConnectionStrings["AppDbInt"].ConnectionString;
        //string connStrg = WebConfigurationManager.ConnectionStrings["AppDbExt"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            DayOfWeek wk = DateTime.Today.DayOfWeek;

            //if (!FillAdminOverviewTable()) lbl_infoLabel.Text = lbl_infoLabel.Text+ '\n' + "Error: Konnte Datenbank nicht füllen";

            if (!FillAdminDDls()) lbl_infoLabel.Text = lbl_infoLabel.Text + '\n' + "Error: Konnte DropdownLists nicht füllen";

            
        }

        private bool FillAdminDDls()
        {
            db = new DataBase(connStrg);
            
            try
            {
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
            catch(Exception x)
            {
                lbl_infoLabel.Text = lbl_infoLabel.Text + x.Message;
                return false;
            }




            return true;
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
            if (Page.IsValid)
            {
                try
                {
                    db = new DataBase(connStrg);
                    DataTable dt = db.RunQuery("SELECT description FROM maindish");

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
                catch (Exception x)
                {
                    lbl_infoLabel.Text = lbl_infoLabel.Text + x.Message;
                    
                }

            }
            
           
        }
    }

    


}
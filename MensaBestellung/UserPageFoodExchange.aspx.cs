using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

        DateTime currentWeekMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    FillGV();
                }
            }
            catch (Exception ex)
            {
                lbl_info.Text = ex.Message;
                return;
            }
        }

        private void DisableDoubleOrder()
        {
            DataTable dtOrders = db.RunQuery($"SELECT menuDate FROM user_orders_menu WHERE user_id = {Session["UserID"]} " +
                $"AND user_orders_menu.menuDate BETWEEN '{currentWeekMonday.ToString("yyyy-MM-dd")}' AND '{currentWeekMonday.AddDays(4).ToString("yyyy-MM-dd")}'");
            foreach (GridViewRow row in gv_foodExchange.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("buy");
                foreach (DataRow menuDate in dtOrders.Rows)
                {
                    if (row.Cells[0].Text == ((DateTime)menuDate[0]).ToString("dd.MM.yyyy"))
                    {
                        chk.Enabled = false;
                    }
                }
            }
        }

        private void FillGV()
        {
            db = new DataBase(connStrg);
            db.Open();
            db.Close();


            DataTable dt = db.RunQuery($"SELECT menu.menuDate, " +
                $"CONCAT(" +
                $"COALESCE(CONCAT('Beilage: ', sidedish.description, '<br>'),''), " +
                $"COALESCE(CONCAT('Hauptspeise1: ', main1.description, '<br>'), ''), " +
                $"COALESCE(CONCAT('Hauptspeise2: ', main2.description), '')) AS menu, " +
                $"CONCAT(user.firstname, ' ', user.lastName) AS seller " +
                $"FROM user_orders_menu " +
                $"JOIN menu ON user_orders_menu.menuDate = menu.menuDate " +
                $"JOIN user ON user_orders_menu.user_id = user.user_id " +
                $"LEFT JOIN sidedish ON menu.sideDish = sidedish.dish_id " +
                $"LEFT JOIN maindish main1 ON menu.mainDish1 = main1.dish_id " +
                $"LEFT JOIN maindish main2 ON menu.mainDish2 = main2.dish_id " +
                $"WHERE user_orders_menu.foodExchange = 1 " +
                $"AND user_orders_menu.menuDate BETWEEN '{(TimeSpan.Compare(DateTime.Now.TimeOfDay,new TimeSpan(13,30,00))>0?DateTime.Now.ToString("yyyy-MM-dd"):DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))}' AND '{currentWeekMonday.AddDays(4).ToString("yyyy -MM-dd")}'");

            gv_foodExchange.DataSource = dt;
            gv_foodExchange.DataBind();

            DisableDoubleOrder();
        }

        protected void SelectCheckBox_OnCheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox cb1 = (CheckBox)gv_foodExchange.Rows[index].FindControl("buy");
            bool isChecked = cb1.Checked;

            row.BackColor = Color.Yellow;
            
            //row.BackColor = default(Color);
        }

        protected void btn_saveExchangeFoodOrder_Click(object sender, EventArgs e)
        {
            DialogBox dialogBox = (DialogBox)LoadControl("DialogBox.ascx");
            dialogBox.Title = "Speichern";
            try
            {
                db = new DataBase(connStrg);
                List<string> cart = new List<string>();
                foreach (GridViewRow row in gv_foodExchange.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("buy");
                    if (chk != null && chk.Checked)
                    {
                        int uIDtoBuy = Convert.ToInt32(db.RunQueryScalar($"SELECT user_id FROM user WHERE firstname = '{row.Cells[3].Text.Split(' ')[0]}' AND lastname = '{row.Cells[3].Text.Split(' ')[1]}'"));
                        cart.Add($"{row.Cells[0].Text};{uIDtoBuy}");
                    }
                }
                bool execute = false;
                string sqlCmd = "UPDATE user_orders_menu " +
                    $"SET user_id = {Session["UserID"]}, foodExchange = 0 " +
                    $"WHERE ";
                foreach (string item in cart)
                {
                    execute = true;
                    sqlCmd += $"menuDate = '{DateTime.ParseExact(item.Split(';')[0], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture):yyyy-MM-dd}' AND user_id = {item.Split(';')[1]} OR ";
                }
                if (execute) db.RunNonQuery(sqlCmd.Remove(sqlCmd.Length - 3));
                dialogBox.description("Essen wurde bestellt");
            }
            catch (Exception ex)
            {
                dialogBox.description("Fehler! Es ist ein Problem aufgetreten, bitte versuchen Sie es später nocheinmal.");
            }
            finally
            {
                FillGV();
                
                form1.Controls.Add(dialogBox);
            }
        }

        protected void btn_goBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserPage.aspx");
        }

    }
}
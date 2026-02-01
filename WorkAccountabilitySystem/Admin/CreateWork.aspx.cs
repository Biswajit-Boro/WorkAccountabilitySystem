using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAccountabilitySystem.Infrastructure;


namespace WorkAccountabilitySystem.Admin
{
    public partial class CreateWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Create_Click(object sender, EventArgs e)
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO WorkItems (Description, CreatedBy, Priority)
              VALUES (@d, @a, @p)", c);

                cmd.Parameters.AddWithValue("@d", txtDesc.Text.Trim());
                cmd.Parameters.AddWithValue("@a", Session["UserId"]);
                cmd.Parameters.AddWithValue("@p", ddlPriority.SelectedValue);

                cmd.ExecuteNonQuery();
            }

            Response.Redirect("~/Dashboard.aspx");
        }

    }
}
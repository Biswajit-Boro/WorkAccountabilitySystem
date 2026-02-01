using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem.Admin
{
    public partial class AdminActions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Auth.RequireRole("Admin");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO AdminActions
                    (WorkId, AdminId, ActionType, Reason)
                    VALUES (@w, @a, @t, @r)", c);

                cmd.Parameters.AddWithValue("@w", txtWorkId.Text);
                cmd.Parameters.AddWithValue("@a", Session["UserId"]);
                cmd.Parameters.AddWithValue("@t", ddlAction.SelectedValue);
                cmd.Parameters.AddWithValue("@r", txtReason.Text.Trim());

                cmd.ExecuteNonQuery();
            }
        }
    }
}
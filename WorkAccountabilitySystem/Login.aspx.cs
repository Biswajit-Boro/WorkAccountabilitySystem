using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAccountabilitySystem.Infrastructure;
using System.Data.SqlClient;


namespace WorkAccountabilitySystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT UserId, Role 
                  FROM Users 
                  WHERE Username = @u 
                    AND PasswordHash = @p 
                    AND IsActive = 1", c);

                cmd.Parameters.AddWithValue("@u", txtUser.Text.Trim());
                cmd.Parameters.AddWithValue("@p", txtPass.Text.Trim());

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    Session["UserId"] = r["UserId"];
                    Session["Role"] = r["Role"].ToString();

                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    // Optional minimal feedback (no UI dependency)
                    Response.Write("Invalid credentials");
                }
            }
        }
    }
}
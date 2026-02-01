using System;
using System.Data.SqlClient;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem.User
{
    public partial class OptIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Auth.RequireRole("User");

            if (!IsPostBack && Request.QueryString["workId"] != null)
            {
                txtWorkId.Text = Request.QueryString["workId"];
            }
        }

        protected void OptIn_Click(object sender, EventArgs e)
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO WorkOptIn (WorkId, UserId)
                    VALUES (@w, @u)", c);

                cmd.Parameters.AddWithValue("@w", txtWorkId.Text);
                cmd.Parameters.AddWithValue("@u", Session["UserId"]);

                cmd.ExecuteNonQuery();
            }

            Response.Redirect("~/Dashboard.aspx");
        }
    }
}

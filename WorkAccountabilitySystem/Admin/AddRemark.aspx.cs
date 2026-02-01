using System;
using System.Data.SqlClient;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem.Admin
{
    public partial class AddRemark : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Auth.RequireRole("Admin");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            // ADDED: validate WorkId presence
            if (Request.QueryString["workId"] == null)
            {
                // ADDED: hard stop if page is misused
                throw new Exception("WorkId missing in request.");
            }

            int workId = int.Parse(Request.QueryString["workId"]);

            int progressId;

            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                // EXISTING QUERY (unchanged)
                SqlCommand getProgress = new SqlCommand(
                    @"SELECT TOP 1 ProgressId
                      FROM ProgressUpdates
                      WHERE WorkId = @w
                      ORDER BY CreatedDate DESC", c);

                getProgress.Parameters.AddWithValue("@w", workId);

                // CHANGED: store result safely
                object result = getProgress.ExecuteScalar(); // ADDED

                if (result == null) // ADDED
                {
                    // ADDED: no user progress exists yet
                    Response.Write("<script>alert('Cannot add admin remark. No user progress exists yet.');</script>");
                    return; // ADDED: stop execution safely
                }

                progressId = Convert.ToInt32(result); // CHANGED (safe conversion)
            }

            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                // EXISTING INSERT (unchanged logic)
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO AdminRemarks
                      (ProgressId, AdminId, Remark)
                      VALUES (@p, @a, @r)", c);

                cmd.Parameters.AddWithValue("@p", progressId); // CHANGED: derived value
                cmd.Parameters.AddWithValue("@a", Session["UserId"]);
                cmd.Parameters.AddWithValue("@r", txtRemark.Text.Trim());

                cmd.ExecuteNonQuery();
            }

            // ADDED: redirect after successful insert
            Response.Redirect("/Dashboard.aspx");
        }
    }
}

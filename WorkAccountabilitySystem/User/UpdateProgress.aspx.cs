using System;
using System.Data.SqlClient;
using System.Web.UI;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem.User
{
    public partial class UpdateProgress : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Auth.RequireRole("User");

            if (!IsPostBack)
            {
                LoadWork();
            }
        }

        private void LoadWork()
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT w.WorkId, w.Description
                    FROM WorkItems w
                    JOIN WorkOptIn o ON o.WorkId = w.WorkId
                    WHERE o.UserId = @u
                      AND w.IsDeleted = 0", c);

                cmd.Parameters.AddWithValue("@u", Session["UserId"]);

                ddlWork.DataSource = cmd.ExecuteReader();
                ddlWork.DataTextField = "Description";
                ddlWork.DataValueField = "WorkId";
                ddlWork.DataBind();
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO ProgressUpdates
                    (WorkId, UserId, Status, UserRemark)
                    VALUES (@w, @u, @s, @r)", c);

                cmd.Parameters.AddWithValue("@w", ddlWork.SelectedValue);
                cmd.Parameters.AddWithValue("@u", Session["UserId"]);
                cmd.Parameters.AddWithValue("@s", ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@r", txtRemark.Text.Trim());

                cmd.ExecuteNonQuery();
            }
        }
    }
}

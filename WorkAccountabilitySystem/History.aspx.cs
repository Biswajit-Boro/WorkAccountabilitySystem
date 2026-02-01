using System;
using System.Data;
using System.Data.SqlClient;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProgress();
                LoadAdminActions();
            }
        }

        private void LoadProgress()
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    u.Username,
                    p.Status,
                    p.UserRemark,
                    p.CreatedDate,
                    ISNULL('Admin replied: ' + ar.Remark,'') AS AdminRemark
                FROM ProgressUpdates p
                JOIN Users u ON u.UserId = p.UserId
                LEFT JOIN AdminRemarks ar ON ar.ProgressId = p.ProgressId
                WHERE p.WorkId = @w
                ORDER BY p.CreatedDate
                ", c);

                cmd.Parameters.AddWithValue("@w", Request.QueryString["workId"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptHistory.DataSource = dt;
                rptHistory.DataBind();
            }
        }

        private void LoadAdminActions()
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    a.ActionType,
                    a.Reason,
                    u.Username,
                    a.CreatedDate
                FROM AdminActions a
                JOIN Users u ON u.UserId = a.AdminId
                WHERE a.WorkId = @w
                ORDER BY a.CreatedDate
                ", c);

                cmd.Parameters.AddWithValue("@w", Request.QueryString["workId"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptAdmin.DataSource = dt;
                rptAdmin.DataBind();
            }
        }
    }
}

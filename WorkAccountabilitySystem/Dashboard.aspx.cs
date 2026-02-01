using System;
using System.Data;
using System.Data.SqlClient;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadActive();
        }

        private void LoadActive()
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT
                    w.WorkId,
                    w.Description,
                    w.Priority,

                    ISNULL(
                        (SELECT TOP 1 p.Status
                         FROM ProgressUpdates p
                         WHERE p.WorkId = w.WorkId
                         ORDER BY p.CreatedDate DESC),
                        'Open'
                    ) AS Status,

                    ISNULL(
                        STUFF((
                            SELECT ', ' + u.Username
                            FROM WorkOptIn o
                            JOIN Users u ON u.UserId = o.UserId
                            WHERE o.WorkId = w.WorkId
                            FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)'),1,2,''),
                        '—'
                    ) AS Users

                FROM WorkItems w
                WHERE w.IsDeleted = 0
                AND NOT EXISTS (
                    SELECT 1 FROM AdminActions a
                    WHERE a.WorkId = w.WorkId
                    AND a.ActionType = 'ForceClosed'
                )
                ORDER BY
                    CASE w.Priority
                        WHEN 'High' THEN 1
                        WHEN 'Medium' THEN 2
                        WHEN 'Low' THEN 3
                    END,
                    w.CreatedDate DESC
                ", c);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvActive.DataSource = dt;
                gvActive.DataBind();
            }
        }
        protected bool IsAdmin()
        {
            return Session["Role"] != null &&
                   Session["Role"].ToString() == "Admin";
        }

        protected bool IsUser()
        {
            return Session["Role"] != null &&
                   Session["Role"].ToString() == "User";
        }

    }
}

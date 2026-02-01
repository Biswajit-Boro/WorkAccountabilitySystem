using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using WorkAccountabilitySystem.Infrastructure;

namespace WorkAccountabilitySystem
{
    public partial class AllHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadTimeline();
                LoadWorkWise();
            }
        }

        // ================= TIMELINE =================
        private void LoadTimeline()
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(@"
                SELECT EventTime, EventType, Actor, Description, WorkId
                FROM (
                    SELECT
                        w.CreatedDate AS EventTime,
                        'Work Created' AS EventType,
                        u.Username AS Actor,
                        w.Description AS Description,
                        w.WorkId AS WorkId
                    FROM WorkItems w
                    JOIN Users u ON u.UserId = w.CreatedBy

                    UNION ALL
                    SELECT
                        o.OptInDate AS EventTime,
                        'User Opted In' AS EventType,
                        u.Username AS Actor,
                        'Opted in' AS Description,
                        o.WorkId AS WorkId
                    FROM WorkOptIn o
                    JOIN Users u ON u.UserId = o.UserId

                    UNION ALL
                    SELECT
                        p.CreatedDate AS EventTime,
                        'Progress ' + p.Status AS EventType,
                        u.Username AS Actor,
                        p.UserRemark AS Description,
                        p.WorkId AS WorkId
                    FROM ProgressUpdates p
                    JOIN Users u ON u.UserId = p.UserId

                    UNION ALL
                    SELECT
                        ar.CreatedDate AS EventTime,
                        'Admin Reply' AS EventType,
                        u.Username AS Actor,
                        ar.Remark AS Description,
                        p.WorkId AS WorkId
                    FROM AdminRemarks ar
                    JOIN Users u ON u.UserId = ar.AdminId
                    JOIN ProgressUpdates p ON p.ProgressId = ar.ProgressId

                    UNION ALL
                    SELECT
                        a.CreatedDate AS EventTime,
                        'Admin ' + a.ActionType AS EventType,
                        u.Username AS Actor,
                        a.Reason AS Description,
                        a.WorkId AS WorkId
                    FROM AdminActions a
                    JOIN Users u ON u.UserId = a.AdminId
                ) X
                ORDER BY EventTime DESC", c);

                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);

                rptAllHistory.DataSource = dt;
                rptAllHistory.DataBind();
            }
        }

        // ================= WORK-WISE =================
        private void LoadWorkWise()
        {
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                // ADDED: get latest status per work
                SqlCommand wcmd = new SqlCommand(@"
                SELECT
                    w.WorkId,
                    w.Description,
                    ISNULL((
                        SELECT TOP 1 p.Status
                        FROM ProgressUpdates p
                        WHERE p.WorkId = w.WorkId
                        ORDER BY p.CreatedDate DESC
                    ), 'Open') AS LatestStatus
                FROM WorkItems w", c);

                DataTable workDt = new DataTable();
                new SqlDataAdapter(wcmd).Fill(workDt);

                var map = new Dictionary<int, dynamic>();

                foreach (DataRow w in workDt.Rows)
                {
                    map[(int)w["WorkId"]] = new
                    {
                        WorkId = (int)w["WorkId"],
                        Description = w["Description"],
                        LatestStatus = w["LatestStatus"],
                        Events = new List<object>()
                    };
                }

                // 🔴 FIXED: every column explicitly aliased
                SqlCommand ecmd = new SqlCommand(@"
                SELECT EventTime, Actor, Text, WorkId
                FROM (
                    SELECT
                        p.CreatedDate AS EventTime,
                        u.Username AS Actor,
                        'User: ' + p.Status + ' - ' + p.UserRemark AS Text,
                        p.WorkId AS WorkId
                    FROM ProgressUpdates p
                    JOIN Users u ON u.UserId = p.UserId

                    UNION ALL
                    SELECT
                        ar.CreatedDate AS EventTime,
                        u.Username AS Actor,
                        'Admin Reply: ' + ar.Remark AS Text,
                        p.WorkId AS WorkId
                    FROM AdminRemarks ar
                    JOIN Users u ON u.UserId = ar.AdminId
                    JOIN ProgressUpdates p ON p.ProgressId = ar.ProgressId

                    UNION ALL
                    SELECT
                        a.CreatedDate AS EventTime,
                        u.Username AS Actor,
                        'Admin Action: ' + a.ActionType + ' - ' + a.Reason AS Text,
                        a.WorkId AS WorkId
                    FROM AdminActions a
                    JOIN Users u ON u.UserId = a.AdminId
                ) X
                ORDER BY WorkId, EventTime", c);

                DataTable evDt = new DataTable();
                new SqlDataAdapter(ecmd).Fill(evDt);

                foreach (DataRow r in evDt.Rows)
                {
                    int wid = (int)r["WorkId"];

                    map[wid].Events.Add(new
                    {
                        EventTime = r["EventTime"],
                        Actor = r["Actor"],
                        Text = r["Text"]
                    });
                }

                rptWorkWise.DataSource = map.Values;
                rptWorkWise.DataBind();
            }
        }

        // ================= ADMIN REOPEN =================
        protected void Reopen_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
                return;

            int workId = int.Parse(e.CommandArgument.ToString());

            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand a = new SqlCommand(@"
                INSERT INTO AdminActions
                (WorkId, AdminId, ActionType, Reason)
                VALUES (@w, @a, 'Reopen', 'Reopened from All History')", c);

                a.Parameters.AddWithValue("@w", workId);
                a.Parameters.AddWithValue("@a", Session["UserId"]);
                a.ExecuteNonQuery();

                // CRITICAL: reopen must change state
                SqlCommand p = new SqlCommand(@"
                INSERT INTO ProgressUpdates
                (WorkId, UserId, Status, UserRemark)
                VALUES (@w, @u, 'InProgress', 'Reopened by admin')", c);

                p.Parameters.AddWithValue("@w", workId);
                p.Parameters.AddWithValue("@u", Session["UserId"]);
                p.ExecuteNonQuery();
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void ShowTimeline_Click(object sender, EventArgs e)
        {
            pnlTimeline.Visible = true;
            pnlWorkWise.Visible = false;
        }

        protected void ShowWorkWise_Click(object sender, EventArgs e)
        {
            pnlTimeline.Visible = false;
            pnlWorkWise.Visible = true;
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}

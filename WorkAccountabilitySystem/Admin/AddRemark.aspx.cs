using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            using (SqlConnection c = Db.Conn())
            {
                c.Open();

                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO AdminRemarks
                      (ProgressId, AdminId, Remark)
                      VALUES (@p, @a, @r)", c);

                cmd.Parameters.AddWithValue("@p", txtProgressId.Text);
                cmd.Parameters.AddWithValue("@a", Session["UserId"]);
                cmd.Parameters.AddWithValue("@r", txtRemark.Text.Trim());

                cmd.ExecuteNonQuery();
            }
        }
    }
}
    
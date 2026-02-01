using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkAccountabilitySystem.Infrastructure
{
    public static class Auth
    {
        public static void RequireRole(string role)
        {
            if (HttpContext.Current.Session["Role"] == null ||
                HttpContext.Current.Session["Role"].ToString() != role)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace WorkAccountabilitySystem.Infrastructure
{
    public static class Db
    {
        public static SqlConnection Conn()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB"].ConnectionString
            );
        }
    }
}
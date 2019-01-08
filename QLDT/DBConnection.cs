using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace QLDT
{
    class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            //Data Source=DESKTOP-ON3CI0I\SQLEXPRESS;Initial Catalog=QLDT_V1;Integrated Security=True
            string datasource = @"DESKTOP-ON3CI0I\SQLEXPRESS";
            string database = "QLDT_V1";
            string connString = @"Data Source=" + datasource + ";Initial Catalog="+ database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
